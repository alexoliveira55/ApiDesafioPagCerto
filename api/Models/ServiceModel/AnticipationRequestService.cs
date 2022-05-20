using api.Models.EntityModel;
using api.Models.ServiceModel.Interfaces;
using api.Repository.Interfaces;
using api.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static api.Utils.Utilities;
using System.Linq;

namespace api.Models.ServiceModel
{
    public class AnticipationRequestService : IAnticipationRequestService
    {
        protected readonly IAnticipationRequestRepository _IAnticipationRequestRepository;
        protected readonly ITransactionAnticipationRequestRepository _ITransactionAnticipationRequestRepository;
        protected readonly ITransactionCardService _ITransactionCardService;
        public AnticipationRequestService(IAnticipationRequestRepository iAnticipationRequestRepository, ITransactionAnticipationRequestRepository iTransactionAnticipationRequestRepository, ITransactionCardService iTransactionCardService)
        {
            _IAnticipationRequestRepository = iAnticipationRequestRepository;
            _ITransactionAnticipationRequestRepository = iTransactionAnticipationRequestRepository;
            _ITransactionCardService = iTransactionCardService;
        }

        private async Task<IEnumerable<TransactionAnticipationRequest>> ConsultUnderAnalysis()
        {
            return await _ITransactionAnticipationRequestRepository.ConsultComplete(t => t.StatusAnticipation == EnStatusAnticipationRequest.UnderAnalysis.GetHashCode());
        }

        private async Task<IEnumerable<TransactionAnticipationRequest>> ConsultCompletedRequest()
        {
            return await _ITransactionAnticipationRequestRepository.ConsultComplete(t => t.StatusAnticipation == EnStatusAnticipationRequest.Finished.GetHashCode());
        }

        private async Task<IEnumerable<TransactionAnticipationRequest>> ConsultPendingRequest()
        {
            return await _ITransactionAnticipationRequestRepository.ConsultComplete(t => t.StatusAnticipation == EnStatusAnticipationRequest.Pending.GetHashCode());
        }

        public async Task<IEnumerable<TransactionAnticipationRequest>> ConsultRequestByStatus(EnStatusAnticipationRequest enStatusAnticipationRequest)
        {
            switch (enStatusAnticipationRequest)
            {
                case EnStatusAnticipationRequest.Pending:
                    return await ConsultPendingRequest();
                case EnStatusAnticipationRequest.UnderAnalysis:
                    return await ConsultUnderAnalysis();
                case EnStatusAnticipationRequest.Finished:
                    return await ConsultCompletedRequest();
                default:
                    return null;
            }
        }

        public async Task<bool> ExistsPendingRequestForTransactionCardNsu(long nsu)
        {
            var transactionRequest = await _ITransactionAnticipationRequestRepository.Consult(t => t.StatusAnticipation != EnStatusAnticipationRequest.Finished.GetHashCode());

            if (transactionRequest == null) return false;

            return transactionRequest.Count() > 0;
        }

        public async Task<AnticipationRequest> RequestAnticipationTransactions(IEnumerable<long> nsuTransactions)
        {
            List<TransactionAnticipationRequest> transactionAnticipationRequests = new List<TransactionAnticipationRequest>();
            AnticipationRequest anticipationRequest = new AnticipationRequest();
            foreach (var nsu in nsuTransactions)
            {
                if (await ExistsPendingRequestForTransactionCardNsu(nsu)) continue;

                var transactionCard = await _ITransactionCardService.GetTransactionByNsu(nsu);

                if (transactionCard == null) continue;

                if (!transactionCard.ConfirmationAcquirer) continue;

                if (transactionCard.Anticipated) continue;

                transactionAnticipationRequests.Add(new TransactionAnticipationRequest(transactionCard));
                anticipationRequest.ValueAnticipationRequest += transactionCard.ValueLiquid;
            }

            if (anticipationRequest.ValueAnticipationRequest == 0) return null;

            await _IAnticipationRequestRepository.Add(anticipationRequest);

            foreach (var transactionRequest in transactionAnticipationRequests)
            {
                transactionRequest.IdAnticipationRequest = anticipationRequest.Id;
                await _ITransactionAnticipationRequestRepository.Add(transactionRequest);
            }
            anticipationRequest.TransactionAnticipationRequests.AddRange(transactionAnticipationRequests);

            return anticipationRequest;
        }

        public async Task<AnticipationRequest> GetRequestAnticipationTransactionById(long id)
        {
            return await _IAnticipationRequestRepository.GetRequestAnticipationTransactionById(id);
        }

        public async Task UpdateRequestAnticipation(AnticipationRequest anticipation)
        {
            await _IAnticipationRequestRepository.Update(anticipation);
        }

        public async Task ApproveTransactionAntecipationRequest(TransactionAnticipationRequest transactionAnticipationRequest)
        {
            await ApproveDesapproveTransactionAnticipation(transactionAnticipationRequest, EnResultAnalizeAnticipationRequest.Approved);
            await ValidateFinishRequestAnticipation(transactionAnticipationRequest);
        }

        public async Task DisapproveTransactionAntecipationRequest(TransactionAnticipationRequest transactionAnticipationRequest)
        {
            await ApproveDesapproveTransactionAnticipation(transactionAnticipationRequest, EnResultAnalizeAnticipationRequest.Disapproved);
            await ValidateFinishRequestAnticipation(transactionAnticipationRequest);
        }

        private async Task ApproveDesapproveTransactionAnticipation(TransactionAnticipationRequest transactionAnticipationRequest, EnResultAnalizeAnticipationRequest enResultAnalizeAnticipationRequest)
        {
            if (transactionAnticipationRequest == null) return;
            transactionAnticipationRequest.ResultAnalizeTransaction = enResultAnalizeAnticipationRequest.GetHashCode();
            transactionAnticipationRequest.StatusAnticipation = EnStatusAnticipationRequest.Finished.GetHashCode();
            await _ITransactionAnticipationRequestRepository.Update(transactionAnticipationRequest);
        }
        private async Task ValidateFinishRequestAnticipation(TransactionAnticipationRequest transactionAnticipationRequest)
        {
            var requestAnticipation = await GetRequestAnticipationTransactionById(transactionAnticipationRequest.IdAnticipationRequest);

            if (requestAnticipation == null) return;

            var transactionCard = await _ITransactionCardService.GetTransactionWithInstallmentByNsu(transactionAnticipationRequest.NsuTransction);

            if (transactionCard == null) return;


            var transactionsAnticipationsFromRequest = await _ITransactionAnticipationRequestRepository.Consult(t => t.IdAnticipationRequest == requestAnticipation.Id);

            if (transactionsAnticipationsFromRequest.All(t => t.StatusAnticipation == EnStatusAnticipationRequest.Finished.GetHashCode() && t.ResultAnalizeTransaction == EnResultAnalizeAnticipationRequest.Disapproved.GetHashCode()))
            {
                requestAnticipation.ResultAnticipationRequest = EnResultAnalizeAnticipationRequest.Disapproved.GetHashCode();
                requestAnticipation.DateEndAnalyze = DateTime.Now;
            }
            else if (transactionsAnticipationsFromRequest.All(t => t.StatusAnticipation == EnStatusAnticipationRequest.Finished.GetHashCode() && t.ResultAnalizeTransaction == EnResultAnalizeAnticipationRequest.Approved.GetHashCode()))
            {
                requestAnticipation.ResultAnticipationRequest = EnResultAnalizeAnticipationRequest.Approved.GetHashCode();
                requestAnticipation.DateEndAnalyze = DateTime.Now;
            }
            else if (transactionsAnticipationsFromRequest.All(t => t.StatusAnticipation == EnStatusAnticipationRequest.Finished.GetHashCode()))
            {
                requestAnticipation.ResultAnticipationRequest = EnResultAnalizeAnticipationRequest.ApprovedPartially.GetHashCode();
                requestAnticipation.DateEndAnalyze = DateTime.Now;
            }


            foreach (var installment in transactionCard.InstallmentTransactions)
            {
                installment.AnticipatedValue = Utilities.DiscountPercentageRate(installment.ValueLiquid);
                installment.DateAdvancedPayment = DateTime.Now;
                requestAnticipation.AnticipatedValue += installment.AnticipatedValue;
                await _ITransactionCardService.UpdateInstallmentTransactionCard(installment);
            }

            transactionCard.Anticipated = true;

            await _IAnticipationRequestRepository.Update(requestAnticipation);
            await _ITransactionCardService.UpdateTransactionCard(transactionCard);
        }

        public async Task<TransactionAnticipationRequest> GetTransactionAnticipationRequestById(long id, long nsu)
        {
            var result = await _ITransactionAnticipationRequestRepository.Consult(t => t.NsuTransction == nsu && t.IdAnticipationRequest == id);

            return result.FirstOrDefault();
        }

        public async Task<bool> ExistsTransactionNonFinishedOnAnticipationRequest(long id)
        {
            var transactionsAnticipations = await _ITransactionAnticipationRequestRepository.Consult(t => t.IdAnticipationRequest == id && t.StatusAnticipation != EnStatusAnticipationRequest.Finished.GetHashCode());

            if (transactionsAnticipations == null) return false;

            return transactionsAnticipations.Count() > 0;
        }

        public async Task SetRequestAnticipationInAnalysis(AnticipationRequest anticipation)
        {
            anticipation.DateStartAnalyze = DateTime.Now;
            await UpdateRequestAnticipation(anticipation);
            var transactionsAnticipations = await _ITransactionAnticipationRequestRepository.Consult(t => t.IdAnticipationRequest == anticipation.Id);

            foreach (var transaction in transactionsAnticipations)
            {
                transaction.StatusAnticipation = EnStatusAnticipationRequest.UnderAnalysis.GetHashCode();
                await _ITransactionAnticipationRequestRepository.Update(transaction);
            }            


        }
    }

}

using api.Models.EntityModel;
using api.Models.ServiceModel.Interfaces;
using api.Models.ViewModel;
using api.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static api.Utils.Utilities;

namespace api.Models.ServiceModel
{
    public class TransactionCardService : ITransactionCardService
    {
        protected readonly ITransactionCardRepository _ITransactionCardRepository;
        protected readonly ITransactionAnticipationRequestRepository _ITransactionAnticipationRequestRepository;
        protected readonly IInstallmentTransactionRepository _IInstallmentTransactionRepository;
        public TransactionCardService(ITransactionCardRepository iTransactionCardRepository, IInstallmentTransactionRepository iInstallmentTransactionRepository, ITransactionAnticipationRequestRepository iTransactionAnticipationRequestRepository)
        {
            _ITransactionCardRepository = iTransactionCardRepository;
            _IInstallmentTransactionRepository = iInstallmentTransactionRepository;
            _ITransactionAnticipationRequestRepository = iTransactionAnticipationRequestRepository;
        }

        public async Task<TransactionCard> GetTransactionByNsu(long nsu)
        {
            return await _ITransactionCardRepository.GetTransactionByNsu(nsu);
        }

        public async Task<List<TransactionCard>> GetTransactionsForAnticipation()
        {
            List<TransactionCard> transactionCards = new List<TransactionCard>();
            var transactions = await _ITransactionCardRepository.Consult(t => !t.Anticipated && t.ConfirmationAcquirer);

            foreach (var transaction in transactions)
            {
                var transactionRequest = await _ITransactionAnticipationRequestRepository.Consult(t => t.StatusAnticipation != EnStatusAnticipationRequest.Finished.GetHashCode());
                if (transactionRequest.Count() == 0)
                {
                    transactionCards.Add(transaction);
                }
            }
            return transactionCards;
        }

        public async Task<TransactionCard> GetTransactionWithInstallmentByNsu(long nsu)
        {
            return await _ITransactionCardRepository.GetTransactionByNsuWithInstallments(nsu);
        }

        public async Task<TransactionCard> MakePaymentWithCard(TransactionCardViewModel transactionViewModel)
        {
            var transactionCard = TransactionCard.ParseViewModel(transactionViewModel);

            if (transactionCard == null) return null;

            transactionCard.ApproveDisapproveTransaction();

            await _ITransactionCardRepository.Add(transactionCard);

            if (!transactionCard.ConfirmationAcquirer) return transactionCard;

            GenerateInstallments(transactionCard);

            return transactionCard;
        }

        public async Task UpdateInstallmentTransactionCard(InstallmentTransaction installment)
        {
            await _IInstallmentTransactionRepository.Update(installment);
        }

        public async Task UpdateTransactionCard(TransactionCard transactionCard)
        {
            await _ITransactionCardRepository.Update(transactionCard);
        }



        private async void GenerateInstallments(TransactionCard transactionCard)
        {
            if (transactionCard == null) return;
            if (transactionCard.Nsu <= 0) return;

            for (int installmentCounter = 0; installmentCounter <= transactionCard.NumberOfInstallments - 1; installmentCounter++)
            {
                var installment = new InstallmentTransaction
                {
                    Id = 0,
                    AnticipatedValue = null,
                    DateAdvancedPayment = null,
                    ExpectedDateReceivement = InstallmentTransaction.GetExpectedDate(transactionCard.TransactionDate, installmentCounter + 1),
                    InstallmentNumber = installmentCounter + 1,
                    NsuTransaction = transactionCard.Nsu,
                    TransactionCard = transactionCard,
                    ValueGross = transactionCard.ValueGrossForInstallment,
                    ValueLiquid = transactionCard.ValueLiquidForInstallment
                };

                await _IInstallmentTransactionRepository.Add(installment);
                transactionCard.InstallmentTransactions.Add(installment);
            }
        }
    }
}

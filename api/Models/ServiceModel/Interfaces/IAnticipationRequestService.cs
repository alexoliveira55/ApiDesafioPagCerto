using api.Models.EntityModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using static api.Utils.Utilities;

namespace api.Models.ServiceModel.Interfaces
{
    public interface IAnticipationRequestService
    {
        Task ApproveTransactionAntecipationRequest(TransactionAnticipationRequest transactionAnticipationRequest);
        Task DisapproveTransactionAntecipationRequest(TransactionAnticipationRequest transactionAnticipationRequest);
        Task<AnticipationRequest> RequestAnticipationTransactions(IEnumerable<long> nsuTransactions);
        Task<AnticipationRequest> GetRequestAnticipationTransactionById(long id);
        Task<TransactionAnticipationRequest> GetTransactionAnticipationRequestById(long id, long nsu);
        Task<IEnumerable<TransactionAnticipationRequest>> ConsultRequestByStatus(EnStatusAnticipationRequest enStatusAnticipationRequest);
        Task<bool> ExistsPendingRequestForTransactionCardNsu(long nsu);
        Task<bool> ExistsTransactionNonFinishedOnAnticipationRequest(long id);
        Task UpdateRequestAnticipation(AnticipationRequest anticipation);
        Task SetRequestAnticipationInAnalysis(AnticipationRequest anticipation);


    }
}

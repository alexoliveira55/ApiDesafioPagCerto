using api.Models.EntityModel;
using api.Models.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Models.ServiceModel.Interfaces
{
    public interface ITransactionCardService
    {
        Task<TransactionCard> GetTransactionWithInstallmentByNsu(long nsu);
        Task<TransactionCard> GetTransactionByNsu(long nsu);
        Task<List<TransactionCard>> GetTransactionsForAnticipation();
        Task<TransactionCard> MakePaymentWithCard(TransactionCardViewModel transactionViewModel);
        Task UpdateTransactionCard(TransactionCard transactionCard);
        Task UpdateInstallmentTransactionCard(InstallmentTransaction installment);
    }
}

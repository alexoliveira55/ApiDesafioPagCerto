using api.Models.EntityModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Repository.Interfaces
{
    public interface ITransactionCardRepository : IRepository<TransactionCard>
    {
        Task<TransactionCard> GetTransactionByNsuWithInstallments(long nsu);
        Task<TransactionCard> GetTransactionByNsu(long nsu);

    }
}

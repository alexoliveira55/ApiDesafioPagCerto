using api.Infrastructure.Context;
using api.Models.EntityModel;
using api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace api.Repository
{
    public class TransactionCardRepository : Repository<TransactionCard>, ITransactionCardRepository
    {
        public TransactionCardRepository(DbContextApi db) : base(db)
        {

        }
        public async Task<TransactionCard> GetTransactionByNsu(long nsu)
        {
            return await Db.TransactionCards.AsNoTracking()
                                            .FirstOrDefaultAsync(c => c.Nsu == nsu);
        }

        public async Task<TransactionCard> GetTransactionByNsuWithInstallments(long nsu)
        {
            return await Db.TransactionCards.AsNoTracking()
                                            .Include(t => t.InstallmentTransactions)
                                            .FirstOrDefaultAsync(c => c.Nsu == nsu);
        }
    }

}

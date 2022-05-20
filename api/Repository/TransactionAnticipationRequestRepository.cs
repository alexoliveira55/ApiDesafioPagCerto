using api.Infrastructure.Context;
using api.Models.EntityModel;
using api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace api.Repository
{
    public class TransactionAnticipationRequestRepository : Repository<TransactionAnticipationRequest>, ITransactionAnticipationRequestRepository
    {
        public TransactionAnticipationRequestRepository(DbContextApi db) : base(db)
        {

        }

        public async Task<IEnumerable<TransactionAnticipationRequest>> ConsultComplete(Expression<Func<TransactionAnticipationRequest, bool>> predicate)
        {
            return await Db.TransactionAnticipationRequests.AsNoTracking()
                                                           .Include(t=> t.TransactionCard)
                                                                .ThenInclude(t=> t.InstallmentTransactions)
                                                           .Include(t=> t.AnticipationRequest)
                                                           .Where(predicate).ToListAsync();
        }
    }

}

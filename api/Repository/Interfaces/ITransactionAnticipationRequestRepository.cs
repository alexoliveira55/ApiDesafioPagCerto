using api.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace api.Repository.Interfaces
{
    public interface ITransactionAnticipationRequestRepository : IRepository<TransactionAnticipationRequest>
    {
        Task<IEnumerable<TransactionAnticipationRequest>> ConsultComplete(Expression<Func<TransactionAnticipationRequest, bool>> predicate);
    }
}

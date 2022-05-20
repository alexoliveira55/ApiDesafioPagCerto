using api.Models.EntityModel;
using System.Threading.Tasks;

namespace api.Repository.Interfaces
{
    public interface IAnticipationRequestRepository : IRepository<AnticipationRequest>
    {
        Task<AnticipationRequest> GetRequestAnticipationTransactionById(long id);
    }
}

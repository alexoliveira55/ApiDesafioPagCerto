using api.Infrastructure.Context;
using api.Models.EntityModel;
using api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace api.Repository
{
    public class AnticipationRequestRepository : Repository<AnticipationRequest>, IAnticipationRequestRepository
    {
        public AnticipationRequestRepository(DbContextApi db) : base(db)
        {
        }

        public async Task<AnticipationRequest> GetRequestAnticipationTransactionById(long id)
        {
            return await Db.AnticipationRequests.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }

}

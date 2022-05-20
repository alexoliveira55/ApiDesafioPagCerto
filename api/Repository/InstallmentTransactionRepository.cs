using api.Infrastructure.Context;
using api.Models.EntityModel;
using api.Repository.Interfaces;

namespace api.Repository
{
    public class InstallmentTransactionRepository : Repository<InstallmentTransaction>, IInstallmentTransactionRepository
    {
        public InstallmentTransactionRepository(DbContextApi db) : base(db)
        {

        }
    }
}

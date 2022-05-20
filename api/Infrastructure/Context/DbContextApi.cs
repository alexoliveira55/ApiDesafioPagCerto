using api.Models.EntityModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace api.Infrastructure.Context
{
    public class DbContextApi : DbContext
    {
        public DbContextApi(DbContextOptions<DbContextApi> options) : base(options)
        {

        }

        public DbSet<TransactionCard> TransactionCards { get; set; }
        public DbSet<InstallmentTransaction> InstallmentTransactions { get; set; }
        public DbSet<AnticipationRequest> AnticipationRequests { get; set; }
        public DbSet<TransactionAnticipationRequest> TransactionAnticipationRequests { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContextApi).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            base.OnModelCreating(modelBuilder);
        }

    }
}

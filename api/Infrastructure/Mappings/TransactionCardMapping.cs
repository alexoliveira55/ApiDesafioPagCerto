using api.Models.EntityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Infrastructure.Mappings
{
    public class TransactionCardMapping : IEntityTypeConfiguration<TransactionCard>
    {
        public void Configure(EntityTypeBuilder<TransactionCard> builder)
        {
            builder.HasKey(p => p.Nsu);

            //1:N - TransactionCard - InstallmentTransaction
            builder.HasMany(t => t.InstallmentTransactions)
                .WithOne(c => c.TransactionCard)
                .HasForeignKey(c => c.NsuTransaction);

            //1:N - TransactionAnticipationRequest - TransactionCard
            builder.HasMany(t => t.TransactionAnticipationRequests)
                   .WithOne(c => c.TransactionCard)
                   .HasForeignKey(c => c.NsuTransction);

            builder.ToTable("TransactionCard");
        }
    }
}

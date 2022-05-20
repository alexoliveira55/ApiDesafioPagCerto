using api.Models.EntityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Infrastructure.Mappings
{
    public class TransactionAnticipationRequestMapping : IEntityTypeConfiguration<TransactionAnticipationRequest>
    {
        public void Configure(EntityTypeBuilder<TransactionAnticipationRequest> builder)
        {
            builder.HasKey(p => new { p.NsuTransction, p.IdAnticipationRequest });

            builder.ToTable("TransactionAnticipationRequest");
        }
    }
}

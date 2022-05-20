using api.Models.EntityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Infrastructure.Mappings
{
    public class AnticipationRequestMapping : IEntityTypeConfiguration<AnticipationRequest>
    {
        public void Configure(EntityTypeBuilder<AnticipationRequest> builder)
        {
            builder.HasKey(p => p.Id);
            //1:N - AnticipationRequest - TransactionAnticipationRequest
            builder.HasMany(t => t.TransactionAnticipationRequests)
                .WithOne(c => c.AnticipationRequest)
                .HasForeignKey(c => c.IdAnticipationRequest);

            builder.ToTable("AnticipationRequest");
        }
    }
}

using api.Models.EntityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Infrastructure.Mappings
{
    public class InstallmentTransactionMapping : IEntityTypeConfiguration<InstallmentTransaction>
    {
        public void Configure(EntityTypeBuilder<InstallmentTransaction> builder)
        {
            builder.HasKey(p => p.Id);

            builder.ToTable("InstallmentTransaction");
        }
    }
}

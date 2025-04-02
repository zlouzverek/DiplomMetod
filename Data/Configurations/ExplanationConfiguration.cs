using DiplomMetod.Data.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiplomMetod.Data.Configurations
{
    public class ExplanationConfiguration : IEntityTypeConfiguration<Explanation>
    {
        public void Configure(EntityTypeBuilder<Explanation> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ApproveLevel)
               .HasConversion(v => v.ToString(), v => (ApproveLevel)Enum.Parse(typeof(ApproveLevel), v));

            builder.HasOne(x => x.Organization)
                .WithMany(x => x.Explonations)
                .HasForeignKey(x => x.OrganizationId)
                .IsRequired();
                
        }
    }
}

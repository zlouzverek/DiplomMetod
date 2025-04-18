using DiplomMetod.Data.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DiplomMetod.Data.Configurations
{
    public class KeyWordConfiguration : IEntityTypeConfiguration<KeyWord>
    {
        public void Configure(EntityTypeBuilder<KeyWord> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired(); 

            builder.HasOne(x => x.Form)
                .WithMany(x => x.KeyWords)
                .HasForeignKey(x => x.FormId);
        }
    }
}

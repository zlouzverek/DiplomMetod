using DiplomMetod.Data.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiplomMetod.Data.Configurations
{
    public class FormConfiguration : IEntityTypeConfiguration<Form>
    {
        public void Configure(EntityTypeBuilder<Form> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.FormType)
                .WithMany(x => x.Forms)
                .HasForeignKey(x => x.FormTypeId)
                .IsRequired();

            builder.HasOne(x => x.ReferenceBook)
                .WithMany(x => x.Forms)
                .HasForeignKey(x => x.ReferenceBooksId)
                .IsRequired();

            builder.HasMany(x => x.KeyWords)
                .WithOne(x => x.Form)
                .HasForeignKey(x => x.FormId);

            builder.HasOne(x => x.Explanation)
                .WithOne(x => x.Form)
                .HasForeignKey<Explanation>(z => z.FormId);

            builder.HasOne(x => x.RegionsDivision)
                .WithMany(x => x.Forms)
                .HasForeignKey(x => x.RegionsDivisionsId);
        }

    }
}

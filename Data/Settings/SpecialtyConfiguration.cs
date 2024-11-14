using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace Data.Settings;

public class SpecialtyConfiguration : IEntityTypeConfiguration<Specialty>
{
    public void Configure(EntityTypeBuilder<Specialty> builder)
    {
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.NameSpecialty).IsRequired().HasMaxLength(60);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(100);
        builder.Property(x => x.State).IsRequired();
    }
}
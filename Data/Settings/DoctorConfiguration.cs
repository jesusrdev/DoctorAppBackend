using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace Data.Settings;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Lastname).IsRequired().HasMaxLength(60);
        builder.Property(x => x.Firstname).IsRequired().HasMaxLength(60);
        builder.Property(x => x.Direction).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Phone).IsRequired(false).HasMaxLength(40);
        builder.Property(x => x.Genre).IsRequired().HasMaxLength(1);
        builder.Property(x => x.State).IsRequired();
        builder.Property(x => x.SpecialtyId).IsRequired();
        
        
        //* Relations
        builder.HasOne(x => x.Specialty).WithMany()
            .HasForeignKey(x => x.SpecialtyId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
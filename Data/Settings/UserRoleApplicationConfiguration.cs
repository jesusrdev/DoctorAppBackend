using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entities;

namespace Data.Settings
{
    public class UserRoleApplicationConfiguration : IEntityTypeConfiguration<UserRoleApplication>
    {
        public void Configure(EntityTypeBuilder<UserRoleApplication> builder)
        {
            builder.HasKey(r => new { r.UserId, r.RoleId });

            // Relación con Usuario
            builder.HasOne(r => r.UserApplication)
                   .WithMany(u => u.UserRoles)
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.NoAction);

            // Relación con Rol
            builder.HasOne(r => r.RoleApplication)
                   .WithMany(r => r.UserRoles)
                   .HasForeignKey(r => r.RoleId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

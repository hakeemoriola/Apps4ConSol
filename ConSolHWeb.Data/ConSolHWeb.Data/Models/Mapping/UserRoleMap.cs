using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class UserRoleMap : EntityTypeConfiguration<UserRole>
    {
        public UserRoleMap()
        {
            // Primary Key
            this.HasKey(t => t.UserRoleName);

            // Properties
            this.Property(t => t.UserRoleName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.RoleDescription)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("UserRole");
            this.Property(t => t.UserRoleName).HasColumnName("UserRoleName");
            this.Property(t => t.RoleDescription).HasColumnName("RoleDescription");
        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class ModulesInRoleMap : EntityTypeConfiguration<ModulesInRole>
    {
        public ModulesInRoleMap()
        {
            // Primary Key
            this.HasKey(t => t.RoleName);

            // Properties
            this.Property(t => t.RoleName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ModulesInRole");
            this.Property(t => t.RoleName).HasColumnName("RoleName");
            this.Property(t => t.Modules).HasColumnName("Modules");
        }
    }
}

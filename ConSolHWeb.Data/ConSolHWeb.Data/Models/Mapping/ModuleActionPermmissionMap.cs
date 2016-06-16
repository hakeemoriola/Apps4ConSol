using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class ModuleActionPermmissionMap : EntityTypeConfiguration<ModuleActionPermmission>
    {
        public ModuleActionPermmissionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ModuleId)
                .HasMaxLength(50);

            this.Property(t => t.RoleName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ModuleActionPermmission");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ModuleId).HasColumnName("ModuleId");
            this.Property(t => t.ModuleActionId).HasColumnName("ModuleActionId");
            this.Property(t => t.MAPValue).HasColumnName("MAPValue");
            this.Property(t => t.RoleName).HasColumnName("RoleName");
        }
    }
}

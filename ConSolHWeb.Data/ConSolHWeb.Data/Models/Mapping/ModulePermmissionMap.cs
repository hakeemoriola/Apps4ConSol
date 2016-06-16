using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class ModulePermmissionMap : EntityTypeConfiguration<ModulePermmission>
    {
        public ModulePermmissionMap()
        {
            // Primary Key
            this.HasKey(t => new { t.RoleName, t.ModuleID, t.hasView, t.hasEdit, t.hasAdd, t.hasDelete });

            // Properties
            this.Property(t => t.RoleName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ModuleID)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.hasView)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.hasEdit)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.hasAdd)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.hasDelete)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ModulePermmission");
            this.Property(t => t.RoleName).HasColumnName("RoleName");
            this.Property(t => t.ModuleID).HasColumnName("ModuleID");
            this.Property(t => t.hasView).HasColumnName("hasView");
            this.Property(t => t.hasEdit).HasColumnName("hasEdit");
            this.Property(t => t.hasAdd).HasColumnName("hasAdd");
            this.Property(t => t.hasDelete).HasColumnName("hasDelete");
        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class ModulesActionMap : EntityTypeConfiguration<ModulesAction>
    {
        public ModulesActionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ModuleId)
                .HasMaxLength(50);

            this.Property(t => t.ModuleActionName)
                .HasMaxLength(50);

            this.Property(t => t.ModuleActionUrl)
                .HasMaxLength(80);

            // Table & Column Mappings
            this.ToTable("ModulesActions");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ModuleId).HasColumnName("ModuleId");
            this.Property(t => t.ModuleActionName).HasColumnName("ModuleActionName");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.CanAppearOnLink).HasColumnName("CanAppearOnLink");
            this.Property(t => t.ModuleActionUrl).HasColumnName("ModuleActionUrl");
        }
    }
}

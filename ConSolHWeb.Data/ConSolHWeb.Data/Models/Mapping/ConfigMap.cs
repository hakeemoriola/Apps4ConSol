using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class ConfigMap : EntityTypeConfiguration<Config>
    {
        public ConfigMap()
        {
            // Primary Key
            this.HasKey(t => new { t.RecID, t.ConfigName });

            // Properties
            this.Property(t => t.RecID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ConfigName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("Configs");
            this.Property(t => t.RecID).HasColumnName("RecID");
            this.Property(t => t.ConfigName).HasColumnName("ConfigName");
            this.Property(t => t.ConfigValue).HasColumnName("ConfigValue");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}

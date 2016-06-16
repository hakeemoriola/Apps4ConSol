using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class NgPrefixMap : EntityTypeConfiguration<NgPrefix>
    {
        public NgPrefixMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Prefix)
                .HasMaxLength(4);

            this.Property(t => t.Networks)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("NgPrefix");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Prefix).HasColumnName("Prefix");
            this.Property(t => t.Networks).HasColumnName("Networks");
        }
    }
}

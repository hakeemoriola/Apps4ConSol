using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class VxGoeZoneMap : EntityTypeConfiguration<VxGoeZone>
    {
        public VxGoeZoneMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ZoneName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("VxGoeZones");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ZoneName).HasColumnName("ZoneName");
        }
    }
}

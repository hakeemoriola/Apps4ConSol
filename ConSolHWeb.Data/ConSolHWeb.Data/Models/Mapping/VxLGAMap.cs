using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class VxLGAMap : EntityTypeConfiguration<VxLGA>
    {
        public VxLGAMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.LGA_Name)
                .HasMaxLength(70);

            // Table & Column Mappings
            this.ToTable("VxLGAS");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.LGA_Name).HasColumnName("LGA_Name");
            this.Property(t => t.StateId).HasColumnName("StateId");
        }
    }
}

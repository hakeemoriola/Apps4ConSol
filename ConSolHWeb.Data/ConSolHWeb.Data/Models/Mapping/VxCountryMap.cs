using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class VxCountryMap : EntityTypeConfiguration<VxCountry>
    {
        public VxCountryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.CountryName)
                .HasMaxLength(70);

            // Table & Column Mappings
            this.ToTable("VxCountry");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CountryName).HasColumnName("CountryName");
        }
    }
}

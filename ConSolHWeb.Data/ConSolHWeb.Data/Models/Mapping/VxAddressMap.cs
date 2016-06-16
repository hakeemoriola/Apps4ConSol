using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class VxAddressMap : EntityTypeConfiguration<VxAddress>
    {
        public VxAddressMap()
        {
            // Primary Key
            this.HasKey(t => t.PhoneNo);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.PhoneNo)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.State)
                .HasMaxLength(50);

            this.Property(t => t.State1)
                .HasMaxLength(50);

            this.Property(t => t.City)
                .HasMaxLength(50);

            this.Property(t => t.Town)
                .HasMaxLength(50);

            this.Property(t => t.LGA)
                .HasMaxLength(50);

            this.Property(t => t.Area)
                .HasMaxLength(50);

            this.Property(t => t.Location)
                .HasMaxLength(50);

            this.Property(t => t.Region)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("VxAddress");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PhoneNo).HasColumnName("PhoneNo");
            this.Property(t => t.FullAddress).HasColumnName("FullAddress");
            this.Property(t => t.AddrSet).HasColumnName("AddrSet");
            this.Property(t => t.State).HasColumnName("State");
            this.Property(t => t.State1).HasColumnName("State1");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.Town).HasColumnName("Town");
            this.Property(t => t.LGA).HasColumnName("LGA");
            this.Property(t => t.Area).HasColumnName("Area");
            this.Property(t => t.Location).HasColumnName("Location");
            this.Property(t => t.Region).HasColumnName("Region");
        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class VxProductMap : EntityTypeConfiguration<VxProduct>
    {
        public VxProductMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.PhoneNo)
                .HasMaxLength(50);

            this.Property(t => t.Product)
                .HasMaxLength(50);

            this.Property(t => t.SKU)
                .HasMaxLength(50);

            this.Property(t => t.SmartCardId)
                .HasMaxLength(50);

            this.Property(t => t.BatchNo)
                .HasMaxLength(50);

            this.Property(t => t.Brand)
                .HasMaxLength(50);

            this.Property(t => t.MeterNo)
                .HasMaxLength(50);

            this.Property(t => t.AccountNo)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("VxProducts");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PhoneNo).HasColumnName("PhoneNo");
            this.Property(t => t.Product).HasColumnName("Product");
            this.Property(t => t.SKU).HasColumnName("SKU");
            this.Property(t => t.SmartCardId).HasColumnName("SmartCardId");
            this.Property(t => t.BatchNo).HasColumnName("BatchNo");
            this.Property(t => t.Brand).HasColumnName("Brand");
            this.Property(t => t.MeterNo).HasColumnName("MeterNo");
            this.Property(t => t.AccountNo).HasColumnName("AccountNo");
        }
    }
}

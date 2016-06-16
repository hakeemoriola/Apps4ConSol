using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class FAQResourceMap : EntityTypeConfiguration<FAQResource>
    {
        public FAQResourceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.FileName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("FAQResource");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.FaqId).HasColumnName("FaqId");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.FileTypeId).HasColumnName("FileTypeId");
        }
    }
}

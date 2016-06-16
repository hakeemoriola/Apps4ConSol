using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class FAQMap : EntityTypeConfiguration<FAQ>
    {
        public FAQMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("FAQ");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.FaqQuestion).HasColumnName("FaqQuestion");
            this.Property(t => t.FaqAnswer).HasColumnName("FaqAnswer");
            this.Property(t => t.Active).HasColumnName("Active");
        }
    }
}

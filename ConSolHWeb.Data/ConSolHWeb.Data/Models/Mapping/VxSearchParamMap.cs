using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class VxSearchParamMap : EntityTypeConfiguration<VxSearchParam>
    {
        public VxSearchParamMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.pName)
                .HasMaxLength(50);

            this.Property(t => t.pControl)
                .HasMaxLength(50);

            this.Property(t => t.pDecision)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("VxSearchParams");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.pName).HasColumnName("pName");
            this.Property(t => t.pSingOrMutiple).HasColumnName("pSingOrMutiple");
            this.Property(t => t.pValue).HasColumnName("pValue");
            this.Property(t => t.pControl).HasColumnName("pControl");
            this.Property(t => t.pOptions).HasColumnName("pOptions");
            this.Property(t => t.pDecision).HasColumnName("pDecision");
        }
    }
}

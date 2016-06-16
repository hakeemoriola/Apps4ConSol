using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class BaseMetaColumnMap : EntityTypeConfiguration<BaseMetaColumn>
    {
        public BaseMetaColumnMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.BaseColumnName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("BaseMetaColumns");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.BaseColumnName).HasColumnName("BaseColumnName");
        }
    }
}

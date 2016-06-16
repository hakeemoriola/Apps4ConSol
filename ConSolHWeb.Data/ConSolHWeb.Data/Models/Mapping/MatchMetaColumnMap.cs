using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class MatchMetaColumnMap : EntityTypeConfiguration<MatchMetaColumn>
    {
        public MatchMetaColumnMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.DBColumn)
                .HasMaxLength(50);

            this.Property(t => t.BaseDbColumn)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("MatchMetaColumns");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.DbId).HasColumnName("DbId");
            this.Property(t => t.TableId).HasColumnName("TableId");
            this.Property(t => t.DBColumn).HasColumnName("DBColumn");
            this.Property(t => t.BaseDbColumn).HasColumnName("BaseDbColumn");
            this.Property(t => t.IsDColumn).HasColumnName("IsDColumn");
        }
    }
}

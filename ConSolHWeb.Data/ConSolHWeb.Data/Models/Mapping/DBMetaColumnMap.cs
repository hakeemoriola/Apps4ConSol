using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class DBMetaColumnMap : EntityTypeConfiguration<DBMetaColumn>
    {
        public DBMetaColumnMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ColumnName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DBMetaColumns");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.DbId).HasColumnName("DbId");
            this.Property(t => t.TableId).HasColumnName("TableId");
            this.Property(t => t.ColumnName).HasColumnName("ColumnName");
        }
    }
}

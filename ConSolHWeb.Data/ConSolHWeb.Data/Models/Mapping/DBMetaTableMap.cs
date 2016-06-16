using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class DBMetaTableMap : EntityTypeConfiguration<DBMetaTable>
    {
        public DBMetaTableMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.TableName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DBMetaTables");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.DBId).HasColumnName("DBId");
            this.Property(t => t.TableName).HasColumnName("TableName");
        }
    }
}

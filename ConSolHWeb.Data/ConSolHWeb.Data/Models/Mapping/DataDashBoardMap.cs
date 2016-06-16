using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class DataDashBoardMap : EntityTypeConfiguration<DataDashBoard>
    {
        public DataDashBoardMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("DataDashBoard");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.DbId).HasColumnName("DbId");
            this.Property(t => t.TableId).HasColumnName("TableId");
            this.Property(t => t.TotalRecordCount).HasColumnName("TotalRecordCount");
            this.Property(t => t.TotalUniqueCount).HasColumnName("TotalUniqueCount");
        }
    }
}

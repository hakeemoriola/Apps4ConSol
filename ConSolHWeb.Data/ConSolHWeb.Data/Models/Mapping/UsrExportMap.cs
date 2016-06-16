using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class UsrExportMap : EntityTypeConfiguration<UsrExport>
    {
        public UsrExportMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.FilePath)
                .HasMaxLength(250);

            this.Property(t => t.FileUrl)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("UsrExports");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.FileTypeId).HasColumnName("FileTypeId");
            this.Property(t => t.FilePath).HasColumnName("FilePath");
            this.Property(t => t.FileUrl).HasColumnName("FileUrl");
            this.Property(t => t.DateCreated).HasColumnName("DateCreated");
        }
    }
}

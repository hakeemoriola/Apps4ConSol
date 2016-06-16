using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class VxDbDataSourceMap : EntityTypeConfiguration<VxDbDataSource>
    {
        public VxDbDataSourceMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Id, t.PhoneNo });

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.PhoneNo)
                .IsRequired()
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("VxDbDataSource");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PhoneNo).HasColumnName("PhoneNo");
            this.Property(t => t.DbSource).HasColumnName("DbSource");

            // Relationships
            this.HasRequired(t => t.VxTelephone)
                .WithMany(t => t.VxDbDataSources)
                .HasForeignKey(d => d.PhoneNo);

        }
    }
}

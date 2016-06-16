using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class ConSettingMap : EntityTypeConfiguration<ConSetting>
    {
        public ConSettingMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ProjectName)
                .HasMaxLength(50);

            this.Property(t => t.ServerIP)
                .HasMaxLength(50);

            this.Property(t => t.Username)
                .HasMaxLength(50);

            this.Property(t => t.Password)
                .HasMaxLength(200);

            this.Property(t => t.Database)
                .HasMaxLength(50);

            this.Property(t => t.Port)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ConSettings");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ProjectName).HasColumnName("ProjectName");
            this.Property(t => t.ServerIP).HasColumnName("ServerIP");
            this.Property(t => t.Username).HasColumnName("Username");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.Database).HasColumnName("Database");
            this.Property(t => t.Port).HasColumnName("Port");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}

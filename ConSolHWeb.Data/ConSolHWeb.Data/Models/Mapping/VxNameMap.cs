using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class VxNameMap : EntityTypeConfiguration<VxName>
    {
        public VxNameMap()
        {
            // Primary Key
            this.HasKey(t => t.PhoneNo);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.PhoneNo)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.Title)
                .HasMaxLength(10);

            this.Property(t => t.Firstname)
                .HasMaxLength(50);

            this.Property(t => t.Middlename)
                .HasMaxLength(50);

            this.Property(t => t.Surname)
                .HasMaxLength(50);

            this.Property(t => t.fullName)
                .HasMaxLength(150);

            this.Property(t => t.Gender)
                .HasMaxLength(6);

            // Table & Column Mappings
            this.ToTable("VxNames");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PhoneNo).HasColumnName("PhoneNo");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Firstname).HasColumnName("Firstname");
            this.Property(t => t.Middlename).HasColumnName("Middlename");
            this.Property(t => t.Surname).HasColumnName("Surname");
            this.Property(t => t.fullName).HasColumnName("fullName");
            this.Property(t => t.Gender).HasColumnName("Gender");
        }
    }
}

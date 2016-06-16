using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.UserId);

            // Properties
            this.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.Password)
                .HasMaxLength(150);

            this.Property(t => t.Title)
                .HasMaxLength(10);

            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.MiddleName)
                .HasMaxLength(50);

            this.Property(t => t.DateOfBirth)
                .HasMaxLength(15);

            this.Property(t => t.Gender)
                .HasMaxLength(6);

            this.Property(t => t.Text_Pass)
                .HasMaxLength(150);

            this.Property(t => t.SecurityAnswer)
                .HasMaxLength(50);

            this.Property(t => t.PhoneNo)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Users");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.MiddleName).HasColumnName("MiddleName");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.DateOfBirth).HasColumnName("DateOfBirth");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.DateCreated).HasColumnName("DateCreated");
            this.Property(t => t.tempData).HasColumnName("tempData");
            this.Property(t => t.KeyData).HasColumnName("KeyData");
            this.Property(t => t.Activated).HasColumnName("Activated");
            this.Property(t => t.Invaildated).HasColumnName("Invaildated");
            this.Property(t => t.Text_Pass).HasColumnName("Text_Pass");
            this.Property(t => t.Imported_data).HasColumnName("Imported_data");
            this.Property(t => t.SecurityQuestion).HasColumnName("SecurityQuestion");
            this.Property(t => t.SecurityAnswer).HasColumnName("SecurityAnswer");
            this.Property(t => t.PhoneNo).HasColumnName("PhoneNo");
        }
    }
}

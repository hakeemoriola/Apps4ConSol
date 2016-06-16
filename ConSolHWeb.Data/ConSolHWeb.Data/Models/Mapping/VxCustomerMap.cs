using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class VxCustomerMap : EntityTypeConfiguration<VxCustomer>
    {
        public VxCustomerMap()
        {
            // Primary Key
            this.HasKey(t => t.PhoneNo);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.PhoneNo)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.CustomerType)
                .HasMaxLength(50);

            this.Property(t => t.CustomerNo)
                .HasMaxLength(50);

            this.Property(t => t.CallerProfile)
                .HasMaxLength(50);

            this.Property(t => t.DateOfBirth)
                .HasMaxLength(50);

            this.Property(t => t.Age)
                .HasMaxLength(50);

            this.Property(t => t.Occupation)
                .HasMaxLength(50);

            this.Property(t => t.Industry)
                .HasMaxLength(50);

            this.Property(t => t.EmploymentStatus)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("VxCustomers");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PhoneNo).HasColumnName("PhoneNo");
            this.Property(t => t.CustomerType).HasColumnName("CustomerType");
            this.Property(t => t.CustomerNo).HasColumnName("CustomerNo");
            this.Property(t => t.CallerProfile).HasColumnName("CallerProfile");
            this.Property(t => t.DateOfBirth).HasColumnName("DateOfBirth");
            this.Property(t => t.Age).HasColumnName("Age");
            this.Property(t => t.Occupation).HasColumnName("Occupation");
            this.Property(t => t.Industry).HasColumnName("Industry");
            this.Property(t => t.EmploymentStatus).HasColumnName("EmploymentStatus");

            // Relationships
            this.HasRequired(t => t.VxTelephone)
                .WithOptional(t => t.VxCustomer);

        }
    }
}

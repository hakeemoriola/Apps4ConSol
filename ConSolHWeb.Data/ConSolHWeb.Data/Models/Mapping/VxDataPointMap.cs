using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class VxDataPointMap : EntityTypeConfiguration<VxDataPoint>
    {
        public VxDataPointMap()
        {
            // Primary Key
            this.HasKey(t => t.MOBILENUMBER2);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(10);

            this.Property(t => t.Surname)
                .HasMaxLength(50);

            this.Property(t => t.Firstname)
                .HasMaxLength(50);

            this.Property(t => t.Middlename)
                .HasMaxLength(50);

            this.Property(t => t.NAME)
                .HasMaxLength(150);

            this.Property(t => t.Gender)
                .HasMaxLength(6);

            this.Property(t => t.Age)
                .HasMaxLength(50);

            this.Property(t => t.Town)
                .HasMaxLength(50);

            this.Property(t => t.MOBILENUMBER1)
                .HasMaxLength(15);

            this.Property(t => t.MOBILENUMBER2)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.Occupation)
                .HasMaxLength(50);

            this.Property(t => t.JOBSTATUS)
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .HasMaxLength(150);

            this.Property(t => t.Industry)
                .HasMaxLength(50);

            this.Property(t => t.STATE)
                .HasMaxLength(50);

            this.Property(t => t.LGA)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("VxDataPoints");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Surname).HasColumnName("Surname");
            this.Property(t => t.Firstname).HasColumnName("Firstname");
            this.Property(t => t.Middlename).HasColumnName("Middlename");
            this.Property(t => t.NAME).HasColumnName("NAME");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.Age).HasColumnName("Age");
            this.Property(t => t.ADDRESS).HasColumnName("ADDRESS");
            this.Property(t => t.AddrSet).HasColumnName("AddrSet");
            this.Property(t => t.Town).HasColumnName("Town");
            this.Property(t => t.MOBILENUMBER1).HasColumnName("MOBILENUMBER1");
            this.Property(t => t.MOBILENUMBER2).HasColumnName("MOBILENUMBER2");
            this.Property(t => t.Occupation).HasColumnName("Occupation");
            this.Property(t => t.JOBSTATUS).HasColumnName("JOBSTATUS");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Industry).HasColumnName("Industry");
            this.Property(t => t.STATE).HasColumnName("STATE");
            this.Property(t => t.LGA).HasColumnName("LGA");
            this.Property(t => t.Source).HasColumnName("Source");
        }
    }
}

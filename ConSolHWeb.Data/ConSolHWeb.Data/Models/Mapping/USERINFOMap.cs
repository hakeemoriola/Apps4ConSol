using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class USERINFOMap : EntityTypeConfiguration<USERINFO>
    {
        public USERINFOMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Badgenumber)
                .HasMaxLength(24);

            this.Property(t => t.SSN)
                .HasMaxLength(20);

            this.Property(t => t.Name)
                .HasMaxLength(40);

            this.Property(t => t.Gender)
                .HasMaxLength(8);

            // Table & Column Mappings
            this.ToTable("USERINFO");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.USERID).HasColumnName("USERID");
            this.Property(t => t.Badgenumber).HasColumnName("Badgenumber");
            this.Property(t => t.SSN).HasColumnName("SSN");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.DEFAULTDEPTID).HasColumnName("DEFAULTDEPTID");
            this.Property(t => t.InheritDeptSch).HasColumnName("InheritDeptSch");
            this.Property(t => t.InheritDeptSchClass).HasColumnName("InheritDeptSchClass");
            this.Property(t => t.AutoSchPlan).HasColumnName("AutoSchPlan");
            this.Property(t => t.Processed).HasColumnName("Processed");
        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class VxEmailMap : EntityTypeConfiguration<VxEmail>
    {
        public VxEmailMap()
        {
            // Primary Key
            this.HasKey(t => t.PhoneNo);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.PhoneNo)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.Email)
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("VxEmails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PhoneNo).HasColumnName("PhoneNo");
            this.Property(t => t.Email).HasColumnName("Email");

            // Relationships
            this.HasRequired(t => t.VxTelephone)
                .WithOptional(t => t.VxEmail);

        }
    }
}

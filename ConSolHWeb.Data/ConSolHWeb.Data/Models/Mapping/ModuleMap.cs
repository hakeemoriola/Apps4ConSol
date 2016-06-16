using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class ModuleMap : EntityTypeConfiguration<Module>
    {
        public ModuleMap()
        {
            // Primary Key
            this.HasKey(t => t.RecID);

            // Properties
            this.Property(t => t.ModuleID)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.ModuleName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ModuleImage)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.ModuleDescription)
                .IsRequired();

            this.Property(t => t.Group_Name)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Module");
            this.Property(t => t.RecID).HasColumnName("RecID");
            this.Property(t => t.ModuleID).HasColumnName("ModuleID");
            this.Property(t => t.ModuleName).HasColumnName("ModuleName");
            this.Property(t => t.ModuleImage).HasColumnName("ModuleImage");
            this.Property(t => t.ModuleDescription).HasColumnName("ModuleDescription");
            this.Property(t => t.active).HasColumnName("active");
            this.Property(t => t.Data_Dependent).HasColumnName("Data_Dependent");
            this.Property(t => t.M_Order).HasColumnName("M_Order");
            this.Property(t => t.Is_Admin).HasColumnName("Is_Admin");
            this.Property(t => t.Group_Name).HasColumnName("Group_Name");
        }
    }
}

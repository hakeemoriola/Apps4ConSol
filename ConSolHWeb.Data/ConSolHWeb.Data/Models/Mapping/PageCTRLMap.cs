using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class PageCTRLMap : EntityTypeConfiguration<PageCTRL>
    {
        public PageCTRLMap()
        {
            // Primary Key
            this.HasKey(t => t.RecId);

            // Properties
            this.Property(t => t.RecId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ControlName)
                .HasMaxLength(50);

            this.Property(t => t.ControlPath)
                .HasMaxLength(255);

            this.Property(t => t.Param1)
                .HasMaxLength(50);

            this.Property(t => t.Param2)
                .HasMaxLength(50);

            this.Property(t => t.Param3)
                .HasMaxLength(50);

            this.Property(t => t.Param4)
                .HasMaxLength(50);

            this.Property(t => t.SelectionFormula1)
                .HasMaxLength(200);

            this.Property(t => t.SelectionFormula2)
                .HasMaxLength(200);

            this.Property(t => t.SelectionFormula3)
                .HasMaxLength(200);

            this.Property(t => t.FromValueSelectionFormula)
                .HasMaxLength(200);

            this.Property(t => t.ToValueSelectionFormula)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("PageCTRLs");
            this.Property(t => t.RecId).HasColumnName("RecId");
            this.Property(t => t.ControlName).HasColumnName("ControlName");
            this.Property(t => t.ControlPath).HasColumnName("ControlPath");
            this.Property(t => t.Param1).HasColumnName("Param1");
            this.Property(t => t.Param2).HasColumnName("Param2");
            this.Property(t => t.Param3).HasColumnName("Param3");
            this.Property(t => t.Param4).HasColumnName("Param4");
            this.Property(t => t.SFCount).HasColumnName("SFCount");
            this.Property(t => t.SelectionFormula1).HasColumnName("SelectionFormula1");
            this.Property(t => t.SelectionFormula2).HasColumnName("SelectionFormula2");
            this.Property(t => t.SelectionFormula3).HasColumnName("SelectionFormula3");
            this.Property(t => t.IsRange).HasColumnName("IsRange");
            this.Property(t => t.FromValueSelectionFormula).HasColumnName("FromValueSelectionFormula");
            this.Property(t => t.ToValueSelectionFormula).HasColumnName("ToValueSelectionFormula");
            this.Property(t => t.MetaTitle).HasColumnName("MetaTitle");
            this.Property(t => t.MetaKeywords).HasColumnName("MetaKeywords");
            this.Property(t => t.MetaDescription).HasColumnName("MetaDescription");
        }
    }
}

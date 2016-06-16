using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class DataCountDashMap : EntityTypeConfiguration<DataCountDash>
    {
        public DataCountDashMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("DataCountDash");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PhoneNoCount).HasColumnName("PhoneNoCount");
            this.Property(t => t.NAMECount).HasColumnName("NAMECount");
            this.Property(t => t.GENDERcount).HasColumnName("GENDERcount");
            this.Property(t => t.AGECount).HasColumnName("AGECount");
            this.Property(t => t.ADDRESSCount).HasColumnName("ADDRESSCount");
            this.Property(t => t.TOWNCount).HasColumnName("TOWNCount");
            this.Property(t => t.OCCUPATIONCount).HasColumnName("OCCUPATIONCount");
            this.Property(t => t.JOBSTATUSCount).HasColumnName("JOBSTATUSCount");
            this.Property(t => t.EMAILCount).HasColumnName("EMAILCount");
            this.Property(t => t.INDUSTRYCount).HasColumnName("INDUSTRYCount");
            this.Property(t => t.STATECount).HasColumnName("STATECount");
            this.Property(t => t.LGACount).HasColumnName("LGACount");
        }
    }
}

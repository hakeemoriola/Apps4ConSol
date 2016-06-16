using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConSolHWeb.Data.Models.Mapping
{
    public class PageMap : EntityTypeConfiguration<Page>
    {
        public PageMap()
        {
            // Primary Key
            this.HasKey(t => t.page_id);

            // Properties
            this.Property(t => t.ControlName)
                .HasMaxLength(50);

            this.Property(t => t.ControlPath)
                .HasMaxLength(255);

            this.Property(t => t.title)
                .HasMaxLength(255);

            this.Property(t => t.nav_title)
                .HasMaxLength(255);

            this.Property(t => t.slug)
                .HasMaxLength(255);

            this.Property(t => t.uri)
                .HasMaxLength(255);

            this.Property(t => t.meta_title)
                .HasMaxLength(255);

            this.Property(t => t.status)
                .HasMaxLength(10);

            this.Property(t => t.template)
                .HasMaxLength(255);

            this.Property(t => t.sub_template)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("Pages");
            this.Property(t => t.page_id).HasColumnName("page_id");
            this.Property(t => t.ControlName).HasColumnName("ControlName");
            this.Property(t => t.ControlPath).HasColumnName("ControlPath");
            this.Property(t => t.Pagetype).HasColumnName("Pagetype");
            this.Property(t => t.is_home).HasColumnName("is_home");
            this.Property(t => t.title).HasColumnName("title");
            this.Property(t => t.nav_title).HasColumnName("nav_title");
            this.Property(t => t.slug).HasColumnName("slug");
            this.Property(t => t.uri).HasColumnName("uri");
            this.Property(t => t.parent_id).HasColumnName("parent_id");
            this.Property(t => t.meta_title).HasColumnName("meta_title");
            this.Property(t => t.meta_keywords).HasColumnName("meta_keywords");
            this.Property(t => t.meta_description).HasColumnName("meta_description");
            this.Property(t => t.banner).HasColumnName("banner");
            this.Property(t => t.css).HasColumnName("css");
            this.Property(t => t.js).HasColumnName("js");
            this.Property(t => t.status).HasColumnName("status");
            this.Property(t => t.orderrank).HasColumnName("orderrank");
            this.Property(t => t.template).HasColumnName("template");
            this.Property(t => t.sub_template).HasColumnName("sub_template");
            this.Property(t => t.additionals).HasColumnName("additionals");
            this.Property(t => t.widgets).HasColumnName("widgets");
            this.Property(t => t.in_main_nav).HasColumnName("in_main_nav");
            this.Property(t => t.in_footer_nav).HasColumnName("in_footer_nav");
            this.Property(t => t.in_bottom_nav).HasColumnName("in_bottom_nav");
            this.Property(t => t.body).HasColumnName("body");
            this.Property(t => t.updated_at).HasColumnName("updated_at");
            this.Property(t => t.created_at).HasColumnName("created_at");
        }
    }
}

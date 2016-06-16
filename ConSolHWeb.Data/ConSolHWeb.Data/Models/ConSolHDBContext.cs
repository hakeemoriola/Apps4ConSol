using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ConSolHWeb.Data.Models.Mapping;

namespace ConSolHWeb.Data.Models
{
    public partial class ConSolHDBContext : DbContext
    {
        static ConSolHDBContext()
        {
            Database.SetInitializer<ConSolHDBContext>(null);
        }

        public ConSolHDBContext()
            : base("Name=ConSolHDBContext")
        {
        }

        public DbSet<BaseMetaColumn> BaseMetaColumns { get; set; }
        public DbSet<Config> Configs { get; set; }
        public DbSet<ConSetting> ConSettings { get; set; }
        public DbSet<DataCountDash> DataCountDashes { get; set; }
        public DbSet<DataDashBoard> DataDashBoards { get; set; }
        public DbSet<DBMetaColumn> DBMetaColumns { get; set; }
        public DbSet<DBMetaTable> DBMetaTables { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<FAQResource> FAQResources { get; set; }
        public DbSet<FileType> FileTypes { get; set; }
        public DbSet<MatchMetaColumn> MatchMetaColumns { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<ModuleActionPermmission> ModuleActionPermmissions { get; set; }
        public DbSet<ModulePermmission> ModulePermmissions { get; set; }
        public DbSet<ModulesAction> ModulesActions { get; set; }
        public DbSet<ModulesInRole> ModulesInRoles { get; set; }
        public DbSet<NgPrefix> NgPrefixes { get; set; }
        public DbSet<PageCTRL> PageCTRLs { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<USERINFO> USERINFOes { get; set; }
        public DbSet<UserInRole> UserInRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UsrExport> UsrExports { get; set; }
        public DbSet<VxAddress> VxAddresses { get; set; }
        public DbSet<VxCountry> VxCountries { get; set; }
        public DbSet<VxCustomer> VxCustomers { get; set; }
        public DbSet<VxDbDataSource> VxDbDataSources { get; set; }
        public DbSet<VxEmail> VxEmails { get; set; }
        public DbSet<VxGoeZone> VxGoeZones { get; set; }
        public DbSet<VxLGA> VxLGAS { get; set; }
        public DbSet<VxName> VxNames { get; set; }
        public DbSet<VxProduct> VxProducts { get; set; }
        public DbSet<VxSearchParam> VxSearchParams { get; set; }
        public DbSet<VxTelephone> VxTelephones { get; set; }
        public DbSet<VxDataPoint> VxDataPoints { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BaseMetaColumnMap());
            modelBuilder.Configurations.Add(new ConfigMap());
            modelBuilder.Configurations.Add(new ConSettingMap());
            modelBuilder.Configurations.Add(new DataCountDashMap());
            modelBuilder.Configurations.Add(new DataDashBoardMap());
            modelBuilder.Configurations.Add(new DBMetaColumnMap());
            modelBuilder.Configurations.Add(new DBMetaTableMap());
            modelBuilder.Configurations.Add(new FAQMap());
            modelBuilder.Configurations.Add(new FAQResourceMap());
            modelBuilder.Configurations.Add(new FileTypeMap());
            modelBuilder.Configurations.Add(new MatchMetaColumnMap());
            modelBuilder.Configurations.Add(new ModuleMap());
            modelBuilder.Configurations.Add(new ModuleActionPermmissionMap());
            modelBuilder.Configurations.Add(new ModulePermmissionMap());
            modelBuilder.Configurations.Add(new ModulesActionMap());
            modelBuilder.Configurations.Add(new ModulesInRoleMap());
            modelBuilder.Configurations.Add(new NgPrefixMap());
            modelBuilder.Configurations.Add(new PageCTRLMap());
            modelBuilder.Configurations.Add(new PageMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new USERINFOMap());
            modelBuilder.Configurations.Add(new UserInRoleMap());
            modelBuilder.Configurations.Add(new UserRoleMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UsrExportMap());
            modelBuilder.Configurations.Add(new VxAddressMap());
            modelBuilder.Configurations.Add(new VxCountryMap());
            modelBuilder.Configurations.Add(new VxCustomerMap());
            modelBuilder.Configurations.Add(new VxDbDataSourceMap());
            modelBuilder.Configurations.Add(new VxEmailMap());
            modelBuilder.Configurations.Add(new VxGoeZoneMap());
            modelBuilder.Configurations.Add(new VxLGAMap());
            modelBuilder.Configurations.Add(new VxNameMap());
            modelBuilder.Configurations.Add(new VxProductMap());
            modelBuilder.Configurations.Add(new VxSearchParamMap());
            modelBuilder.Configurations.Add(new VxTelephoneMap());
            modelBuilder.Configurations.Add(new VxDataPointMap());
        }
    }
}

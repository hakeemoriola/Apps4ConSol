using ConSolHWeb.Data.DataProviders;
using ConSolHWeb.Data.Models;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Text;
using System.Web.UI.WebControls;

namespace ConSolHWeb.Data
{
    /// <summary>
    /// Summary description for DataProvider
    /// </summary>
    public abstract class DataProvider : ProviderBase
    {
        public abstract int AddBaseMetaColumn(BaseMetaColumn curr);

        public abstract DataCount GetDataCount();

        public abstract List<RawDataDashBoard> GetRawDataDashBoard();

        public abstract string GetHarmonisedCount();

        public abstract List<Stuff> GetJustDabase();

        public abstract int AddConSetting(ConSetting p);

        public abstract List<Stuff> GetJustDbColumns();

        public abstract List<BaseMetaColumn> GetAllBaseDbColumns();

        public abstract int AddDBMetaColumn(DBMetaColumn p);

        public abstract int AddDBMetaTable(DBMetaTable p);

        public abstract int AddMatchMetaColumn(MatchMetaColumn curr);

        public abstract int AddModuleInfo(Module moduleinfo);

        public abstract List<Stuff> GetJustDbColumns(string tableName, int dbId);

        public abstract int ExecuteSQL(string sQL);

        public abstract MatchMetaColumn GetMatchMetaColumnPicked(int dBId);

        public abstract int AddModulePermissions(List<ModulePermmission> modulepermissions, string RoleName);

        public abstract int AddModulePermissions(List<ModulePermmission> permlist, string p, List<ModulesAction> actionlist);

        public abstract int AddModulesInRole(string roleName, string modules);

        public abstract int AddPage(Page page);

        public abstract int AddUserDetail(Models.User user);

        public abstract List<DBMetaColumn> GetDBMetaColumnByDbId(int dbId);

        public abstract int AddUserRole(UserRole userole);

        public abstract bool CheckColumnExits(string columnName, int tableId, int dbId);

        public abstract int UpdateUserDetailWithoutPassword(User usr);

        public abstract int UpdateUserDetailWithoutPasswordByAdmin(User usr);

        public abstract bool CheckIfEmailExists(string email);

        public abstract List<MatchMetaColumn> CheckMatchMetaColumns(string tableColumn, string baseColumn, int tbaleId, int dbID);

        public abstract int SetIsDColumn(int dbId, string columnName);

        public abstract int UpdateUserDetailByAdmin(User usr);

        public abstract int DeleteModuleInfo(string moduleid);

        public abstract string GetDBTableByDBId(string dbId);

        public abstract DBMetaTable GetDBMetaTableByDbId(int dbId);

        public abstract int DeleteUserDetail(string username);

        public abstract int DeleteUserRole(string userrolename);

        public abstract object GetAllConSettings();

        public abstract long GetRecordCountPerDBByDbId(int v, string tablename);

        public abstract List<ConSetting> GetAllConSettings2();

        public abstract List<EmployeeDetail2> GetAllEmployees();

        public abstract List<EmployeeDetail2> GetAllEmployees(int BatchId);

        public abstract List<EmployeeDetail2> GetAllEmployeesToFix();

        public abstract List<ModulesAction> getAllModuleActionsPermmitedByRole(string p);

        public abstract List<Module> getAllModules();

        public abstract int SaveMatchMetaColumn(List<MatchMetaColumn> list);

        public abstract List<ModulesAction> getAllModulesActionPermmitedByRole(string p);

        public abstract List<Module> getAllModulesByRole(string p);

        public abstract int SaveDBMetaTable(DBMetaTable mt);

        public abstract List<Page> GetAllPages();

        public abstract List<USERINFO> GetAllUnprocessedEmployees();

        //Users
        public abstract List<Models.User> getAllUserDetails();

        //Roles
        public abstract List<UserRole> getAllUserRoles();

        public abstract BaseMetaColumn GetBaseMetaColumnById(int p);

        public abstract List<RawData> GetPrepHarmonizedData();

        public abstract List<DBMetaColumn> GetColumnsByTableId(int table);

        public abstract List<ColumnName> GetColumnsByTableUsingConsetting(ConSetting db, string tablename);

        public abstract Hashtable GetConfig();

        public abstract Config GetConfigDetailByConfigID(int p);

        public abstract long GetUniqueRecordCountPerDBByDbId(int v, string ColumnName, string tableName);

        public abstract int SaveDBMetaColumn(List<DBMetaColumn> list, int dbId);

        public abstract ConSetting GetConSettingById(int id);

        public abstract List<DBMetaTable> GetDBMetaTableByDBId(int v);

        public abstract Hashtable getEditControls();

        public abstract USERINFO GetEmployeeDetailByID(int uid);

        public abstract Hashtable getFriendlyNames();

        public abstract bool GetIfCurUserAllowed();

        public abstract Hashtable getListControl2s();

        public abstract Hashtable getListControls();

        public abstract List<MatchMetaColumn> GetMatchMetaColumnsByDbId(int p);

        public abstract List<ModulesAction> GetModuleActionsByModules(string modules);

        public abstract Module getModuleInfoByModuleId(string moduleid);

        public abstract List<Module> GetModulesInRole(string roleName);

        // public abstract bool MailingListCheckEmail(string email);
        public abstract Page GetPageByPageId(int pageId);

        public abstract string GetPageContentByURI(string p);

        public abstract StringBuilder GetPageDataForBreadCrumbs(string p, Label lblPageName);

        public abstract Hashtable GetPagesControls();

        //public abstract List<Page> GetPageUIRByGallery();
        public abstract string GetPageTitleByURI(string p);

        public abstract List<Module> GetPermittedModules(string modulelist);

        public abstract List<RawData> GetPrepRawData();

        public abstract string GetPrintURLWithData(UrlData urldata);

        public abstract List<ModulesAction> GetRoleModuleActionPermissions(string p);

        public abstract List<ModulePermmission> GetRoleModulePermissions(string roleName);

        public abstract List<DBMetaTable> GetTablesinDBbyDbId(int dbId);

        public abstract List<ColumnName> GetTablesInDBUsingConsetting(ConSetting db, string daatabasename);

        public abstract string GetUrlForPrintByControlName(string debitnote);

        public abstract Models.User getUserDetailByUsername(string username);

        public abstract int GetUserID(string username);

        public abstract bool GetUserPresenterType(string p);

        public abstract string GetUserRoleByUserId(string UserRecID);

        public abstract string getUserRoleByUserName(string username);

        public abstract UserRole getUserRoleByUserRoleName(string userrolename);

        public abstract Models.User Login(string username, string password);

        public abstract int SetUserInRole(string username, string rolename);

        public abstract int UpdateBaseMetaColumn(BaseMetaColumn p);

        public abstract int UpdateConfigDetail(Config aston);

        public abstract int UpdateConSetting(ConSetting p);

        public abstract int UpdateModuleInfo(Module moduleinfo);

        public abstract int UpdatePassword(string Username, int UserId, string enc_p, string new_p);

        public abstract int UpdateUserDetail(Models.User user);

        public abstract int UpdateUserRole(UserRole userole);

        public abstract bool UsersExist(string username);

        public abstract bool CheckTableExits(string tableName, int dBId);

        public abstract int AddUsrExport(UsrExport exp);

        public abstract List<UsrExport> GetUsrExportsByUserId(int v);

        public abstract int AddVxSearchParam(VxSearchParam curr);

        public abstract List<VxSearchParam> GetUserFilters(int v);

        public abstract int DeleteRecordByID(int categoryID);

        public abstract int UpdateVxSearchParam(VxSearchParam contact);
    }
}
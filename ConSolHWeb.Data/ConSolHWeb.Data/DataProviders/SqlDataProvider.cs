using ConSolHWeb.Data.DataProviders;
using ConSolHWeb.Data.Models;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace ConSolHWeb.Data
{
    public class SqlDataProvider : DataProvider
    {
        private ConSolHDBContext db = null;

        public SqlDataProvider()
        {
            db = new ConSolHDBContext();
        }

        private static string connectionString()
        {
            CPNetFrameWorkDataProvidersSection sec = (ConfigurationManager.GetSection("CPNetFrameWorkDataProviders")) as CPNetFrameWorkDataProvidersSection;
            string connectionStringName = sec.DataProviders[sec.DataProviderName].Parameters["connectionStringName"];
            return WebConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }

        public string connectionString(string me)
        {
            CPNetFrameWorkDataProvidersSection sec = (ConfigurationManager.GetSection("CPNetFrameWorkDataProviders")) as CPNetFrameWorkDataProvidersSection;
            string connectionStringName = sec.DataProviders[sec.DataProviderName].Parameters["connectionStringName"];
            return WebConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }

        public override List<User> getAllUserDetails()
        {
            var config = (from c in db.Users
                          select c).ToList();
            if (config != null) { return config; }
            return null;
        }

        public override int AddUserDetail(User user)
        {
            if (user != null)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public override int UpdateUserDetail(User user)
        {
            try
            {
                User p = (from c in db.Users
                          where c.UserId == user.UserId
                          select c).FirstOrDefault();
                p.FirstName = user.FirstName;
                p.LastName = user.LastName;
                p.Title = user.Title;
                p.Password = user.Password;
                p.Text_Pass = user.Text_Pass;

                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public override int DeleteUserDetail(string username)
        {
            try
            {
                var config = (from c in db.Users
                              where c.UserName == username
                              select c).FirstOrDefault();
                db.Users.Remove(config);
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public override User getUserDetailByUsername(string username)
        {
            var config = (from c in db.Users
                          where c.UserName == username
                          select c).FirstOrDefault();
            return config;
        }

        public override List<UserRole> getAllUserRoles()
        {
            var config = (from c in db.UserRoles
                          select c).ToList();
            return config;
        }

        public override int AddUserRole(UserRole userole)
        {
            if (userole != null)
            {
                db.UserRoles.Add(userole);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public override int UpdateUserRole(UserRole userole)
        {
            try
            {
                UserRole p = (from c in db.UserRoles
                              where c.UserRoleName == userole.UserRoleName
                              select c).FirstOrDefault();
                p.RoleDescription = userole.RoleDescription;

                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public override int DeleteUserRole(string userrolename)
        {
            try
            {
                var config = (from c in db.UserRoles
                              where c.UserRoleName == userrolename
                              select c).FirstOrDefault();
                db.UserRoles.Remove(config);
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public override UserRole getUserRoleByUserRoleName(string userrolename)
        {
            var config = (from c in db.UserRoles
                          where c.UserRoleName == userrolename
                          select c).FirstOrDefault();
            if (config != null) { return config; }
            return null;
        }

        public override int SetUserInRole(string username, string rolename)
        {
            try
            {
                var config = (from c in db.UserInRoles
                              where c.UserName == username
                              select c).FirstOrDefault();
                if (config != null)
                {
                    db.UserInRoles.Remove(config);
                    db.UserInRoles.Add(new UserInRole() { UserName = username, UserRoleName = rolename });
                }
                else { db.UserInRoles.Add(new UserInRole() { UserName = username, UserRoleName = rolename }); }
                db.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public override User Login(string username, string password)
        {
            if ((!string.IsNullOrEmpty(username)) && (!string.IsNullOrEmpty(password)))
            {
                User user = (from c in db.Users
                             where c.UserName == username && c.Password == password
                             select c).FirstOrDefault();
                return user;
            }
            return null;
        }

        public override bool GetIfCurUserAllowed()
        {
            return false;
        }

        public override List<Module> getAllModules()
        {
            var config = (from c in db.Modules
                          orderby c.M_Order
                          select c).ToList();
            if (config != null) { return config; }
            return null;
        }

        public override int AddModuleInfo(Module moduleinfo)
        {
            if (moduleinfo != null)
            {
                db.Modules.Add(moduleinfo);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public override int UpdateModuleInfo(Module moduleinfo)
        {
            throw new NotImplementedException();
        }

        public override int DeleteModuleInfo(string moduleid)
        {
            try
            {
                var config = (from c in db.Modules
                              where c.ModuleID == moduleid
                              select c).FirstOrDefault();
                db.Modules.Remove(config);
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public override Module getModuleInfoByModuleId(string moduleid)
        {
            var config = (from c in db.Modules
                          where c.ModuleID == moduleid
                          select c).FirstOrDefault();
            if (config != null) { return config; }
            return null;
        }

        public override int AddModulesInRole(string roleName, string modules)
        {
            ModulesInRole config = null;
            try
            {
                config = (from c in db.ModulesInRoles
                          where c.RoleName == roleName
                          select c).FirstOrDefault();

                ModulesInRole mir = new ModulesInRole();
                mir.RoleName = roleName;
                mir.Modules = modules;

                if (!string.IsNullOrEmpty(roleName) && !string.IsNullOrEmpty(modules))
                {
                    if (config != null)
                    {
                        db.ModulesInRoles.Remove(config);

                        db.ModulesInRoles.Add(mir);
                        db.SaveChanges();
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public override List<Module> GetModulesInRole(string roleName)
        {
            List<Module> list = new List<Module>();
            if (roleName != "System Administrator")
            {
                var config = (from c in db.ModulesInRoles
                              where c.RoleName == roleName
                              select c).ToList();
                foreach (var item in config)
                {
                    string[] modules = item.Modules.Split(',');
                    if (modules != null)
                    {
                        foreach (var module in modules)
                        {
                            if (!string.IsNullOrEmpty(module))
                            {
                                Module m = new Module();
                                m.ModuleID = module;
                                list.Add(m);
                            }
                        }
                    }
                }
                return list;
            }
            return null;
        }

        public override int AddModulePermissions(List<ModulePermmission> modulepermissions, string RoleName)
        {
            return 0;
        }

        public override List<ModulePermmission> GetRoleModulePermissions(string roleName)
        {
            if (roleName != "System Administrator")
            {
                List<ModulePermmission> list = new List<ModulePermmission>();

                return list;
            }
            return null;
        }

        public override string getUserRoleByUserName(string username)
        {
            var config = (from c in db.UserInRoles
                          where c.UserName == username
                          select c).FirstOrDefault();
            if (config != null) { return config.UserRoleName; }
            return "";
        }

        public override bool UsersExist(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                var p = (from c in db.Users
                         where c.UserName == username
                         select c).FirstOrDefault();
                if (p != null) { return true; }
            }
            return false;
        }

        public override int UpdateConfigDetail(Config aston)
        {
            throw new NotImplementedException();
        }

        public override Config GetConfigDetailByConfigID(int p)
        {
            var config = (from c in db.Configs
                          where c.RecID == p
                          select c).FirstOrDefault();
            if (config != null) { return config; }
            return null;
        }

        public override int GetUserID(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                var p = (from c in db.Users
                         where c.UserName == username
                         select c).FirstOrDefault();
                if (p != null) { return p.UserId; }
            }
            return 0;
        }

        public override Hashtable GetConfig()
        {
            Hashtable ht = new Hashtable();
            var configs = (from c in db.Configs
                           select c).ToList();
            foreach (var item in configs)
            {
                ht[item.ConfigName] = item.ConfigValue;
            }
            return ht;
        }

        public override List<Module> GetPermittedModules(string modulelist)
        {
            throw new NotImplementedException();
        }

        public override bool GetUserPresenterType(string p)
        {
            throw new NotImplementedException();
        }

        public override Hashtable getListControls()
        {
            Hashtable ht = new Hashtable();
            var p = (from c in db.Pages
                     where c.Pagetype == 1
                     select c).ToList();
            foreach (var item in p)
            {
                ht[item.ControlName.ToLower()] = item;
            }
            return ht;
        }

        public override Hashtable getEditControls()
        {
            throw new NotImplementedException();
        }

        public override Hashtable getFriendlyNames()
        {
            throw new NotImplementedException();
        }

        public override int UpdatePassword(string Username, int UserId, string enc_p, string new_p)
        {
            try
            {
                User p = (from c in db.Users
                          where c.UserId == UserId
                          select c).FirstOrDefault();
                p.Text_Pass = new_p;
                p.Password = enc_p;
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public override bool CheckIfEmailExists(string emailAdd)
        {
            try
            {
                var p = (from c in db.Users
                         where c.UserName == emailAdd
                         select c).FirstOrDefault();
                return p.UserName == emailAdd;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override List<Module> getAllModulesByRole(string roleName)
        {
            if (roleName != "Administrator")
            {
                List<Module> list = new List<Module>();
                var modules = (from c in db.ModulesInRoles
                               where c.RoleName == roleName
                               select c).ToList();
                foreach (var item in modules)
                {
                    if (!string.IsNullOrEmpty(item.Modules))
                    {
                        string[] modList = item.Modules.Split(',');
                        foreach (var module in modList)
                        {
                            if (!string.IsNullOrEmpty(module))
                            {
                                var modDet = (from c in db.Modules
                                              where c.ModuleID == module
                                              select c).FirstOrDefault();
                                list.Add(modDet);
                            }
                        }
                    }
                }
                return list;
            }
            else { return null; }
        }

        public override List<ModulesAction> getAllModuleActionsPermmitedByRole(string roleName)
        {
            if (roleName != "Administrator")
            {
                return null;
            }
            return null;
        }

        //public override int AddEmailToList(MailingList maillist)
        //{
        //    if (maillist != null)
        //    {
        //        db.MailingLists.Add(maillist);
        //        db.SaveChanges();
        //        return 1;
        //    }
        //    return 0;
        //}

        //public override bool MailingListCheckEmail(string email)
        //{
        //    if (!string.IsNullOrEmpty(email))
        //    {
        //        var p = (from c in db.MailingLists
        //                 where c.Email == email
        //                 select c).FirstOrDefault();
        //        if (p != null) { return true; }
        //    }
        //    return false;
        //}

        public override List<Page> GetAllPages()
        {
            var config = (from c in db.Pages
                          where c.Pagetype == 1
                          orderby c.orderrank
                          select c).ToList();
            return config;
        }

        public override int AddPage(Page page)
        {
            if (page != null)
            {
                db.Pages.Add(page);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public override Page GetPageByPageId(int pageId)
        {
            var config = (from c in db.Pages
                          where c.page_id == pageId
                          select c).FirstOrDefault();
            if (config != null) { return config; }
            return null;
        }

        //public override int UpdatePage(Page page, List<ControlsForPage> ctrls)
        //{
        //    try
        //    {
        //        if (page != null && ctrls.Count > 2)
        //        {
        //            var p = (from k in db.Pages
        //                     where k.page_id == page.page_id
        //                     select k).FirstOrDefault();
        //            if (p != null)
        //            {
        //                p.additionals = page.additionals;
        //                p.created_at = page.created_at;
        //                p.body = page.body;
        //                p.in_bottom_nav = page.in_bottom_nav;
        //                p.in_footer_nav = page.in_footer_nav;
        //                p.in_main_nav = page.in_main_nav;
        //                p.is_home = page.is_home;
        //                p.meta_description = page.meta_description;
        //                p.meta_keywords = page.meta_keywords;
        //                p.meta_title = page.meta_title;
        //                p.nav_title = page.nav_title;
        //                p.status = page.status;
        //                p.title = page.title;
        //                p.uri = page.uri;
        //                p.ControlName = page.uri;
        //                p.slug = page.slug;
        //                p.banner = page.banner;
        //                p.css = page.css;
        //                p.js = page.js;
        //                //p.orderrank = page.orderrank;
        //                p.sub_template = page.sub_template;
        //                p.template = page.template;
        //                p.widgets = page.widgets;
        //            }

        //            var Itemtorem = (from c in db.ControlsForPages
        //                             where c.PageUri == page.uri
        //                             select c);
        //            if (Itemtorem != null)
        //                db.ControlsForPages.RemoveRange(Itemtorem);

        //            foreach (var item in ctrls)
        //            {
        //                db.ControlsForPages.Add(item);
        //            }
        //            db.SaveChanges();
        //            return 1;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    return 0;
        //}

        //public override List<PageControl> GetAllPageControls()
        //{
        //    var config = (from c in db.PageControls
        //                  select c).ToList();
        //    return config;
        //}

        //public override List<ControlsForPage> GetPageControlsByPageUri(string pageUri)
        //{
        //    var config = (from c in db.ControlsForPages
        //                  join p in db.PageControls on c.ControlName equals p.Name
        //                  orderby p.IOrder ascending
        //                  where c.PageUri == pageUri
        //                  select c).ToList();
        //    return config;
        //}

        //public override int DeletePage(int ship)
        //{
        //    try
        //    {
        //        List<ControlsForPage> crts = null;
        //        var page = (from c in db.Pages
        //                    where c.page_id == ship
        //                    select c).FirstOrDefault();
        //        if (page != null)
        //        {
        //            crts = (from t in db.ControlsForPages
        //                    where t.PageUri == page.uri || t.PageUri == page.ControlName
        //                    select t).ToList();
        //            db.Pages.Remove(page);
        //        }
        //        if (crts != null)
        //        {
        //            db.ControlsForPages.RemoveRange(crts);
        //        }
        //        db.SaveChanges();
        //        return 1;
        //    }
        //    catch (Exception e)
        //    {
        //        return 0;
        //    }
        //}

        //public override List<PageControlsAdmin> GetAllLinksByPCName(string pcName)
        //{
        //    var config = (from c in db.PageControlsAdmins
        //                  where c.PCName == pcName
        //                  select c).ToList();
        //    return config;
        //}

        //public override List<Banner> GetAllBanners()
        //{
        //    var config = (from c in db.Banners
        //                  select c).ToList();
        //    return config;
        //}

        //public override List<Banner> ProductsAsBanners()
        //{
        //    List<Banner> list = new List<Banner>();

        //    return list;
        //}

        public override StringBuilder GetPageDataForBreadCrumbs(string p, Label lblPageName)
        {
            List<ConSolHWeb.Data.Models.Page> pages = new List<ConSolHWeb.Data.Models.Page>();
            var home = (from c in db.Pages
                        where c.ControlName == "home"
                        select c).FirstOrDefault();
            pages.Add(home);

            var m = (from c in db.Pages
                     where c.ControlName == p
                     select c).FirstOrDefault();
            if (m.parent_id == m.page_id)
            {
                pages.Add(m);
            }
            else
            {
                var m1 = (from c in db.Pages
                          where c.page_id == m.parent_id
                          select c).FirstOrDefault();
                if (m1 != null)
                {
                    pages.Add(m1);
                }
                pages.Add(m);
            }

            StringBuilder middle = new StringBuilder();
            foreach (var item in pages.OrderBy(c => c.orderrank))
            {
                if (item.ControlName != m.ControlName)
                {
                    middle.Append("<li><a href='" + Util.BaseSiteUrl + "p.aspx?p=" + item.ControlName + "'>" + item.nav_title + "</a></li>");
                }
                else
                {
                    lblPageName.Text = item.nav_title;
                    middle.Append("<li><a class='active' href='" + Util.BaseSiteUrl + "p.aspx?p=" + item.ControlName + "'>" + item.nav_title + "</a></li>");
                }
            }
            return middle;
        }

        public override Hashtable getListControl2s()
        {
            Hashtable ht = new Hashtable();
            var p = (from c in db.Pages
                     where c.Pagetype == 2
                     select c).ToList();
            foreach (var item in p)
            {
                ht[item.ControlName.ToLower()] = item;
            }
            return ht;
        }

        public override string GetPageTitleByURI(string p)
        {
            var pa = (from p1 in db.Pages
                      where p1.uri == p
                      select p1).FirstOrDefault();
            return pa.title;
        }

        public override string GetPageContentByURI(string p)
        {
            var pa = (from p1 in db.Pages
                      where p1.uri == p
                      select p1).FirstOrDefault();
            return pa.body;
        }

        public override string GetUserRoleByUserId(string UserId)
        {
            var config = (from c in db.UserInRoles
                          where c.UserName == UserId
                          select c).FirstOrDefault();
            if (config != null) { return config.UserRoleName; }
            return "";
        }

        public override List<ModulesAction> GetModuleActionsByModules(string modules)
        {
            throw new NotImplementedException();
        }

        public override List<ModulesAction> GetRoleModuleActionPermissions(string p)
        {
            throw new NotImplementedException();
        }

        public override int AddModulePermissions(List<ModulePermmission> permlist, string p, List<ModulesAction> actionlist)
        {
            throw new NotImplementedException();
        }

        public override List<ModulesAction> getAllModulesActionPermmitedByRole(string roleName)
        {
            if (roleName != "Administrator")
            {
                return null;
            }
            return null;
        }

        public override List<EmployeeDetail2> GetAllEmployees()
        {
            using (SqlConnection con = new SqlConnection(connectionString()))
            {
                List<EmployeeDetail2> list = new List<EmployeeDetail2>();
                con.Open();
                using (Stream file = Util.getAppResource("AllActivePayrollEmployeeData"))
                {
                    System.IO.StreamReader SReader = new System.IO.StreamReader(file);
                    string strSQL = SReader.ReadToEnd();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = strSQL; cmd.CommandTimeout = 0;
                    SqlDataReader r = cmd.ExecuteReader();
                    EmployeeDetail2 curr;
                    while (r.Read())
                    {
                        curr = new EmployeeDetail2();
                        curr.Category = (r["Category"] is DBNull) ? "" : (string)r["Category"];
                        curr.EmpId = (r["EmpId"] is DBNull) ? String.Empty : (string)r["EmpId"];
                        curr.SalaryStep = (r["SalaryStep"] is DBNull) ? String.Empty : (string)r["SalaryStep"];
                        curr.Firstname = (r["FirstName"] is DBNull) ? String.Empty : (string)r["FirstName"];
                        curr.MiddleName = (r["MiddleName"] is DBNull) ? string.Empty : (string)r["MiddleName"];
                        curr.FullName = (r["FullName"] is DBNull) ? string.Empty : (string)r["FullName"];
                        curr.LastName = (r["LastName"] is DBNull) ? string.Empty : (string)r["LastName"];
                        curr.Dept = (r["Dept"] is DBNull) ? String.Empty : (string)r["Dept"];
                        curr.Gender = (r["Gender"] is DBNull) ? string.Empty : (string)r["Gender"];
                        curr.Deduction = (r["Deduction"] is DBNull) ? 0 : (decimal)r["Deduction"];
                        curr.Earnings = (r["Earnings"] is DBNull) ? 0 : (decimal)r["Earnings"];
                        curr.NetTotal = (r["NetTotal"] is DBNull) ? 0 : (decimal)r["NetTotal"];
                        list.Add(curr);
                    }
                    con.Close();
                    return list;
                }
            }
        }

        public override List<USERINFO> GetAllUnprocessedEmployees()
        {
            var pa = (from p1 in db.USERINFOes
                      where p1.Processed == null
                      select p1).ToList();
            return pa;
        }

        public override USERINFO GetEmployeeDetailByID(int uid)
        {
            var pa = (from p1 in db.USERINFOes
                      where p1.Id == uid
                      select p1).FirstOrDefault();
            return pa;
        }

        private int LoadClockData(int Batch)
        {
            try
            {
                int result = -1;
                using (SqlConnection con = new SqlConnection(connectionString()))
                {
                    con.Open();
                    string strSQL = Util.GetSQLString("MoveAttendanceByBatchId", new string[] { Batch.ToString() });
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = strSQL;
                    cmd.CommandTimeout = 0;
                    result = cmd.ExecuteNonQuery();
                    con.Close();
                }
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public override List<EmployeeDetail2> GetAllEmployeesToFix()
        {
            using (SqlConnection con = new SqlConnection(connectionString()))
            {
                List<EmployeeDetail2> list = new List<EmployeeDetail2>();
                con.Open();
                string strSQL = @"
                                    select *
                                    from EmployeeDetails e
                                    where e.Active = 1 and e.SalaryStep is null
                                ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = strSQL;
                cmd.CommandTimeout = 0;
                SqlDataReader r = cmd.ExecuteReader();
                EmployeeDetail2 curr;
                while (r.Read())
                {
                    curr = new EmployeeDetail2();
                    curr.Category = (r["Category"] is DBNull) ? "" : (string)r["Category"];
                    curr.EmpId = (r["EmpId"] is DBNull) ? String.Empty : (string)r["EmpId"];
                    curr.SalaryStep = (r["SalaryStep"] is DBNull) ? String.Empty : (string)r["SalaryStep"];
                    curr.Firstname = (r["FirstName"] is DBNull) ? String.Empty : (string)r["FirstName"];
                    curr.MiddleName = (r["MiddleName"] is DBNull) ? string.Empty : (string)r["MiddleName"];
                    curr.LastName = (r["LastName"] is DBNull) ? string.Empty : (string)r["LastName"];
                    curr.Dept = (r["Dept"] is DBNull) ? String.Empty : (string)r["Dept"];
                    curr.Gender = (r["Gender"] is DBNull) ? string.Empty : (string)r["Gender"];
                    curr.FullName = (r["FullName"] is DBNull) ? string.Empty : (string)r["FullName"];
                    list.Add(curr);
                }
                con.Close();
                return list;
            }
        }

        public override List<EmployeeDetail2> GetAllEmployees(int BatchId)
        {
            using (SqlConnection con = new SqlConnection(connectionString()))
            {
                List<EmployeeDetail2> list = new List<EmployeeDetail2>();
                con.Open();
                string strSQL = Util.GetSQLString("AllActivePayrollEmployeeData", new object[] { BatchId });
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = strSQL;
                cmd.CommandTimeout = 0;
                SqlDataReader r = cmd.ExecuteReader();
                EmployeeDetail2 curr;
                while (r.Read())
                {
                    curr = new EmployeeDetail2();
                    curr.Category = (r["Category"] is DBNull) ? "" : (string)r["Category"];
                    curr.EmpId = (r["EmpId"] is DBNull) ? String.Empty : (string)r["EmpId"];
                    curr.SalaryStep = (r["SalaryStep"] is DBNull) ? String.Empty : (string)r["SalaryStep"];
                    curr.Firstname = (r["FirstName"] is DBNull) ? String.Empty : (string)r["FirstName"];
                    curr.MiddleName = (r["MiddleName"] is DBNull) ? string.Empty : (string)r["MiddleName"];
                    curr.LastName = (r["LastName"] is DBNull) ? string.Empty : (string)r["LastName"];
                    curr.Dept = (r["Dept"] is DBNull) ? String.Empty : (string)r["Dept"];
                    curr.Gender = (r["Gender"] is DBNull) ? string.Empty : (string)r["Gender"];
                    curr.Deduction = (r["Deduction"] is DBNull) ? 0 : (decimal)r["Deduction"];
                    curr.Earnings = (r["Earnings"] is DBNull) ? 0 : (decimal)r["Earnings"];
                    curr.NetTotal = (r["NetTotal"] is DBNull) ? 0 : (decimal)r["NetTotal"];
                    list.Add(curr);
                }
                con.Close();
                return list;
            }
        }

        public override string GetUrlForPrintByControlName(string data)
        {
            //string url = Util.BaseSiteUrl + "ReportViewerPage.aspx?crt=debitnotereport&debitnoteid=" + debitnoteid;
            return "[{result: nice_url}]";
        }

        public override string GetPrintURLWithData(UrlData urldata)
        {
            StringBuilder sb = new StringBuilder();
            if (urldata != null)
            {
                if (ConfigurationManager.AppSettings["isDebug"] != null)
                {
                    int isDebug = int.Parse(ConfigurationManager.AppSettings["isDebug"]);
                    if (isDebug == 1)
                    {
                        sb.Append(HttpContext.Current.Server.UrlDecode(Util.BaseSiteUrl + "ReportViewerPage.aspx?crt=" + urldata.ctr));
                    }
                    else
                    {
                        sb.Append(HttpContext.Current.Server.UrlDecode("http://" + System.Environment.MachineName + Util.BaseVirtualAppPath + "ReportViewerPage.aspx?crt=" + urldata.ctr));
                    }
                    int counto = urldata.data.Count;
                    for (int i = 0; i < counto; i++)
                    {
                        sb.Append("&Param" + (i + 1) + "=" + urldata.data[i]);
                    }
                    return sb.ToString();
                }
            }
            return string.Empty;
        }

        public override Hashtable GetPagesControls()
        {
            Hashtable ht = new Hashtable();
            var py = (from c in db.PageCTRLs
                      select c).ToList();

            foreach (var item in py)
            {
                ht[item.ControlName.ToString()] = item;
            }
            return ht;
        }

        public override int AddConSetting(ConSetting page)
        {
            if (page != null)
            {
                db.ConSettings.Add(page);
                db.SaveChanges();
                var UserID = page.Id;
                return UserID;
            }
            return 0;
        }

        public override ConSetting GetConSettingById(int id)
        {
            var py = (from c in db.ConSettings
                      where c.Id == id
                      select c).FirstOrDefault();
            return py;
        }

        public override int UpdateConSetting(ConSetting page)
        {
            var py = (from k in db.ConSettings
                      where k.Id == page.Id
                      select k).FirstOrDefault();
            if (py != null)
            {
                py.Database = page.Database;
                py.IsActive = page.IsActive;
                py.Password = page.Password;
                py.Port = page.Port;
                py.ProjectName = page.ProjectName;
                py.ServerIP = page.ServerIP;
                py.Username = page.Username;
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public override int AddBaseMetaColumn(BaseMetaColumn page)
        {
            if (page != null)
            {
                db.BaseMetaColumns.Add(page);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public override BaseMetaColumn GetBaseMetaColumnById(int id)
        {
            var py = (from c in db.BaseMetaColumns
                      where c.Id == id
                      select c).FirstOrDefault();
            return py;
        }

        public override int UpdateBaseMetaColumn(BaseMetaColumn page)
        {
            var py = (from k in db.BaseMetaColumns
                      where k.Id == page.Id
                      select k).FirstOrDefault();
            if (py != null)
            {
                py.Id = page.Id;
                py.BaseColumnName = page.BaseColumnName;
            }
            db.SaveChanges();
            return 1;
        }

        public override int AddDBMetaTable(DBMetaTable page)
        {
            if (page != null)
            {
                db.DBMetaTables.Add(page);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public override int AddDBMetaColumn(DBMetaColumn page)
        {
            if (page != null)
            {
                db.DBMetaColumns.Add(page);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public override List<MatchMetaColumn> GetMatchMetaColumnsByDbId(int id)
        {
            var py = (from c in db.MatchMetaColumns
                      where c.DbId == id
                      select c).ToList();
            return py;
        }

        public override List<DBMetaTable> GetTablesinDBbyDbId(int dbId)
        {
            var py = (from c in db.DBMetaTables
                      where c.DBId == dbId
                      select c).ToList();
            return py;
        }

        public override List<DBMetaColumn> GetColumnsByTableId(int table)
        {
            var py = (from c in db.DBMetaColumns
                      where c.TableId == table
                      select c).ToList();
            return py;
        }

        public override int AddMatchMetaColumn(MatchMetaColumn page)
        {
            if (page != null)
            {
                db.MatchMetaColumns.Add(page);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public override List<MatchMetaColumn> CheckMatchMetaColumns(string tableColumn, string baseColumn, int tbaleId, int dbID)
        {
            var py = (from c in db.MatchMetaColumns
                      where c.TableId == tbaleId && c.DbId == dbID && c.DBColumn == tableColumn && c.BaseDbColumn == baseColumn
                      select c).ToList();
            return py;
        }

        public override List<ColumnName> GetColumnsByTableUsingConsetting(ConSetting db, string sTableName)
        {
            using (SqlConnection con = GetConnection(db))
            {
                List<ColumnName> list = new List<ColumnName>();
                string strSQL = string.Format(@"select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='{0}' ORDER BY COLUMN_NAME", sTableName);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = strSQL;
                cmd.CommandTimeout = 0;
                SqlDataReader r = cmd.ExecuteReader();
                ColumnName curr;
                while (r.Read())
                {
                    curr = new ColumnName();
                    curr.TColumnName = (r["COLUMN_NAME"] is DBNull) ? "" : (string)r["COLUMN_NAME"];
                    list.Add(curr);
                }
                con.Close();
                return list;
            }
        }

        private SqlConnection GetConnection(ConSetting gb)
        {
            try
            {
                string ConStr = string.Format("Data Source={0}; Initial Catalog={1}; User ID={2}; Password={3};", gb.ServerIP, gb.Database.Remove(gb.Database.Length - 2, 2), gb.Username, gb.Password);
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConStr;
                con.Open();
                if (con.State == ConnectionState.Open)
                    return con;
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override bool CheckColumnExits(string columnName, int tableId, int dbId)
        {
            var p = (from c in db.DBMetaColumns
                     where c.ColumnName == columnName && c.DbId == dbId && c.TableId == tableId
                     select c).ToList();
            if (p.Count > 0)
                return true;
            else return false;
        }

        public override object GetAllConSettings()
        {
            var py = (from c in db.ConSettings
                      where c.IsActive == true
                      select c).ToList();
            return py;
        }

        public override List<DBMetaTable> GetDBMetaTableByDBId(int v)
        {
            var py = (from c in db.DBMetaTables
                      where c.DBId == v
                      select c).ToList();
            return py;
        }

        public override List<ColumnName> GetTablesInDBUsingConsetting(ConSetting db, string daatabasename)
        {
            using (SqlConnection con = GetConnection(db))
            {
                List<ColumnName> list = new List<ColumnName>();
                string strSQL = string.Format(@"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG='{0}' ORDER BY TABLE_NAME", daatabasename.Remove(daatabasename.Length - 2, 2));
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = strSQL;
                cmd.CommandTimeout = 0;
                SqlDataReader r = cmd.ExecuteReader();
                ColumnName curr;
                while (r.Read())
                {
                    curr = new ColumnName();
                    curr.TColumnName = (r["TABLE_NAME"] is DBNull) ? "" : (string)r["TABLE_NAME"];
                    list.Add(curr);
                }
                con.Close();
                return list;
            }
        }

        public override bool CheckTableExits(string tableName, int dBId)
        {
            var p = (from c in db.DBMetaTables
                     where c.TableName == tableName && c.DBId == dBId
                     select c).ToList();
            if (p.Count > 0)
                return true;
            else return false;
        }

        public override List<ConSetting> GetAllConSettings2()
        {
            var py = (from c in db.ConSettings
                      select c).ToList();
            return py;
        }

        public override List<RawData> GetPrepRawData()
        {
            List<RawData> list = new List<RawData>();
            List<ConSetting> alldata = GetAllConSettings2();
            if (alldata != null)
            {
                foreach (ConSetting item in alldata)
                {
                    RawData curr = new RawData();
                    curr.DatabaseName = item.Database;
                    curr.IPAddress = item.ServerIP;
                    curr.TableName = "";
                    curr.DbId = item.Id.ToString();
                    curr.TotalRecordCount = 0;
                    curr.NowSyching = item.IsActive;
                    list.Add(curr);
                    curr = null;
                }
                return list;
            }
            return null;
        }

        public override string GetDBTableByDBId(string dbId)
        {
            int dbIdk = int.Parse(dbId.Trim());
            var name = (from c in db.DBMetaTables
                        where c.DBId == dbIdk
                        select c.TableName).FirstOrDefault();
            if (name != string.Empty)
                return name;
            else
                return string.Empty;
        }

        public override long GetRecordCountPerDBByDbId(int v, string tablename)
        {
            try
            {
                long recordCount = 0;
                ConSetting gb = DataService.Provider.GetConSettingById(v);
                //string ConStr = string.Format("Data Source={0}; Initial Catalog={1}; User ID={2}; Password={3};", gb.ServerIP, gb.Database.Remove(gb.Database.Length - 2, 2), gb.Username, gb.Password);
                SqlConnection con = GetConnection(gb);
                if (con != null && con.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand(string.Format("SELECT count(*) FROM {0}", tablename), con);
                    cmd.CommandTimeout = 0;
                    recordCount = long.Parse(cmd.ExecuteScalar().ToString());
                }
                return recordCount;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public override string GetHarmonisedCount()
        {
            long recordCount = 0;
            using (SqlConnection con = new SqlConnection(connectionString("me")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT count(*) FROM VxTelephones", con);
                cmd.CommandTimeout = 0;
                recordCount = long.Parse(cmd.ExecuteScalar().ToString());

                return recordCount.ToString("N00", CultureInfo.InvariantCulture);
            }
        }

        public override int AddUsrExport(UsrExport page)
        {
            if (page != null)
            {
                db.UsrExports.Add(page);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public override List<UsrExport> GetUsrExportsByUserId(int v)
        {
            var py = (from c in db.UsrExports
                      where c.UserId == v
                      select c).ToList();
            return py;
        }

        public override List<RawData> GetPrepHarmonizedData()
        {
            return GetAllConSettings3();
        }

        private List<RawData> GetAllConSettings3()
        {
            using (SqlConnection con = new SqlConnection(connectionString()))
            {
                List<RawData> list = new List<RawData>();
                con.Open();

                string strSQL = @"SELECT t.DbId, t.TableName,DBColumn u_column,BaseDbColumn, d.[Database] db, d.ServerIP
                                      FROM MatchMetaColumns c inner join DBMetaTables t on c.DbId = t.DbId
                                      inner join ConSettings d on c.DbId = d.Id
                                  WHERE BaseDbColumn = 'PhoneNo'";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = strSQL;
                cmd.CommandTimeout = 0;
                SqlDataReader r = cmd.ExecuteReader();
                RawData curr;
                while (r.Read())
                {
                    curr = new RawData();
                    curr.DatabaseName = (r["db"] is DBNull) ? "" : (string)r["db"];
                    curr.IPAddress = (r["ServerIP"] is DBNull) ? String.Empty : (string)r["ServerIP"];
                    curr.TableName = (r["TableName"] is DBNull) ? String.Empty : (string)r["TableName"];
                    curr.ColumnName = (r["u_column"] is DBNull) ? String.Empty : (string)r["u_column"];
                    curr.DbId = (r["DbId"] is DBNull) ? string.Empty : (string)r["DbId"].ToString();
                    curr.TotalRecordCount = 0;
                    list.Add(curr);
                }
                con.Close();
                return list;
            }
        }

        public override long GetUniqueRecordCountPerDBByDbId(int v, string columnName, string tableName)
        {
            if (!string.IsNullOrEmpty(columnName))
            {
                long recordCount = 0;
                ConSetting gb = DataService.Provider.GetConSettingById(v);
                //string ConStr = string.Format("Data Source={0}; Initial Catalog={1}; User ID={2}; Password={3};", gb.ServerIP, gb.Database.Remove(gb.Database.Length - 2, 2), gb.Username, gb.Password);
                SqlConnection con = GetConnection(gb);
                if (con != null && con.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand(string.Format("SELECT count(distinct {0}) FROM {1} WHERE LEN({0}) > 7 ", columnName, tableName), con);
                    cmd.CommandTimeout = 0;
                    recordCount = long.Parse(cmd.ExecuteScalar().ToString());

                    return recordCount;
                }
            }
            return 0;
        }

        public override DataCount GetDataCount()
        {
            DataCount curr = null;
            using (SqlConnection con = new SqlConnection(connectionString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(@"Select
                                                        MOBILENUMBER1Count
                                                        ,NAMECount
                                                        ,GENDERcount
                                                        ,AGECount
                                                        ,TOWNCount
                                                        ,OCCUPATIONCount
                                                        ,JOBSTATUSCount
                                                        , EMAILCount
                                                        ,INDUSTRYCount
                                                        ,STATECount
                                                        ,LGACount
                                                 from DataCountDash
                                                "
                                            , con);
                cmd.CommandTimeout = 0;
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    curr = new DataCount();
                    curr.PhoneNoCount = (r["MOBILENUMBER1Count"] is DBNull) ? 0 : int.Parse(r["MOBILENUMBER1Count"].ToString());
                    curr.NAMECount = (r["NAMECount"] is DBNull) ? 0 : int.Parse(r["NAMECount"].ToString());
                    curr.GENDERCount = (r["GENDERCount"] is DBNull) ? 0 : int.Parse(r["GENDERCount"].ToString());
                    curr.AGECount = (r["AGECount"] is DBNull) ? 0 : int.Parse(r["AGECount"].ToString());
                    curr.TOWNCount = (r["TOWNCount"] is DBNull) ? 0 : int.Parse(r["TOWNCount"].ToString());
                    curr.OCCUPATIONCount = (r["OCCUPATIONCount"] is DBNull) ? 0 : int.Parse(r["OCCUPATIONCount"].ToString());
                    curr.JOBSTATUSCount = (r["JOBSTATUSCount"] is DBNull) ? 0 : int.Parse(r["JOBSTATUSCount"].ToString());
                    curr.EMAILCount = (r["EMAILCount"] is DBNull) ? 0 : int.Parse(r["EMAILCount"].ToString());
                    curr.INDUSTRYCount = (r["INDUSTRYCount"] is DBNull) ? 0 : int.Parse(r["INDUSTRYCount"].ToString());
                    curr.STATECount = (r["STATECount"] is DBNull) ? 0 : int.Parse(r["STATECount"].ToString());
                    curr.LGACount = (r["LGACount"] is DBNull) ? 0 : int.Parse(r["LGACount"].ToString());
                    //curr.PhoneNoCount = (r["PhoneNoCount"] is DBNull) ? 0 : int.Parse(r["PhoneNoCount"].ToString());
                }
                con.Close();
            }
            return curr;
        }

        public override int AddVxSearchParam(VxSearchParam page)
        {
            if (page != null)
            {
                db.VxSearchParams.Add(page);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public override List<VxSearchParam> GetUserFilters(int v)
        {
            var py = (from c in db.VxSearchParams
                      where c.UserId == v
                      select c).ToList();
            return py;
        }

        public override int DeleteRecordByID(int categoryID)
        {
            var py = (from c in db.VxSearchParams
                      where c.Id == categoryID
                      select c).FirstOrDefault();
            if (py != null)
            {
                db.VxSearchParams.Remove(py);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public override int UpdateVxSearchParam(VxSearchParam contact)
        {
            var py = (from k in db.VxSearchParams
                      where k.Id == contact.Id
                      select k).FirstOrDefault();
            if (py != null)
            {
                py.Id = contact.Id;
                py.pControl = contact.pControl;
                py.pDecision = contact.pDecision;
                py.pName = contact.pName;
                py.pOptions = contact.pOptions;
                py.pValue = contact.pValue;
                py.UserId = contact.UserId;
            }
            db.SaveChanges();
            return 1;
        }

        public override int ExecuteSQL(string sQL)
        {
            using (SqlConnection con = new SqlConnection(connectionString()))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sQL, con))
                {
                    cmd.CommandTimeout = 0;
                    int recordCount = cmd.ExecuteNonQuery();
                    return recordCount;
                }
            }
        }

        public override List<Stuff> GetJustDabase()
        {
            using (SqlConnection con = new SqlConnection(connectionString()))
            {
                List<Stuff> list = new List<Stuff>();
                con.Open();

                string strSQL = @"select Id, SUBSTRING([Database],0, len([Database])-1) DbName from [dbo].[ConSettings]";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = strSQL;
                cmd.CommandTimeout = 0;
                SqlDataReader r = cmd.ExecuteReader();
                Stuff curr;
                while (r.Read())
                {
                    curr = new Stuff();
                    curr.Name = (r["DbName"] is DBNull) ? "" : (string)r["DbName"];
                    curr.Id = (r["Id"] is DBNull) ? "" : (string)r["Id"].ToString();
                    list.Add(curr);
                }
                r.Close();
                con.Close();
                return list;
            }
        }

        public override List<Stuff> GetJustDbColumns()
        {
            using (SqlConnection con = new SqlConnection(connectionString()))
            {
                List<Stuff> list = new List<Stuff>();
                con.Open();

                string strSQL = @"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS
                                    where TABLE_NAME = 'VxDataPoints' and COLUMN_NAME <> 'AddrSet'";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = strSQL;
                cmd.CommandTimeout = 0;
                SqlDataReader r = cmd.ExecuteReader();
                Stuff curr;
                while (r.Read())
                {
                    curr = new Stuff();
                    curr.Name = (r["COLUMN_NAME"] is DBNull) ? "" : (string)r["COLUMN_NAME"];
                    list.Add(curr);
                }
                r.Close();
                con.Close();
                return list;
            }
        }

        public override DBMetaTable GetDBMetaTableByDbId(int dbId)
        {
            var p = (from c in db.DBMetaTables
                     where c.DBId == dbId
                     select c).FirstOrDefault();
            return p;
        }

        public override int SaveDBMetaTable(DBMetaTable mt)
        {
            if (mt != null)
            {
                var p = (from c in db.DBMetaTables
                         where c.DBId == mt.DBId
                         select c).ToList();
                if (p != null)
                {
                    db.DBMetaTables.RemoveRange(p);
                }
                db.DBMetaTables.Add(mt);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public override List<Stuff> GetJustDbColumns(string tableName, int dbId)
        {
            List<Stuff> list = new List<Stuff>();
            ConSetting gb = DataService.Provider.GetConSettingById(dbId);
            if (gb != null)
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = string.Format("Data Source={0}; Initial Catalog={1}; User ID={2}; Password={3};", gb.ServerIP, gb.Database.Remove(gb.Database.Length - 2, 2), gb.Username, gb.Password);
                    con.Open();
                    string strSQL = @"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS
                                    where TABLE_NAME = '" + tableName + "'";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = strSQL;
                    cmd.CommandTimeout = 0;
                    SqlDataReader r = cmd.ExecuteReader();
                    Stuff curr;
                    while (r.Read())
                    {
                        curr = new Stuff();
                        curr.Name = (r["COLUMN_NAME"] is DBNull) ? "" : (string)r["COLUMN_NAME"];
                        list.Add(curr);
                    }
                    r.Close();
                    con.Close();
                    return list;
                }
            }
            return null;
        }

        public override int SaveDBMetaColumn(List<DBMetaColumn> list, int dbId)
        {
            List<MatchMetaColumn> match = new List<MatchMetaColumn>();
            if (list != null)
            {
                var p = (from c in db.DBMetaColumns
                         where c.DbId == dbId
                         select c).ToList();
                var q = (from c in db.MatchMetaColumns
                         where c.DbId == dbId
                         select c).ToList();
                if (p != null)
                {
                    db.DBMetaColumns.RemoveRange(p);
                }
                if (q != null)
                {
                    db.MatchMetaColumns.RemoveRange(q);
                }
                db.DBMetaColumns.AddRange(list);
                foreach (var item in list)
                {
                    MatchMetaColumn m = new MatchMetaColumn();
                    m.DBColumn = item.ColumnName;
                    m.DbId = item.DbId;
                    m.TableId = item.TableId;
                    m.BaseDbColumn = "";
                    match.Add(m);
                }
                db.MatchMetaColumns.AddRange(match);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public override List<DBMetaColumn> GetDBMetaColumnByDbId(int dbId)
        {
            var p = (from c in db.DBMetaColumns
                     where c.DbId == dbId
                     select c).ToList();
            return p;
        }

        public override List<BaseMetaColumn> GetAllBaseDbColumns()
        {
            var p = (from c in db.BaseMetaColumns
                     select c).ToList();
            return p;
        }

        public override int SaveMatchMetaColumn(List<MatchMetaColumn> list)
        {
            if (list != null)
            {
                foreach (var item in list)
                {
                    var py = (from k in db.MatchMetaColumns
                              where k.DbId == item.DbId && k.TableId == item.TableId && k.DBColumn == item.DBColumn
                              select k).FirstOrDefault();
                    if (py != null)
                    {
                        py.BaseDbColumn = item.BaseDbColumn;
                    }
                }
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public override MatchMetaColumn GetMatchMetaColumnPicked(int dBId)
        {
            var py = (from k in db.MatchMetaColumns
                      where k.DbId == dBId && k.IsDColumn == true
                      select k).FirstOrDefault();
            return py;
        }

        public override int SetIsDColumn(int dbId, string columnName)
        {
            var py = (from k in db.MatchMetaColumns
                      where k.DbId == dbId && k.BaseDbColumn == columnName
                      select k).FirstOrDefault();
            if (py != null)
            {
                py.IsDColumn = true;
            }
            db.SaveChanges();
            return 1;
        }

        public override int UpdateUserDetailWithoutPassword(User user)
        {
            try
            {
                User p = (from c in db.Users
                          where c.UserId == user.UserId
                          select c).FirstOrDefault();
                p.FirstName = user.FirstName;
                p.LastName = user.LastName;
                p.Title = user.Title;

                db.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public override int UpdateUserDetailWithoutPasswordByAdmin(User user)
        {
            try
            {
                User p = (from c in db.Users
                          where c.UserName == user.UserName
                          select c).FirstOrDefault();
                p.FirstName = user.FirstName;
                p.LastName = user.LastName;
                p.Title = user.Title;
                p.IsActive = user.IsActive;
                p.Invaildated = user.Invaildated;
                p.Activated = user.Activated;

                db.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public override int UpdateUserDetailByAdmin(User user)
        {
            try
            {
                User p = (from c in db.Users
                          where c.UserId == user.UserId
                          select c).FirstOrDefault();
                p.FirstName = user.FirstName;
                p.LastName = user.LastName;
                p.Title = user.Title;
                p.Password = user.Password;
                p.Text_Pass = user.Text_Pass;
                p.IsActive = user.IsActive;
                p.Invaildated = user.Invaildated;
                p.Activated = user.Activated;

                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public override List<RawDataDashBoard> GetRawDataDashBoard()
        {
            var list = (from c in db.DataDashBoards
                        join d in db.ConSettings on c.DbId equals d.Id
                        join e in db.DBMetaTables on d.Id equals e.DBId
                        select new RawDataDashBoard()
                        {
                            Id = d.Id,
                            DatabaseName = d.ProjectName,
                            IPAddress = d.ServerIP,
                            TableName = e.TableName,
                            TotalRecordCount = (int)c.TotalRecordCount,
                            TotalUniqueCount = (int)c.TotalUniqueCount
                        }).ToList();
            return list;
        }
    }
}
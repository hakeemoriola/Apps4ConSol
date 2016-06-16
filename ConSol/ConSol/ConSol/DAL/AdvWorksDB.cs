using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ConSol.DAL
{
    public class AdvWorksDB
    {
        private const string CUSTOMER_CACHE_KEY = "CUSTOMER_DATA";
        private const string CUSTOMERCOUNT_CACHE_KEY = "CUSTOMER_COUNT";

        private const string FILTER_CACHE_KEY = "CUSTOMER_DATA";
        private const string FILTERCOUNT_CACHE_KEY = "CUSTOMER_COUNT";

        public static DataTable GetCustomersSortedPageWithSelection(int maximumRows, int startRowIndex, string sortExpression, string searchCriteria)
        {
            if (string.IsNullOrEmpty(sortExpression))
                sortExpression = "NAME";
            try
            {
                //if (AdvWorksDBCache.isRecordsCached(CUSTOMER_CACHE_KEY))
                //    return AdvWorksDBCache.GetData(CUSTOMER_CACHE_KEY, startRowIndex + 1, maximumRows, sortExpression, searchCriteria);
                string sql = "";
                SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString());
                if (!string.IsNullOrEmpty(searchCriteria))
                {
                    sql = @"Select
                                n.Surname + ' ' + n.Middlename + ' ' + n.Firstname NAME,
                                n.Gender GENDER,
                                c.Age AGE,
                                a.FullAddress ADDRESS,
                                a.Town TOWN,
                                t.TphoneNumber MOBILENUMBER1,
                                '' MOBILENUMBER2,
                                c.Occupation OCCUPATION,
                                c.EmploymentStatus JOBSTATUS,
                                e.Email EMAIL,
                                c.Industry INDUSTRY,
                                s.Name STATE,
                                a.LGA,
			                    co.ProjectName Source
                                from
                                VxTelephones t
                                left outer join VxNames n on t.PhoneNo = n.PhoneNo
                                left outer join VxCustomers c on t.PhoneNo = c.PhoneNo
                                left outer join VxAddress a on t.PhoneNo = a.PhoneNo
                                left outer join VxEmails e on t.PhoneNo = e.PhoneNo
			                    left outer join VxDbDataSource d on t.PhoneNo = d.PhoneNo
			                    left outer join ConSettings co on d.DbSource = co.Id
                                left outer join StateOptions s on a.State1 = s.Code
                            where d.DbSource in  (" + searchCriteria + ")";
                }
                else
                {
                    sql = @"Select
                                n.Surname + ' ' + n.Middlename + ' ' + n.Firstname NAME,
                                n.Gender GENDER,
                                c.Age AGE,
                                a.FullAddress ADDRESS,
                                a.Town TOWN,
                                t.TphoneNumber MOBILENUMBER1,
                                '' MOBILENUMBER2,
                                c.Occupation OCCUPATION,
                                c.EmploymentStatus JOBSTATUS,
                                e.Email EMAIL,
                                c.Industry INDUSTRY,
                                s.Name STATE,
                                a.LGA,
			                    co.ProjectName Source
                                from
                                VxTelephones t
                                left outer join VxNames n on t.PhoneNo = n.PhoneNo
                                left outer join VxCustomers c on t.PhoneNo = c.PhoneNo
                                left outer join VxAddress a on t.PhoneNo = a.PhoneNo
                                left outer join VxEmails e on t.PhoneNo = e.PhoneNo
			                    left outer join VxDbDataSource d on t.PhoneNo = d.PhoneNo
			                    left outer join ConSettings co on d.DbSource = co.Id
                                left outer join StateOptions s on a.State1 = s.Code";
                }
                SqlCommand custCommand = new SqlCommand(sql, dbConnection);
                custCommand.CommandTimeout = 0;
                custCommand.CommandType = CommandType.Text;

                SqlDataAdapter ad = new SqlDataAdapter(custCommand);
                DataTable dtCustomers = new DataTable();
                ad.Fill(dtCustomers);
                dbConnection.Close();

                //Cache records
                AdvWorksDBCache.Add(CUSTOMER_CACHE_KEY, dtCustomers);

            }
            catch (Exception e)
            {
                throw;
            }
            return AdvWorksDBCache.GetData(CUSTOMER_CACHE_KEY, startRowIndex + 1, maximumRows, sortExpression, null);
        }


        public static DataTable GetCustomersSortedPage(int maximumRows, int startRowIndex, string sortExpression, string searchCriteria)
        {
            if (string.IsNullOrEmpty(sortExpression))
                sortExpression = "NAME";
            try
            {
                if (AdvWorksDBCache.isRecordsCached(CUSTOMER_CACHE_KEY))
                    return AdvWorksDBCache.GetData(CUSTOMER_CACHE_KEY, startRowIndex + 1, maximumRows, sortExpression, searchCriteria);

                SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString());
                string sql = @"Select
                                n.Surname + ' ' + n.Middlename + ' ' + n.Firstname NAME,
                                n.Gender GENDER,
                                c.Age AGE,
                                a.FullAddress ADDRESS,
                                a.Town TOWN,
                                t.TphoneNumber MOBILENUMBER1,
                                '' MOBILENUMBER2,
                                c.Occupation OCCUPATION,
                                c.EmploymentStatus JOBSTATUS,
                                e.Email EMAIL,
                                c.Industry INDUSTRY,
                                s.Name STATE,
                                a.LGA,
			                    co.ProjectName Source
                                from
                                VxTelephones t
                                left outer join VxNames n on t.PhoneNo = n.PhoneNo
                                left outer join VxCustomers c on t.PhoneNo = c.PhoneNo
                                left outer join VxAddress a on t.PhoneNo = a.PhoneNo
                                left outer join VxEmails e on t.PhoneNo = e.PhoneNo
			                    left outer join VxDbDataSource d on t.PhoneNo = d.PhoneNo
			                    left outer join ConSettings co on d.DbSource = co.Id
                                left outer join StateOptions s on a.State1 = s.Code";
                SqlCommand custCommand = new SqlCommand(sql, dbConnection);
                custCommand.CommandTimeout = 0;
                custCommand.CommandType = CommandType.Text;

                SqlDataAdapter ad = new SqlDataAdapter(custCommand);
                DataTable dtCustomers = new DataTable();
                ad.Fill(dtCustomers);
                dbConnection.Close();

                //Cache records
                AdvWorksDBCache.Add(CUSTOMER_CACHE_KEY,dtCustomers);

            }
            catch (Exception e)
            {
                throw;
            }
            return AdvWorksDBCache.GetData(CUSTOMER_CACHE_KEY, startRowIndex + 1, maximumRows, sortExpression, null);
        }

        public static int GetCustomersCount(string searchCriteria)
        {
            int custCount = 0;
            try
            {
                SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString());
                string sql = "select count(*) from VxTelephones ";

                if (!string.IsNullOrEmpty(searchCriteria))
                    sql = sql + " where " + searchCriteria;
                
                SqlCommand sqlCommand = new SqlCommand(sql, dbConnection);
                sqlCommand.Connection = dbConnection;
                dbConnection.Open();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandTimeout = 0;
                custCount = Convert.ToInt32(sqlCommand.ExecuteScalar());

                dbConnection.Close();

                if (AdvWorksDBCache.Get(CUSTOMERCOUNT_CACHE_KEY) != null)
                {
                    // remove customers data if customers count has changed since first cache
                    if (Convert.ToInt32(AdvWorksDBCache.Get(CUSTOMERCOUNT_CACHE_KEY)) != custCount && string.IsNullOrEmpty(searchCriteria))
                    {
                        AdvWorksDBCache.Remove(CUSTOMER_CACHE_KEY);
                    }
                }

                if (string.IsNullOrEmpty(searchCriteria))
                    AdvWorksDBCache.Add(CUSTOMERCOUNT_CACHE_KEY , custCount);
            }
            catch (Exception e)
            {
                throw;
            }
            return custCount ;

        }

        public static int GetFilteredCount(string searchCriteria)
        {
            int custCount = 0;
            try
            {
                SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString());
                string sql = System.Web.HttpContext.Current.Session["FILTER_SQLCOUNT"].ToString();

                //if (!string.IsNullOrEmpty(searchCriteria))
                //    sql = sql + " where " + searchCriteria;

                SqlCommand sqlCommand = new SqlCommand(sql, dbConnection);
                sqlCommand.Connection = dbConnection;
                dbConnection.Open();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandTimeout = 0;
                custCount = Convert.ToInt32(sqlCommand.ExecuteScalar());

                dbConnection.Close();

                if (AdvWorksDBCache.Get(FILTERCOUNT_CACHE_KEY) != null)
                {
                    // remove customers data if customers count has changed since first cache
                    if (Convert.ToInt32(AdvWorksDBCache.Get(FILTERCOUNT_CACHE_KEY)) != custCount && string.IsNullOrEmpty(searchCriteria))
                    {
                        AdvWorksDBCache.Remove(FILTER_CACHE_KEY);
                    }
                }

                if (string.IsNullOrEmpty(searchCriteria))
                    AdvWorksDBCache.Add(FILTERCOUNT_CACHE_KEY, custCount);
            }
            catch (Exception e)
            {
                throw;
            }
            return custCount;

        }


        public static DataTable GetFilteredData(int maximumRows, int startRowIndex, string sortExpression, string searchCriteria)
        {
            if (string.IsNullOrEmpty(sortExpression))
                // sortExpression = "NAME";
            try
            {
                if (AdvWorksDBCache.isRecordsCached(FILTER_CACHE_KEY))
                    return AdvWorksDBCache.GetData(FILTER_CACHE_KEY, startRowIndex + 1, maximumRows, sortExpression, searchCriteria);

                SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString());
                string sql = System.Web.HttpContext.Current.Session["FITER_SQL"].ToString();
                SqlCommand custCommand = new SqlCommand(sql, dbConnection);
                custCommand.CommandTimeout = 0;
                custCommand.CommandType = CommandType.Text;

                SqlDataAdapter ad = new SqlDataAdapter(custCommand);
                DataTable dtCustomers = new DataTable();
                ad.Fill(dtCustomers);
                dbConnection.Close();

                //Cache records
                AdvWorksDBCache.Add(FILTER_CACHE_KEY, dtCustomers);

            }
            catch (Exception e)
            {
                throw;
            }
            return AdvWorksDBCache.GetData(FILTER_CACHE_KEY, startRowIndex + 1, maximumRows, sortExpression, null);
        }
    }
}

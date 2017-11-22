using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.EntityModel;
using Providers.Repositories;
using System.Data.SqlClient;
using System.Data;
using Providers.Helper;
using System.Web;

namespace Providers.Providers.SP.Repositories
{
    public class EmployeeRepository : IEmployee, IDisposable
    {
        SqlHelper objHelper = new SqlHelper();
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public UserDetailsDomainModel AuthenticateEmployee(UserDetailsDomainModel model)
        {
            try
            {
                string strSQL = "[dbo].[GetEmployeeByEmail]";
                string connStr = AppConfig.ConnectionString;

                SqlConnection connection = new SqlConnection(connStr);
                SqlCommand cmd = new SqlCommand(strSQL, connection) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 50)).Value = model.Email;
                connection.Open();
                IDataReader reader = cmd.ExecuteReader();
                List<UserDetailsDomainModel> items = new List<UserDetailsDomainModel>();
                DataMapper Mapper = new DataMapper();
                items = Mapper.MapData<UserDetailsDomainModel>(reader);
                connection.Close();
                if (items.Count() > 0)
                {
                    if (items.FirstOrDefault().Password != model.Password)
                    {
                        return null;
                    }
                }
                return items.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public UserDetailsDomainModel AuthenticateEmployees(UserDetailsDomainModel model)
        {
            SqlHelper objHelper = new SqlHelper();
            UserDetailsDomainModel user = new UserDetailsDomainModel();
            try
            {
                user = objHelper.Query<UserDetailsDomainModel>("GetEmployeeByEmail", new { email = model.Email }).FirstOrDefault();
                if (user != null && user.UserId > 0)
                {
                    if (user.Password != model.Password)
                    {
                        user.isSuccess = false;
                        user.response = "Pasword didn't match";
                    }
                    else
                    {
                        user.isSuccess = true;
                        user.response = "Success";
                    }
                    return user;
                }
            }
            catch(Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
            return user;
        }
        public ResponseDomainModel ChangePassword(ChangePassword model)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            try
            {
                var user = objHelper.Query<UserDetailsDomainModel>("GetEmployeeDataById", new { UserId = model.UserId }).FirstOrDefault();
                if (user != null && user.UserId > 0)
                {
                    if (user.Password == model.OldPassword)
                    {
                        int res = objHelper.Execute("UpdateUserPassword", new { UserId = model.UserId, Password = model.NewPassword });
                        if (res > 0)
                        {
                            objRes.response = "Password changed successfully.";
                            objRes.isSuccess = true;
                        }
                        else
                        {
                            objRes.response = "Something went wrong, Please try again.";
                            objRes.isSuccess = false;
                        }
                    }
                    else
                    {
                        objRes.response = "You have entered incorrect current password.";
                        objRes.isSuccess = false;
                    }
                }
                else
                {
                    objRes.response = "User does not exist.";
                    objRes.isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                objRes.response = "Something went wrong, Please try again.";
                objRes.isSuccess = false;
            }
            return objRes;
        }

        public List<SummaryOfWeekDetailsMainDomainModel> SummaryOfWeekDetailsMain(GetSummaryDomainModel model)
        {
            try
            {
                SqlHelper objHelper = new SqlHelper();
                var summaries = objHelper.Query<SummaryOfWeekDetailsMainDomainModel>("GET_SUMMARY_OF_WEEK_DETAILS_MAIN", new { startday = model.startday, endday = model.endday }).ToList();
                if (summaries.Count() > 0 && summaries != null)
                {
                    var GetDistinctProjects = summaries.GroupBy(x => x.ProjectId).Select(g => new { ProjectId = g.Key });
                    //get subdetails for each summary
                    foreach (var item in GetDistinctProjects)
                    {
                        var summary = objHelper.Query<SummaryOfWeekSubDetailsMainDomainModel>("GET_SUMMARY_OF_WEEK_SUBDETAILS", new { projectId = item.ProjectId, startday = model.startday, endday = model.endday }).ToList();
                        var GetProject = summaries.First(x => x.ProjectId == item.ProjectId);
                        GetProject.subweeksummary = summary;
                    }
                }
                return summaries;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<EmployeesDomainModel> GetEmployees()
        {
            try
            {
                SqlHelper objHelper = new SqlHelper();
                var employees = objHelper.Query<EmployeesDomainModel>("GetEmployeeList", null).ToList();
                return employees;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

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

        public UserDetails AuthenticateEmployee(UserDetails model)
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
                List<UserDetails> items = new List<UserDetails>();
                DataMapper Mapper = new DataMapper();
                items = Mapper.MapData<UserDetails>(reader);
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
        public UserDetails AuthenticateEmployees(UserDetails model)
        {
            SqlHelper objHelper = new SqlHelper();
            UserDetails user = new UserDetails();
            try
            {
                user = objHelper.Query<UserDetails>("GetEmployeeByEmail", new { email = model.Email }).FirstOrDefault();
                if (user != null && user.UserId > 0)
                {
                    if (user.Password != model.Password)
                    {
                        return null;
                    }
                }
            }
            catch(Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
            return user;
        }
        public static UserDetails GetCurrentUserProfile()
        {
            UserDetails info = null;
            if (!object.Equals(HttpContext.Current.Session["user"], null))
            {
                info = (UserDetails)HttpContext.Current.Session["user"];
            }
            return info;
        }
        public Response ChangePassword(ChangePassword model)
        {
            Response objRes = new Response();
            try
            {
                var user = objHelper.Query<UserDetails>("GetEmployeeDataById", new { UserId = model.UserId }).FirstOrDefault();
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
                    objRes.response = "User does not exists.";
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
    }
}

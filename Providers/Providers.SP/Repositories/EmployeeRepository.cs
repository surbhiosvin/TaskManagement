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

namespace Providers.Providers.SP.Repositories
{
    public class EmployeeRepository : IEmployee, IDisposable
    {
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
    }
}

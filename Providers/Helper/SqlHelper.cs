using System;
using System.Collections.Generic;
using System.Configuration;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Providers.Helper
{
    public class SqlHelper
    {
        private static string constr = AppConfig.ConnectionString;
        private SqlConnection con = new SqlConnection(constr);
         
        public int ExecuteScalar(string SpName, object param)
        {
            int res=Convert.ToInt32(con.ExecuteScalar(SpName, param, commandType: CommandType.StoredProcedure));
            return res;
        }
        public int Execute(string SpName, object param)
        {
            int res = con.Execute(SpName, param, commandType: CommandType.StoredProcedure);
            return res;
        }
        public IEnumerable<type> Query<type>(string SpName, object param)
        {
            var res = con.Query<type>(SpName, param, commandType: CommandType.StoredProcedure);
            return res;
        }
    }
}
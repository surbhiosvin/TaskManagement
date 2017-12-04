using DomainModel.EntityModel;
using Providers.Helper;
using Providers.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Providers.Providers.SP.Repositories
{
    public class EmployeeDailyStatusReportRepository: IEmployeeDailyStatusReport
    {
        SqlHelper objHelper = new SqlHelper();
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public List<ProjectReportCategoryDomainModel> GetProjectReportCategoryByDeptId(long DepartmentId)
        {
            try
            {
                var list = objHelper.Query<ProjectReportCategoryDomainModel>("GetProjectReportCategoryByDeptId", new { DepartmentId = DepartmentId }).ToList();
                return list;
            }
            catch(Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
    }
}

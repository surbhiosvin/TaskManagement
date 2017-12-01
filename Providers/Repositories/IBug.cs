using DomainModel.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Providers.Repositories
{
    public interface IBug
    {
        List<EmployeeDomainModel> GetEmployees();
        ResponseDomainModel AddUpdateBug(BugsDomainModel model);
        string AddBugFiles(BugFilesDomainModel model);
        string UpdateBugFiles(BugFilesDomainModel model);
        List<BugsDomainModel> GetBugDetails(BugsDomainModel model);
    }
}

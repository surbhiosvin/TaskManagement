using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.EntityModel;

namespace Providers.Repositories
{
    public interface IEmployee
    {
        UserDetailsDomainModel AuthenticateEmployee(UserDetailsDomainModel model);
        UserDetailsDomainModel AuthenticateEmployees(UserDetailsDomainModel model);
        ResponseDomainModel ChangePassword(ChangePassword model);
        List<SummaryOfWeekDetailsMain> SummaryOfWeekDetailsMain(GetSummaryDomainModel model);
    }
}

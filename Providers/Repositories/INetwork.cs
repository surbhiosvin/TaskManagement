using DomainModel.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Providers.Repositories
{
    public interface INetwork
    {
        List<ReportIssueDomainModel> GetReportIssues(ReportIssueDomainModel model);
        ResponseDomainModel AddUpdateReportIssue(ReportIssueDomainModel model);
        ReportIssueDomainModel GetIssueDescription(long ReportId, string Role);
        List<ReportIssueDomainModel> GetReportedIssueCount(long UserId, string Role);
        ResponseDomainModel ReportedIssueFeedBack(ReportIssueDomainModel model);
        ReportIssueDomainModel GetIssueFeedBack(long ReportId);
        List<NetworkResolvedIssueResultDomainModel> GetNetworkResolvedIssue(int Month, int Year);
    }
}

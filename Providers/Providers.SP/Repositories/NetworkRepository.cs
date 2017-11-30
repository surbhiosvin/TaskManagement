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
    public class NetworkRepository : INetwork, IDisposable
    {
        SqlHelper objHelper = new SqlHelper();
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public List<ReportIssueDomainModel> GetReportIssues(ReportIssueDomainModel model)
        {
            List<ReportIssueDomainModel> listReportIssues = new List<ReportIssueDomainModel>();
            try
            {
                listReportIssues = objHelper.Query<ReportIssueDomainModel>("GetReportedIssuesNew", new { @userid = model.UserId, role = model.Role, status = model.Status, isread = model.IsRead }).ToList();
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
            }
            return listReportIssues;
        }
        public ResponseDomainModel AddUpdateReportIssue(ReportIssueDomainModel model)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            try
            {
                var res = objHelper.Query<string>("AddUpdateReportIssue", new
                {
                    reportid = model.ReportId,
                    userid = model.UserId,
                    isuuesubject = model.IssueSubject,
                    issuedescription = model.IssueDescription,
                    priority = model.Priority,
                    remark = model.Remark,
                    status = model.Status
                }).FirstOrDefault();
                if (res == "Insert")
                {
                    objRes.isSuccess = true;
                    objRes.response = "Issue added successfully";
                }
                else if (res == "Update")
                {
                    objRes.isSuccess = true;
                    objRes.response = "Issue updated successfully.";
                }
                else
                {
                    objRes.isSuccess = false;
                    objRes.response = "Something went wrong, please try again.";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                objRes.isSuccess = false;
                objRes.response = "Something went wrong, please try again.";
            }
            return objRes;
        }
        public ReportIssueDomainModel GetIssueDescription(long ReportId, string Role)
        {
            ReportIssueDomainModel objRes = new ReportIssueDomainModel();
            try
            {
                objRes = objHelper.Query<ReportIssueDomainModel>("GetIssueDescription", new { issueid = ReportId, role = Role }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
            }
            return objRes;
        }
        public List<ReportIssueDomainModel> GetReportedIssueCount(long UserId, string Role)
        {
            List<ReportIssueDomainModel> listReportIssues = new List<ReportIssueDomainModel>();
            try
            {
                listReportIssues = objHelper.Query<ReportIssueDomainModel>("GetReportedIssueCount", new { @userid = UserId, role = Role }).ToList();
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
            }
            return listReportIssues;
        }
        public ResponseDomainModel ReportedIssueFeedBack(ReportIssueDomainModel model)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            try
            {
                var res = objHelper.Query<string>("ReportedIssueFeedBack", new
                {
                    issueid = model.ReportId,
                    speedofsolution = model.SpeedOfSolution,
                    Communication = model.Communication,
                    quality = model.Quality,
                    availability=model.Availability
                }).FirstOrDefault();
                if (res != null && res == "Update")
                {
                    objRes.isSuccess = false;
                    objRes.response = "Feedback saved successfully.";
                }
                else
                {
                    objRes.isSuccess = false;
                    objRes.response = "Something went wrong, please try again.";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                objRes.isSuccess = false;
                objRes.response = "Something went wrong, please try again.";
            }
            return objRes;
        }
        public ReportIssueDomainModel GetIssueFeedBack(long ReportId)
        {
            ReportIssueDomainModel objRes = new ReportIssueDomainModel();
            try
            {
                objRes = objHelper.Query<ReportIssueDomainModel>("GetIssueFeedBack", new { issueid = ReportId}).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
            }
            return objRes;
        }
    }
}

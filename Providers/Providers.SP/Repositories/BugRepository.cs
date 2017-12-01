using Providers.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Providers.Helper;
using DomainModel.EntityModel;

namespace Providers.Providers.SP.Repositories
{
    public class BugRepository : IBug
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        SqlHelper objHelper = new SqlHelper();
        public ResponseDomainModel AddUpdateBug(BugsDomainModel model)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            try
            {
                var res = objHelper.Query<string>("AddUpdateBug", new
                {
                    bugid = model.BugId,
                    userid = model.UserId,
                    bugreportedby = model.BugReportedBy,
                    projectid = model.ProjectId,
                    subject = model.BugSubject,
                    description=model.BugDescription,
                    file=model.Files,
                    priority=model.Priority,
                    status=model.Status
                }).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(res))
                {
                    objRes.isSuccess = false;
                    objRes.response = "Something went wrong, please try again.";
                }
                else if (res == "Insert")
                {
                    objRes.isSuccess = true;
                    objRes.response = "Bug added successfully.";
                }
                else if (res == "Update")
                {
                    objRes.isSuccess = true;
                    objRes.response = "Bug updated successfully.";
                }
                else if (res == "Change")
                {
                    objRes.isSuccess = false;
                    objRes.response = "Not a valid Request.";
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
                objRes.response = ex.Message;
            }
            return objRes;
        }
        public string AddBugFiles(BugFilesDomainModel model)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            try
            {
                var res = objHelper.Query<string>("AddBugFiles", new { files = model.Files }).FirstOrDefault();
                return res;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
        public string UpdateBugFiles(BugFilesDomainModel model)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            try
            {
                var res = objHelper.Query<string>("UpdateBugFiles", new { bugid = model.BugId, files = model.Files }).FirstOrDefault();
                return res;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
        public List<EmployeeDomainModel> GetEmployees()
        {
            try
            {
                var employees = objHelper.Query<EmployeeDomainModel>("GetEmpoyeeList", null).ToList();
                if (employees != null && employees.Count > 0)
                {
                    employees.ForEach(s => s.EmployeeName = s.FirstName + " " + s.LastName);
                }
                return employees;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
        public List<BugsDomainModel> GetBugDetails(BugsDomainModel model)
        {
            try
            {
                var listBugs = objHelper.Query<BugsDomainModel>("GetBugDetails_New", new { bugid = model.BugId, mode = model.Mode, userid = model.UserId, projectid = model.ProjectId }).ToList();
                return listBugs;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }

    }
}

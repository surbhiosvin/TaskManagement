using Providers.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.EntityModel;
using Providers.Helper;
using System.Data.SqlClient;

namespace Providers.Providers.SP.Repositories
{
    public class ProjectRepository : IProject, IDisposable
    {
        SqlHelper objHelper = new SqlHelper();
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<ProjectTypeDomainModel> GetProjectType()
        {
            try
            {
                var projectType = objHelper.Query<ProjectTypeDomainModel>("GetProjectType", null).ToList();
                return projectType;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public ProjectFullDetailsDomainModel GetProjectFullDetails(long ProjectId)
        {
            ProjectFullDetailsDomainModel objRes = new ProjectFullDetailsDomainModel();
            try
            {
                objRes = objHelper.Query<ProjectFullDetailsDomainModel>("GET_PROJECT_FULL_DETAILS_MAIN_NEW_MVC", new { ProjectId = ProjectId }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
            }
            return objRes;
        }
        public List<AddendumDomainModel> GetProjectAddendumDetails(long ProjectId)
        {
            List<AddendumDomainModel> listAddendums = new List<AddendumDomainModel>();
            try
            {
                listAddendums = objHelper.Query<AddendumDomainModel>("GetProjectAddendumDetails", new { ProjectId = ProjectId }).ToList();
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
            }
            return listAddendums;
        }
        public List<ProjectAssignToUserTotalWorkingHoursDomainModel> GetProjectAssignToUserWithTotalWorkingHours(long ProjectId = 0, long DepartmentId = 0, string Status = "")
        {
            List<ProjectAssignToUserTotalWorkingHoursDomainModel> listEmployeeHours = new List<ProjectAssignToUserTotalWorkingHoursDomainModel>();
            try
            {
                listEmployeeHours = objHelper.Query<ProjectAssignToUserTotalWorkingHoursDomainModel>("GET_PROJECT_ASSIGN_TO_USER_WITH_TOTAL_WORKING_HOURS_NEW", new
                {
                    projectid = ProjectId,
                    DepartmentId = DepartmentId,
                    Status = Status
                }).ToList();
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
            }
            return listEmployeeHours;
        }
        public List<ProjectDomainModel> GetProjectList()
        {
            List<ProjectDomainModel> listProjects = new List<ProjectDomainModel>();
            try
            {
                listProjects = objHelper.Query<ProjectDomainModel>("GetProjectList", null).ToList();
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
            }
            return listProjects;
        }
        public ResponseDomainModel AddProjectWorkingHoursBeforePms(ProjectWorkingHoursBeforePMSDomainModel model)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            try
            {
                var add = objHelper.Query<string>("ADD_PROJECT_WORKING_HOURS_BEFORE_PMS", new { projectid = model.ProjectId, Workinghours = model.WorkinghHours, createddate = model.CreatedDate, createdby = model.CreatedBy }).FirstOrDefault();
                if (add == "Insert")
                {
                    objRes.isSuccess = true;
                    objRes.response = "Working Hours saved Successfully.";
                }
                else if (add == "Update")
                {
                    objRes.isSuccess = true;
                    objRes.response = "Working Hours updated Successfully.";
                }
                else
                {
                    objRes.isSuccess = false;
                    objRes.response = "Working Hours not saved.";
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
        public ResponseDomainModel AddUpdateReportCategory(ProjectReportCategoryDomainModel model)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            try
            {
                var CategoryId = objHelper.ExecuteScalar("AddUpdateReportCategory", new
                {
                    CategoryId = model.CategoryId,
                    DepartmentId = model.DepartmentId,
                    CategoryName = model.CategoryName,
                    IsActive = model.IsActive,
                    CreatedDate = model.CreatedDate,
                    CreatedBy = model.CreatedBy
                });
                if (model.CategoryId > 0)
                {
                    objRes.isSuccess = true;
                    objRes.response = "Category updated successfully.";
                }
                else if (CategoryId > 0)
                {
                    objRes.isSuccess = true;
                    objRes.response = "Category added successfully.";
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
        public ResponseDomainModel ActivateDeactivateProjectReportCategory(long CategoryId, bool IsActive)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            try
            {
                var res = objHelper.Execute("ActivateDeactivateProjectReportCategory", new { CategoryId = CategoryId, IsActive = IsActive });
                if (res > 0)
                {
                    objRes.isSuccess = true;
                    objRes.response = "sucsess";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
            }
            return objRes;
        }
        public ResponseDomainModel DeleteProjectReportCategory(long CategoryId)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            try
            {
                var res = objHelper.Execute("DeleteProjectReportCategory", new { CategoryId = CategoryId });
                if (res > 0)
                {
                    objRes.isSuccess = true;
                    objRes.response = "sucsess";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
            }
            return objRes;
        }
        public List<ProjectReportCategoryDomainModel> GetProjectReportCategories()
        {
            List<ProjectReportCategoryDomainModel> listProjectReportCategories = new List<ProjectReportCategoryDomainModel>();
            try
            {
                listProjectReportCategories = objHelper.Query<ProjectReportCategoryDomainModel>("GetProjectReportCategories", null).ToList();
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
            }
            return listProjectReportCategories;
        }
        public ResponseDomainModel AddProjectTimeEstimation(AddendumDomainModel model)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            try
            {
                int ProjectTimeEstimation = objHelper.ExecuteScalar("AddProjectTimeEstimation", new
                {
                    @ProjectId = model.ProjectId,
                    EstimateHours = model.EstimateHours,
                    AssignHours = model.AssignHours,
                    UploadDocument = model.UploadDocument,
                    DeveloperCodingHours = model.DeveloperCodingHours,
                    WebServiceHours = model.WebServiceHours,
                    DesignHours = model.DesignHours,
                    QAHours = model.QAHours,
                    IsApprove = model.IsApprove,
                    HourlyRate = model.HourlyRate,
                    CreatedDate = model.CreatedDate
                });
                if (ProjectTimeEstimation > 0)
                {
                    objRes.isSuccess = true;
                    objRes.response = "Project Time Estimation saved successfully";
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
        public ResponseDomainModel MergeProject(long projectmergeto, long projectmergefrom)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            try
            {
                var merge = objHelper.Query<string>("MERGEPROJECT", new { projectmergeto = projectmergeto, projectmergefrom = projectmergefrom}).FirstOrDefault();
                var add= objHelper.Query<string>("ADD_MERGE_PROJECT_DETAILS", new { projectmergeto = projectmergeto, projectmergefrom = projectmergefrom }).FirstOrDefault();
                if (merge == "Successfull" && add== "Insert")
                {
                    objRes.isSuccess = true;
                    objRes.response = "Project Merged Successfully.";
                }
                else
                {
                    objRes.isSuccess = false;
                    objRes.response = "something wrong with database";
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
        public ResponseDomainModel AddUpdateProject(AddUpdateProjectDomainModel model)
        {
            ResponseDomainModel resp = new ResponseDomainModel();
            try
            {
                var response = objHelper.Query<ResponseDomainModel>("AddUpdateProjectAndEstimation", new
                {
                    ProjectId = model.ProjectId,
                    ClientId = model.ClientId,
                    ProjectTypeId = model.ProjectTypeId,
                    ProjectTitle = model.ProjectTitle,
                    UploadDetailsDocument = model.UploadDetailsDocument,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    ProjectUrl = model.ProjectUrl,
                    ProjectStatus = model.ProjectStatus,
                    Archived = model.Archived,
                    CreatedBy = model.CreatedBy,
                    UpworkProfile = model.upworkprofiles,
                    EstimateHours = model.EstimateHours.ToString(),
                    AssignHours = model.AssignedHours.ToString(),
                    UploadDocument = model.UploadDocument,
                    DeveloperCodingHours = model.developerCodinghours.ToString(),
                    WebServiceHours = model.WebserviceHours.ToString(),
                    DesignHours = model.EstimateDesignHours.ToString(),
                    QAHours = model.QualityAnalystHours.ToString(),
                    HourlyRate = model.HourlyRate,
                    CreatedDate = DateTime.Now
                }).First();
                return response;
            }
            catch(SqlException sq)
            {
                ErrorLog.LogError(sq);
                resp.response = sq.Message;
                return resp;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }

        public List<AllProjectsDomainModel> GetAllProjectsBasedOnStatus(GetAllProjectsDomainModel model)
        {
            try
            {
                var projects = objHelper.Query<AllProjectsDomainModel>("GetProjectAccordingToProjectStatusMode", new
                {
                    PageNumber = model.PageNumber,
                    PageSize = model.PageSize,
                    projecttitle = model.projecttitle,
                    startdate = model.startdate,
                    enddate = model.enddate,
                    projectstatusMode = model.projectstatusMode,
                    projecttype = model.projecttype
                }).ToList();
                return projects;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool UpdateProjectArchive(long projectid)
        {
            try
            {
                var result = objHelper.Query<string>("UpdateProjectArchive", new { projectid = projectid }).FirstOrDefault();
                if (result != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

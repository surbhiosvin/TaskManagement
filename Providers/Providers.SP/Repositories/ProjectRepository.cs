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
                ErrorLog.LogError(ex);
                return null;
            }
        }
        public ProjectFullDetailsDomainModel GetProjectFullDetails(long ProjectId)
        {
            try
            {
               var objRes = objHelper.Query<ProjectFullDetailsDomainModel>("GET_PROJECT_FULL_DETAILS_MAIN_NEW_MVC", new { ProjectId = ProjectId }).FirstOrDefault();
                return objRes;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }

        public GetProjectByIdDomainModel GetProjectDetailsById(long ProjectId)
        {
            try
            {
                var objRes = objHelper.Query<GetProjectByIdDomainModel>("GetProjectById", new { ProjectId = ProjectId }).FirstOrDefault();
                return objRes;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }

        public ResponseDomainModel UpdateProjectStatus(UpdateProjectStatusDomainModel model)
        {
            ResponseDomainModel responseModel = new ResponseDomainModel();
            try
            {
                var response = objHelper.Execute("UpdateProjectStatus", new { ProjectId = model.ProjectId, status = model.status, ProjectUrl = model.ProjectUrl });
                if (response > 0)
                {
                    responseModel.isSuccess = true;
                    responseModel.response = "Success";
                }
                return responseModel;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
        public List<AddendumDomainModel> GetProjectAddendumDetails(long ProjectId)
        {
            try
            {
               var listAddendums = objHelper.Query<AddendumDomainModel>("GetProjectAddendumDetails", new { ProjectId = ProjectId }).ToList();
                return listAddendums;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
        public List<ProjectAssignToUserTotalWorkingHoursDomainModel> GetProjectAssignToUserWithTotalWorkingHours(long ProjectId = 0, long DepartmentId = 0, string Status = "")
        {
            try
            {
               var listEmployeeHours = objHelper.Query<ProjectAssignToUserTotalWorkingHoursDomainModel>("GET_PROJECT_ASSIGN_TO_USER_WITH_TOTAL_WORKING_HOURS_NEW", new
                {
                    projectid = ProjectId,
                    DepartmentId = DepartmentId,
                    Status = Status
                }).ToList();
                return listEmployeeHours;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
        public List<ProjectDomainModel> GetProjectList()
        {
            try
            {
               var listProjects = objHelper.Query<ProjectDomainModel>("GetProjectList", null).ToList();
                return listProjects;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
        public ResponseDomainModel AddProjectWorkingHoursBeforePms(ProjectWorkingHoursBeforePMSDomainModel model)
        {
            try
            {
                ResponseDomainModel objRes = new ResponseDomainModel();
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
                return objRes;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
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
                return objRes;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
            
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
                    objRes.response = "success";
                }
                return objRes;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
        public ResponseDomainModel DeleteProjectReportCategory(long CategoryId)
        {
            try
            {
                ResponseDomainModel objRes = new ResponseDomainModel();
                var res = objHelper.Execute("DeleteProjectReportCategory", new { CategoryId = CategoryId });
                if (res > 0)
                {
                    objRes.isSuccess = true;
                    objRes.response = "success";
                }
                return objRes;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
        public List<ProjectReportCategoryDomainModel> GetProjectReportCategories()
        {
            try
            {
               var listProjectReportCategories = objHelper.Query<ProjectReportCategoryDomainModel>("GetProjectReportCategories", null).ToList();
                return listProjectReportCategories;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
        public ResponseDomainModel AddProjectTimeEstimation(AddendumDomainModel model)
        {
            try
            {
                ResponseDomainModel objRes = new ResponseDomainModel();
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
                return objRes;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
        public ResponseDomainModel MergeProject(long projectmergeto, long projectmergefrom)
        {
            try
            {
                ResponseDomainModel objRes = new ResponseDomainModel();
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
                return objRes;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
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

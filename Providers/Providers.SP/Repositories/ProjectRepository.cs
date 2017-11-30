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
            catch(Exception ex)
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

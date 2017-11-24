using Providers.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.EntityModel;
using Providers.Helper;

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
    }
}

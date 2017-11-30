using DomainModel.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Providers.Repositories
{
    public interface IProject
    {
        List<ProjectTypeDomainModel> GetProjectType();
        ProjectFullDetailsDomainModel GetProjectFullDetails(long ProjectId);
        List<AddendumDomainModel> GetProjectAddendumDetails(long ProjectId);
        List<ProjectAssignToUserTotalWorkingHoursDomainModel> GetProjectAssignToUserWithTotalWorkingHours(long ProjectId = 0, long DepartmentId = 0, string Status = "");
        List<ProjectDomainModel> GetProjectList();
        ResponseDomainModel AddProjectWorkingHoursBeforePms(ProjectWorkingHoursBeforePMSDomainModel model);
        ResponseDomainModel AddUpdateReportCategory(ProjectReportCategoryDomainModel model);
        ResponseDomainModel ActivateDeactivateProjectReportCategory(long CategoryId, bool IsActive);
        ResponseDomainModel DeleteProjectReportCategory(long CategoryId);
        List<ProjectReportCategoryDomainModel> GetProjectReportCategories();
        ResponseDomainModel AddProjectTimeEstimation(AddendumDomainModel model);
        ResponseDomainModel MergeProject(long projectmergeto, long projectmergefrom);
        ResponseDomainModel AddUpdateProject(AddUpdateProjectDomainModel model);
        List<AllProjectsDomainModel> GetAllProjectsBasedOnStatus(GetAllProjectsDomainModel model);
        bool UpdateProjectArchive(long projectid);
    }
}

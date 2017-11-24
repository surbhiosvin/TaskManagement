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
    }
}

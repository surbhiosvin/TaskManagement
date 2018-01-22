using DomainModel.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Providers.Repositories
{
    public interface IProfile
    {
        List<GetProfilesDomainModel> GetProfiles(long ProjectId);
        int? DeleteProfiles(string ProfileIds);
        ResponseDomainModel AddProfile(AddUpworkProfileDomainModel model);
        ResponseDomainModel UpdateProfile(UpdateUpworkProfileDomainModel model);
    }
}

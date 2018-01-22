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
    public class ProfileRepository : IProfile, IDisposable
    {
        SqlHelper objHelper = new SqlHelper();
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public List<GetProfilesDomainModel> GetProfiles(long ProjectId)
        {
            try
            {
                var profiles = objHelper.Query<GetProfilesDomainModel>("GetProfilesByProjectId", new { ProjectId = ProjectId }).ToList();
                return profiles;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int? DeleteProfiles(string ProfileIds)
        {
            try
            {
                var deletedRows = objHelper.Execute("DeleteProfiles", new { idsToDel = ProfileIds });
                return deletedRows;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ResponseDomainModel AddProfile(AddUpworkProfileDomainModel model)
        {
            ResponseDomainModel resp = new ResponseDomainModel();
            try
            {
                var response = objHelper.Query<ResponseDomainModel>("AddProfile", new
                {
                    ProfileName = model.ProfileName,
                    ProjectId = model.ProjectId,
                    ProjectTypeId = model.ProjectTypeId,
                    hourlyRate = model.hourlyRate,
                    loggedHours = model.loggedHours,
                    amount = model.amount,
                    createdBy = model.createdBy
                }).First();
                return resp = response;
            }
            catch (SqlException sq)
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

        public ResponseDomainModel UpdateProfile(UpdateUpworkProfileDomainModel model)
        {
            ResponseDomainModel resp = new ResponseDomainModel();
            try
            {
                var response = objHelper.Query<ResponseDomainModel>("UpdateProfile", new
                {
                    ProfileId = model.ProfileId,
                    ProfileName = model.ProfileName,
                    ProjectTypeId = model.ProjectTypeId,
                    hourlyRate = model.hourlyRate,
                    loggedHours = model.loggedHours,
                    amount = model.amount,
                    createdBy = model.createdBy
                }).First();
                return resp = response;
            }
            catch (SqlException sq)
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
    }
}

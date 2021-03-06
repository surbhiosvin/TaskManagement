﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.EntityModel;
using Providers.Helper;
using Providers.Repositories;

namespace Providers.Providers.SP.Repositories
{
    public class FeedbackRepository : IFeedback, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        SqlHelper objHelper = new SqlHelper();
        public List<FeedbackDomainModel> GetProjectFeedback(long ProjectId)
        {
            try
            {
                var listProjectFeedback = objHelper.Query<FeedbackDomainModel>("GetProjectFeedback", new { ProjectId = ProjectId }).ToList();
                return listProjectFeedback;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
        public List<FeedbackDomainModel> GetFeedList()
        {
            try
            {
               var listFeedback = objHelper.Query<FeedbackDomainModel>("GetAllFeedbacks", null).ToList();
                return listFeedback;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
        public List<FeedTypeDomainModel> GetAllFeedTypes()
        {
            try
            {
               var listFeedtypes = objHelper.Query<FeedTypeDomainModel>("GetAllFeedTypes", null).ToList();
                return listFeedtypes;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
        public ResponseDomainModel AddUpdateFeedback(FeedbackDomainModel model)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            try
            {
                int FeedbackId = objHelper.ExecuteScalar("AddUpdateFeedback", new
                {
                    FeedbackId = model.FeedbackId,
                    ProjectId = model.ProjectId,
                    UserId = model.UserId,
                    FeedTypeId = model.FeedTypeId,
                    Name = model.Name,
                    Description = model.Description,
                    Feed = model.Feed,
                    IsActive = model.IsActive,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                });
                if (model.FeedbackId > 0)
                {
                    objRes.isSuccess = true;
                    objRes.response = "Feedback updated successfully.";
                }
                else if (FeedbackId > 0)
                {
                    objRes.isSuccess = true;
                    objRes.response = "Feedback added successfully.";
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
        public ResponseDomainModel DeleteFeedback(long FeedbackId)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            try
            {
                var res = objHelper.Execute("DeleteFeedback", new { FeedbackId = FeedbackId });
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
        public ResponseDomainModel ActivateDeactivateFeedback(long FeedbackId, bool IsActive)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            try
            {
                var res = objHelper.Execute("ActivateDeactivateFeedback", new { FeedbackId = FeedbackId, IsActive = IsActive });
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
    }
}

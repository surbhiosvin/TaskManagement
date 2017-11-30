using DomainModel.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Providers.Repositories
{
    public interface IFeedback
    {
        List<FeedbackDomainModel> GetProjectFeedback(long ProjectId);
        List<FeedbackDomainModel> GetFeedList();
        List<FeedTypeDomainModel> GetAllFeedTypes();
        ResponseDomainModel AddUpdateFeedback(FeedbackDomainModel model);
        ResponseDomainModel DeleteFeedback(long FeedbackId);
        ResponseDomainModel ActivateDeactivateFeedback(long FeedbackId, bool IsActive);
    }
}

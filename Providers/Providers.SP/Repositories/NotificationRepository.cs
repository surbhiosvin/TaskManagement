using Providers.Helper;
using Providers.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.EntityModel;

namespace Providers.Providers.SP.Repositories
{
    public class NotificationRepository : INotification, IDisposable
    {
        SqlHelper objHelper = new SqlHelper();

        public List<EmployeeDomainModel> ProfileToCompleteNotifications()
        {
            try
            {
                var notifications = objHelper.Query<EmployeeDomainModel>("GetNotificationCount", null).ToList();
                return notifications;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<BirthDayNotificationDomainModel> BirthdayNotifications()
        {
            var notifications = objHelper.Query<BirthDayNotificationDomainModel>("GetUpComingEmployeeBirthday", null).ToList();
            return notifications;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}

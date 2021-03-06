﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.EntityModel;

namespace Providers.Repositories
{
    public interface INotification
    {
        List<EmployeeDomainModel> ProfileToCompleteNotifications();
        List<BirthDayNotificationDomainModel> BirthdayNotifications();
    }
}

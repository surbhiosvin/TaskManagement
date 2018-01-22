﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class WorkingHoursDomainModel
    {
        public string DepartmentName { get; set; }
        public string EmployeeName { get; set; }
        public long UserId { get; set; }
        public string WorkingHours { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

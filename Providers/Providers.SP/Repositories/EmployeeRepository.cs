using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.EntityModel;
using Providers.Repositories;

namespace Providers.Providers.SP.Repositories
{
    public class EmployeeRepository : IEmployee, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public UserDetails AuthenticateUser(UserDetails model)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

using Providers.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.EntityModel;
using Providers.Helper;

namespace Providers.Providers.SP.Repositories
{
    public class ClientRepository : IClient, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public List<ClientDomainModel> GetClients()
        {
            try
            {
                SqlHelper objHelper = new SqlHelper();
                var clients = objHelper.Query<ClientDomainModel>("Get_ClientNameList", null).ToList();
                return clients;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

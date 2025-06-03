using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementConsole
{
    public interface IHttpClientFactoryWrapper
    {
        HttpClient GetClient();
    }

}

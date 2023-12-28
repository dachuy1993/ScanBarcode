using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIApplication.Data
{
    public interface IAPIService
    {
        Task<HttpResponseMessage> Connect(string httpMethod, string url, dynamic data = null);
        string GetAPIUrl();
    }
}

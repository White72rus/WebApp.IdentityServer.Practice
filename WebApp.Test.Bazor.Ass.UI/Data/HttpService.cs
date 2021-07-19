using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Test.Bazor.Ass.UI.Data
{
    public class HttpService
    {
        IHttpContextAccessor _httpContextAccessor;
        public HttpService(IHttpContextAccessor httpContext)
        {
            _httpContextAccessor = httpContext;
        }

        public string Get() {
            try
            {
                string result = _httpContextAccessor.HttpContext.Request.Path.Value;
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            //return String.Empty;
        }
    }
}

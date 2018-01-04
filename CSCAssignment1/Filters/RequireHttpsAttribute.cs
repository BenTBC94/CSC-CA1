using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CSCAssignment1.Filters
{
    public class RequireHttpsAttribute : AuthorizationFilterAttribute
    {
        //public int Port { get; set; }

        //public RequireHttpsAttribute()
        //{
        //    Port = 44317;
        //}

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var request = actionContext.Request;

            if (request.RequestUri.Scheme != Uri.UriSchemeHttps) // if it's not using "https"
            {
                var response = new HttpResponseMessage();

                if (request.Method == HttpMethod.Get || request.Method == HttpMethod.Head)
                {
                    var uri = new UriBuilder(request.RequestUri);
                    uri.Scheme = Uri.UriSchemeHttps;
                    uri.Port = 44317;

                    response.StatusCode = HttpStatusCode.Found;
                    response.Headers.Location = uri.Uri;
                }
                else
                {
                    response.StatusCode = HttpStatusCode.Forbidden;
                }

                actionContext.Response = response;
            }
            else
            {
                base.OnAuthorization(actionContext); // if it's already using "https"
            }
        }
    }
}
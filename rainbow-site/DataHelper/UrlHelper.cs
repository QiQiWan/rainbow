using System.Text;
using Microsoft.AspNetCore.Http;

namespace rainbow_site
{
    public static class UrlHelper
    {
        public static string GetAbsoluteUri(this HttpRequest request)
        {
            return new StringBuilder()
             .Append(request.Scheme)
             .Append("://")
             .Append(request.Host)
             .Append(request.PathBase)
             .Append(request.Path)
             .Append(request.QueryString)
             .ToString();
        }
    }
}
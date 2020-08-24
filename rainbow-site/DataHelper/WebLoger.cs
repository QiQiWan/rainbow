using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace rainbow_site
{
    /// <summary>日志记录中间件类</summary>
    public class WebLogerMiddleWare
    {
        private readonly RequestDelegate _next;
        public WebLogerMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            HttpRequest request = context.Request;

            Loger.Log("当前请求地址为：" + UrlHelper.GetAbsoluteUri(request));

            return this._next(context);
        }
    }

    /// <summary>暴露日志记录中间件</summary>
    public static class WebLogerMiddlewareExtensions
    {
        public static IApplicationBuilder UseWebLoger(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<WebLogerMiddleWare>();
        }
    }
}
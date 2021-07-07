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

    public class Jump404Middleware
    {
        private readonly RequestDelegate next;

        public Jump404Middleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(Microsoft.AspNetCore.Http.HttpContext context)
        {
            // 让请求回来
            await next.Invoke(context);

            var response = context.Response;

            //如果是404就跳转到主页
            if (response.StatusCode == 404)
            {
                response.Redirect("/");
            }
        }
    }
    public static class Jump404MiddlewareExtension
    {
        public static void UseJump404(this IApplicationBuilder app)
        {
            app.UseMiddleware<Jump404Middleware>();
        }
    }
}
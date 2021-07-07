using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace rainbow_site
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }
            // else
            // {
            //     app.UseExceptionHandler("/Home/Error");
            // }

            app.UseExceptionHandler("/Home/Error");
            app.UseStatusCodePagesWithReExecute("/Home/Error");

            app.UseWebLoger();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            /// 记录请求地址中间件
            // app.Use(async (context, next) =>
            // {
            //     HttpRequest request = context.Request;
            //     Loger.Log("当前请求地址为：" + UrlHelper.GetAbsoluteUri(request));
            //     await next();
            // });
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Default",
                    template: "/{action}/{lang}",
                    new { controller = "Home", action = "Index", lang = "zh-cn" }
                );
                // add a new routing for Qixi
                routes.MapRoute(
                    name: "Qixi",
                    template: "Qixi/{action}",
                    new { Controller = "Qixi", action = "Index" }
                );
            });

        }
    }
}

using CCISWebCore.Models;
using CCISWebCore.Models.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace CCISWebCore
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppIdentityDbContext>(options =>
            options.UseSqlServer(
                _config["Data:DefaultConnection:ConnectionString"]));
            //注入 檔案上傳的類別
            //services.AddScoped<FileControlController>();

            services.AddIdentity<AppUser, IdentityRole>(opts =>
            {
                // 密碼強度配置
                // opts.Password.RequiredLength = 12; //要求長度
              //   opts.Password.RequireNonAlphanumeric = false;//密碼須包含不是數字或文字
                // opts.Password.RequireLowercase = false; //是否需要包含小寫字母(a-z).
                // opts.Password.RequireUppercase = false; //是否需要包含大寫字母(A-Z).
                // opts.Password.RequireDigit = true; //須包含數字

                // Lockout settings 帳戶鎖定機制                
                // opts.Lockout.AllowedForNewUsers = true; //確定是否可以鎖定新用戶。默認為true。
                opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); //發生鎖定時用戶鎖定的時間。默認為5分鐘。
                opts.Lockout.MaxFailedAccessAttempts = 3; //錯誤5次鎖定帳號

                // 用戶註冊設置
                //opts.User.RequireUniqueEmail = true; //是否Email地址必須唯一
                //opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";//用戶名可選字元（字母大小寫+數位+（-._@+））

                // 登陸配置
                //opts.SignIn.RequireConfirmedEmail = false;//需要確認的電子郵件登錄。默認為false。
                //opts.SignIn.RequireConfirmedPhoneNumber = false;//需要確認的電話號碼登錄。默認為false。  

            })
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                // options.Cookie.HttpOnly = true;
                // options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                // options.SlidingExpiration = true;

                //資安規定7.1 使用者的會談階段，設定該帳號在合理的時間(至多30分鐘)內未活動即自動失效
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                //帳號登入頁面路徑
                options.LoginPath = "/UserSignIn/TobeSignIn";
            });

            services.AddDbContext<dbCCISCoreContext>(options =>
                options.UseSqlServer(_config["Data:DefaultConnection:ConnectionString"]));

            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //開發用
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(new ExceptionHandlerOptions()
                {
                    ExceptionHandler = async context =>
                    {
                        bool isApi = Regex.IsMatch(context.Request.Path.Value, "^/api/", RegexOptions.IgnoreCase);
                        if (isApi)
                        {
                            context.Response.ContentType = "application/json";
                            var json = @"{ ""Message"": ""Internal Server Error"" }";
                            await context.Response.WriteAsync(json);
                            return;
                        }
                        context.Response.Redirect("/error");
                    }
                });
            }

            //當用戶輸入的網址找不到時
            app.UseStatusCodePagesWithRedirects("/NotFound");
            //強制使用 ASP.NET Core 中的 HTTPS
            //app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                //Path.Combine(env.ContentRootPath, @"node_modules")),
                _config["Data:FilePath:PhysicalPath"]),
                RequestPath = new PathString("/_FileUploads")
            });

            //app.UseStatusCodePages();

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                await next();
            });

            app.UseAuthentication();
            app.UseSession();
            // 舊版網頁 轉址 
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default1",
            //        template: "default.aspx",
            //        defaults: new { Controller = "Home", action = "index" });
            //});
   
            app.UseMvcWithDefaultRoute();
            //若無資料，先建立測試用資料
            //_ = SeedData.EnsurePopulated(app, _config);
            //AppIdentityDbContext.CreateAdminAccount(app.ApplicationServices, _config).Wait();
        }
    }
}

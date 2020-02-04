using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using CCISWebCore.Models.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace CCISWebCore.Models
{
    public static class SeedData
    {
        public static async Task EnsurePopulated(IApplicationBuilder app, IConfiguration configuration)
        {
            dbCCISCoreContext context =
                app.ApplicationServices.GetRequiredService<dbCCISCoreContext>();
            //創建資料庫
            context.Database.Migrate();
            AppIdentityDbContext aDbContext =
                app.ApplicationServices.GetRequiredService<AppIdentityDbContext>();
            //創建資料庫
            aDbContext.Database.Migrate();
            //context.Database.EnsureCreated();
            //若無資料，先建立測試用資料
            //if (!context.Infos.Any())
            //{
            //    context.Infos.AddRange(
            //        new Infos
            //        {
            //            Title = "標題一",
            //             Dtype = 1,
            //            HtmlContent = "<p>html內容</p>",
            //            CreateDate = DateTime.Parse("2018-01-01"),
            //        },
            //         new Infos
            //         {
            //             Title = "標題二",
            //             Dtype = 1,
            //             HtmlContent = "<p>html內容</p>",
            //             CreateDate = DateTime.Parse("2018-01-01"),
            //         },
            //         new Infos
            //         {
            //             Title = "標題三",
            //             Dtype = 1,
            //             HtmlContent = "<p>html內容</p>",
            //             CreateDate = DateTime.Parse("2018-01-01"),
            //         },
            //         new Infos
            //         {
            //             Title = "標題四",
            //             Dtype = 1,
            //             HtmlContent = "<p>html內容</p>",
            //             CreateDate = DateTime.Parse("2018-01-01"),
            //         },
            //         new Infos
            //         {
            //             Title = "標題五",
            //             Dtype = 1,
            //             HtmlContent = "<p>html內容</p>",
            //             CreateDate = DateTime.Parse("2018-01-01"),
            //         },
            //         new Infos
            //         {
            //             Title = "標題六",
            //             Dtype = 1,
            //             HtmlContent = "<p>html內容</p>",
            //             CreateDate = DateTime.Parse("2018-01-01"),
            //         },
            //         new Infos
            //         {
            //             Title = "標題七",
            //             Dtype = 1,
            //             HtmlContent = "<p>html內容</p>",
            //             CreateDate = DateTime.Parse("2018-01-01"),
            //         },
            //         new Infos
            //         {
            //             Title = "標題八",
            //             Dtype = 1,
            //             HtmlContent = "<p>html內容</p>",
            //             CreateDate = DateTime.Parse("2018-01-01"),
            //         },
            //         new Infos
            //         {
            //             Title = "標題九",
            //             Dtype = 1,
            //             HtmlContent = "<p>html內容</p>",
            //             CreateDate = DateTime.Parse("2018-01-01"),
            //         }
            //        );
            //    context.SaveChanges();
            //}

            UserManager<AppUser> userManager =
                app.ApplicationServices.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> roleManager =
                app.ApplicationServices.GetRequiredService<RoleManager<IdentityRole>>();
            string username = configuration["Data:AdminUser:Name"];
            string email = configuration["Data:AdminUser:Email"];
            string password = configuration["Data:AdminUser:Password"];
            string role = configuration["Data:AdminUser:Role"];

            if (await userManager.FindByNameAsync(username) == null)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
                AppUser user = new AppUser
                {
                    UserName = username,
                    Email = email
                };

                IdentityResult result = await userManager
                    .CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}

using Blog.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace Blog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

        try    
        { 
            
            var scope =  host.Services.CreateScope();

            var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            ctx.Database.EnsureCreated();


            var adminRole = new IdentityRole("Admin");
            if (!ctx.Roles.Any())
            {
                //create a role
                roleMgr.CreateAsync(adminRole).GetAwaiter().GetResult();
            }

            if (!ctx.Users.Any(u => u.UserName == "admin"))
            {
                //create an admin
                var adminUser = new IdentityUser
                {
                    UserName = "admin",
                    Email = "admin@test.com",
                };
                var result = userMgr.CreateAsync(adminUser, "password").GetAwaiter().GetResult();
                //add role to user
                userMgr.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
            }
        }
        catch(Exception ex)
        {
                Console.WriteLine(ex.Message);
        }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

using Microsoft.EntityFrameworkCore;

namespace GManage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<Models.AuthDbContext>(opts =>
                opts.UseNpgsql(builder.Configuration["DbConnString"]));
            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<Models.IAuthenticationCache, Models.AuthCache>();
            builder.Services.AddSingleton<Models.IAuthenticationWorker, Models.AuthWorker>();
            builder.Services.AddAuthentication("GManAuthFilter")
                .AddScheme<System.GManAuthOptions, System.GManAuthFilter>
                (
                "GManAuthFilter",
                opts =>
                    opts.ExpiresIn = 30
                );

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                //app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action}/{id}"); //,
                //defaults: new {  });

            app.Run();
        }
    }
}
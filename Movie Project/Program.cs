using Microsoft.EntityFrameworkCore;
using Data.Data;
using Microsoft.EntityFrameworkCore.SqlServer;
using Data.Repository;
using Microsoft.AspNetCore.Identity;

namespace Movie_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var connections = builder.Configuration.GetConnectionString("DefaultConnection");

            //builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connections)); 
            // below we had to add because all seperated with class library and need to specify correct project

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connections, b => b.MigrationsAssembly("Movie Project")));

            //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            // to work identiy we add below as identity will be in Razor pages 
            builder.Services.AddRazorPages();

            // builder.Services.AddScoped<IMovieRepository, MovieRepository>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            
       

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // checking if username and password is valid -- just before authorisation

            app.UseAuthorization();

            //this to add for identity as identity is in razor pages not MVC
            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
               // pattern: "{area=Admin}/{controller=Category}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

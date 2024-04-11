using DataLayer.Content;
using LearningWeb_Core.Convertors;
using LearningWeb_Core.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();
builder.Services.AddRazorPages();


#region Db Context

var configuration = builder.Configuration;
builder.Services.AddDbContext<SiteContext>(options =>
{
    //options.UseSqlServer(configuration["ConnectionStrings:EfCore"]);
    options.UseSqlServer(configuration.GetConnectionString("Learning_Site"));
});

#endregion

#region Ioc


builder.Services.AddTransient<IUserServices, UserServices>();
builder.Services.AddTransient<IViewRenderService, RenderViewToString>();
builder.Services.AddTransient<IPermitionServices, PermitionServices>();
builder.Services.AddTransient<ICourseService, CourseService>();
builder.Services.AddTransient<IOrderService, OrderService>();

#endregion

#region Authentication

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Account/Login";
        option.LogoutPath = "/Account/Logout";
        option.ExpireTimeSpan = TimeSpan.FromDays(10);
    });

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
       name: "areaRoute",
       pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

});


app.Run();

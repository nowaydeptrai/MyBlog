using Microsoft.EntityFrameworkCore;
using MyBlog.Application.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MyBlogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();




builder.Services.AddScoped<MyBlog.Application.Services.Interfaces.IPostService, MyBlog.Application.Services.PostService>();


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseDefaultFiles(new DefaultFilesOptions
{
    DefaultFileNames = new List<string> { "pages/index.html" }
});
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();


app.Run();

using DeveloperAssessment.Web.Interfaces;
using DeveloperAssessment.Web.Repository;
using DeveloperAssessment.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IPaginationService, PaginationService>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using dxxt.Data;
using dxxt.Repositories;
using dxxt.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<dxxtContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("dxxtContext") ?? throw new InvalidOperationException("Connection string 'dxxtContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<dxxtContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("dxxtContext"))
    );

builder.Services.AddScoped<ILecturerREPO, Lecturerrepo>();
builder.Services.AddScoped<ILecturerSERV, Lecturerserv>();

builder.Services.AddScoped<IScoreREPO, Scorerepo>();
builder.Services.AddScoped<IScoreSERV, Scoreserv>();

builder.Services.AddScoped<IStudentREPO, Studentrepo>();
builder.Services.AddScoped<IStudentSERV, Studentserv>();

builder.Services.AddScoped<ISubjectREPO, Subjectrepo>();
builder.Services.AddScoped<ISubjectSERV, Subjectserv>();

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

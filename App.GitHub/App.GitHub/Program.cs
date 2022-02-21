using App.GitHub.Services;
using App.GitHub.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IGitHubApiService, GitHubApiService>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddMemoryCache();

builder.Services.AddResponseCaching();
builder.Services.AddControllers(options =>
{
    options.CacheProfiles.Add("OneHour",
        new CacheProfile()
        {
            Duration = 3600
        });
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/erro/500");
    app.UseStatusCodePagesWithRedirects("/erro/{0}");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseResponseCaching();

app.UseRouting();

app.MapControllers();

app.UseAuthorization();

app.Run();
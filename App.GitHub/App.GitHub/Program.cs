using App.GitHub.Services;
using App.GitHub.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IGitHubApiService, GitHubApiService>();

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "Home.Index",
    pattern: "",
    defaults: new
    {
        controller = "Home",
        action = "Index"
    });
app.MapControllerRoute(
    name: "Home.UserDetails",
    pattern: "detalhes-do-usuario/{login}",
    defaults: new
    {
        controller = "Home",
        action = "UserDetails"
    });

app.Run();
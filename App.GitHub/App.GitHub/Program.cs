using App.GitHub.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IGitHubApiService, GitHubApiService>();

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
app.MapControllerRoute(
    name: "Home.UserRepositories",
    pattern: "repositorios-do-usuario/{login}",
    defaults: new
    {
        controller = "Home",
        action = "UserRepositories"
    });

app.Run();
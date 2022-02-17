using App.GitHub.Models;
using App.GitHub.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.GitHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGitHubApiService _gitHubApiService;

        public HomeController(IGitHubApiService gitHubApiService)
        {
            _gitHubApiService = gitHubApiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<GitHubUser> users = await _gitHubApiService.GetAllUsersAsync();

            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> UserDetails(string login)
        {
            GitHubUserDetails user = await _gitHubApiService.GetUserDetailsAsync(login);

            if (user == null)
            {
                return NotFound();
            }

            return PartialView("_UserDetails", user);
        }

        [HttpGet]
        public async Task<IActionResult> UserRepositories(string login)
        {
            IEnumerable<GitHubUserRepository> repositories = await _gitHubApiService.GetUserRepositoriesAsync(login);

            if (repositories == null)
            {
                return NotFound();
            }

            return PartialView("_UserRepositories", repositories);
        }
    }
}
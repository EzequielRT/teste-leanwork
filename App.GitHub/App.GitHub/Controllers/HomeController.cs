using App.GitHub.Models;
using App.GitHub.Services;
using App.GitHub.ViewModel;
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
            var user = await GetUserDetails(login);
            var repositories = await GetUserRepositories(login);

            var userViewModel = new UserDetailsViewModel
            {
                User = user,
                UserRepositories = repositories
            };

            if (userViewModel == null)
            {
                return NotFound();
            }

            return PartialView("_UserDetails", userViewModel);
        }        

        private async Task<GitHubUserDetails> GetUserDetails(string login)
        {
            var user = await _gitHubApiService.GetUserDetailsAsync(login);
            return user;
        }

        private async Task<IEnumerable<GitHubUserRepository>> GetUserRepositories(string login)
        {
            var repositories = await _gitHubApiService.GetUserRepositoriesAsync(login);
            return repositories;
        }
    }
}
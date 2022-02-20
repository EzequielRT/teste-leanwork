using App.GitHub.Services.Interfaces;
using App.GitHub.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace App.GitHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGitHubApiService _gitHubApiService;
        private readonly IMapper _mapper;

        public HomeController(IGitHubApiService gitHubApiService, IMapper mapper)
        {
            _gitHubApiService = gitHubApiService;
            _mapper = mapper;
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = _mapper.Map<IEnumerable<UserViewModel>>(await _gitHubApiService.GetAllUsersAsync());

            if (users == null) return NotFound();

            return View(users);
        }

        [Route("detalhes-do-usuario/{login}")]
        [HttpGet]
        public async Task<IActionResult> UserDetails(string login)
        {
            var userViewModel = await GetUserDetails(login, new UserDetailsViewModel());
            userViewModel.Repositories = await GetUserRepositories(login);

            if (userViewModel == null) return NotFound();

            return PartialView("_UserDetails", userViewModel);
        }        

        private async Task<UserDetailsViewModel> GetUserDetails(string login, UserDetailsViewModel userViewModel)
        {
            userViewModel = _mapper.Map<UserDetailsViewModel>(await _gitHubApiService.GetUserDetailsAsync(login));
            return userViewModel;
        }

        private async Task<IEnumerable<UserRepositoryViewModel>> GetUserRepositories(string login)
        {
            var repositories = _mapper.Map<IEnumerable<UserRepositoryViewModel>>(await _gitHubApiService.GetUserRepositoriesAsync(login));
            return repositories;
        }
    }
}
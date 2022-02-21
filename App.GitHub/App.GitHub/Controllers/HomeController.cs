using App.GitHub.Services.Interfaces;
using App.GitHub.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace App.GitHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGitHubApiUsers _gitHubApiUsers;
        private readonly IGitHubApiRepositories _gitHubApiRepositories;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public HomeController(IGitHubApiUsers gitHubApiUsers,
                              IGitHubApiRepositories gitHubApiRepositories,
                              IMemoryCache cache,
                              IMapper mapper)
        {
            _gitHubApiUsers = gitHubApiUsers;
            _gitHubApiRepositories = gitHubApiRepositories;
            _cache = cache;
            _mapper = mapper;
        }

        [Route("")]
        [ResponseCache(CacheProfileName = "OneHour")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = _mapper.Map<IEnumerable<UserViewModel>>(await _gitHubApiUsers.GetAllUsersAsync());

            if (users == null) return NotFound();

            return View(users);
        }

        [Route("carregar-mais-usuarios/{since:int}")]
        [HttpGet]
        public async Task<IActionResult> LoadMoreUsers(int since)
        {
            var users = await _cache.GetOrCreateAsync(since, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                return _mapper.Map<IEnumerable<UserViewModel>>(await _gitHubApiUsers.GetAllUsersAsync(since));
            });

            if (users == null) return NotFound();

            return PartialView("_AllUsers", users);
        }

        [Route("detalhes-do-usuario/{login}")]
        [HttpGet]
        public async Task<IActionResult> UserDetails(string login)
        {
            var cacheKey = login;
            UserDetailsViewModel userViewModel;

            if (!_cache.TryGetValue(login, out userViewModel))
            {
                var cacheOptions = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(30)
                };

                userViewModel = await GetUserDetails(login, new UserDetailsViewModel());
                userViewModel.Repositories = await GetUserRepositories(login);

                _cache.Set(cacheKey, userViewModel, cacheOptions);
            }

            if (userViewModel == null) return NotFound();

            return PartialView("_UserDetails", userViewModel);
        }

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Errors(int id)
        {
            var modelErro = new ErrorViewModel();

            switch (id)
            {
                case 500:
                    modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                    modelErro.Titulo = "Ocorreu um erro!";
                    modelErro.ErroCode = id;
                    break;
                case 404:
                    modelErro.Mensagem = "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte";
                    modelErro.Titulo = "Ops! Página não encontrada.";
                    modelErro.ErroCode = id;
                    break;
                default:
                    StatusCode(500);
                    break;
            }

            return View("Error", modelErro);
        }

        private async Task<UserDetailsViewModel> GetUserDetails(string login, UserDetailsViewModel userViewModel)
        {
            userViewModel = _mapper.Map<UserDetailsViewModel>(await _gitHubApiUsers.GetUserDetailsAsync(login));
            return userViewModel;
        }

        private async Task<IEnumerable<UserRepositoryViewModel>> GetUserRepositories(string login)
        {
            var repositories = _mapper.Map<IEnumerable<UserRepositoryViewModel>>(await _gitHubApiRepositories.GetUserRepositoriesAsync(login));
            return repositories;
        }
    }
}
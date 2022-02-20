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
                    return StatusCode(500);
                    break;
            }

            return View("Error", modelErro);
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
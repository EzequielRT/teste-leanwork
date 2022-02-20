using App.GitHub.Services.Models;
using App.GitHub.ViewModels;
using AutoMapper;

namespace App.GitHub.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<GitHubUser, UserViewModel>();
            CreateMap<GitHubUserDetails, UserDetailsViewModel>();
            CreateMap<GitHubUserRepository, UserRepositoryViewModel>();
        }
    }
}

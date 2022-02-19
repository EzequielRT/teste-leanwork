using App.GitHub.Models;

namespace App.GitHub.ViewModel
{
    public class UserDetailsViewModel
    {
        public GitHubUserDetails? User { get; set; }
        public IEnumerable<GitHubUserRepository>? UserRepositories { get; set; }
    }
}
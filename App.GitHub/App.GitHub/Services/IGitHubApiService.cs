using App.GitHub.Models;

namespace App.GitHub.Services
{
    public interface IGitHubApiService
    {
        public Task<IEnumerable<GitHubUser>> GetAllUsersAsync();
        public Task<GitHubUserDetails> GetUserDetailsAsync(string login);
        public Task<IEnumerable<GitHubUserRepository>> GetUserRepositoriesAsync(string login);
    }
}

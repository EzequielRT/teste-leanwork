using App.GitHub.Services.Models;

namespace App.GitHub.Services.Interfaces
{
    public interface IGitHubApiService
    {
        public Task<IEnumerable<GitHubUser>> GetAllUsersAsync();
        public Task<GitHubUserDetails> GetUserDetailsAsync(string login);
        public Task<IEnumerable<GitHubUserRepository>> GetUserRepositoriesAsync(string login);
    }
}

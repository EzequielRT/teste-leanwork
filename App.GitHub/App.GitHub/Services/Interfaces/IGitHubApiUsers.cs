using App.GitHub.Services.Models;

namespace App.GitHub.Services.Interfaces
{
    public interface IGitHubApiUsers
    {
        public Task<IEnumerable<GitHubUser>> GetAllUsersAsync();
        public Task<IEnumerable<GitHubUser>> GetAllUsersAsync(int since);
        public Task<GitHubUserDetails> GetUserDetailsAsync(string login);
    }
}

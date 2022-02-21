using App.GitHub.Services.Models;

namespace App.GitHub.Services.Interfaces
{
    public interface IGitHubApiRepositories
    {
        public Task<IEnumerable<GitHubUserRepository>> GetUserRepositoriesAsync(string login);
    }
}

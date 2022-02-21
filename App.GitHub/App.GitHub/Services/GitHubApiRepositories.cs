using App.GitHub.Services.Interfaces;
using App.GitHub.Services.Models;
using RestSharp;

namespace App.GitHub.Services
{
    public class GitHubApiRepositories : IGitHubApiRepositories
    {
        private readonly RestClient _client;

        public GitHubApiRepositories()
        {
            var options = new RestClientOptions("https://api.github.com/")
            {
                UserAgent = "AppGitHubZek/v1"
            };

            _client = new RestClient(options)
                .AddDefaultHeader(KnownHeaders.Accept, "application/vnd.github.v3+json");
        }        

        public async Task<IEnumerable<GitHubUserRepository>> GetUserRepositoriesAsync(string login)
        {
            var request = new RestRequest("users/{username}/repos", Method.Get)
                .AddParameter("per_page", "100")
                .AddUrlSegment("username", login);
            var response = await _client.ExecuteGetAsync<IEnumerable<GitHubUserRepository>>(request);
            IEnumerable<GitHubUserRepository>? repositories = response.Data;

            return repositories;
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}

using App.GitHub.Models;
using RestSharp;

namespace App.GitHub.Services
{
    public class GitHubApiService : IGitHubApiService
    {
        private readonly RestClient _client;

        public GitHubApiService()
        {
            var options = new RestClientOptions("https://api.github.com/")
            {
                UserAgent = "AppGitHubZek/v1"
            };

            _client = new RestClient(options)
                .AddDefaultHeader(KnownHeaders.Accept, "application/vnd.github.v3+json");
        }

        public async Task<IEnumerable<GitHubUser>> GetAllUsersAsync()
        {
            var request = new RestRequest("users", Method.Get)
                .AddParameter("per_page", "100");
            var response = await _client.ExecuteAsync<IEnumerable<GitHubUser>>(request);
            IEnumerable<GitHubUser> users = response.Data;

            return users;
        }

        public async Task<GitHubUserDetails> GetUserDetailsAsync(string login)
        {
            var request = new RestRequest("users/{username}", Method.Get)
                .AddUrlSegment("username", login);
            var response = await _client.ExecuteGetAsync<GitHubUserDetails>(request);
            GitHubUserDetails user = response.Data;

            return user;
        }

        public async Task<IEnumerable<GitHubUserRepository>> GetUserRepositoriesAsync(string login)
        {
            var request = new RestRequest("users/{username}/repos", Method.Get)
                .AddParameter("per_page", "100")
                .AddUrlSegment("username", login);
            var response = await _client.ExecuteGetAsync<IEnumerable<GitHubUserRepository>>(request);
            IEnumerable<GitHubUserRepository> repositories = response.Data;

            return repositories;
        }
    }
}

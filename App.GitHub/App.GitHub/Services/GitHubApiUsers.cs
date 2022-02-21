using App.GitHub.Services.Interfaces;
using App.GitHub.Services.Models;
using RestSharp;

namespace App.GitHub.Services
{
    public class GitHubApiUsers : IGitHubApiUsers
    {
        private readonly RestClient _client;

        public GitHubApiUsers()
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
            IEnumerable<GitHubUser>? users = response.Data;

            return users;
        }

        public async Task<IEnumerable<GitHubUser>> GetAllUsersAsync(int since)
        {
            var request = new RestRequest("users", Method.Get)
                .AddParameter("per_page", "100")
                .AddParameter("since", since);
            var response = await _client.ExecuteAsync<IEnumerable<GitHubUser>>(request);
            IEnumerable<GitHubUser>? users = response.Data;

            return users;
        }

        public async Task<GitHubUserDetails> GetUserDetailsAsync(string login)
        {
            var request = new RestRequest("users/{username}", Method.Get)
                .AddUrlSegment("username", login);
            var response = await _client.ExecuteGetAsync<GitHubUserDetails>(request);
            GitHubUserDetails? user = response.Data;

            return user;
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}

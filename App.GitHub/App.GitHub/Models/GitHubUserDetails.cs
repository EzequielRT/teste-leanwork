using System.Text.Json.Serialization;

namespace App.GitHub.Models
{
    public class GitHubUserDetails : GitHubUser
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("company")]
        public string? Company { get; set; }

        [JsonPropertyName("blog")]
        public Uri? Blog { get; set; }

        [JsonPropertyName("location")]
        public string? Location { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("hireable")]
        public bool? Hireable { get; set; }

        [JsonPropertyName("bio")]
        public string? Bio { get; set; }

        [JsonPropertyName("twitter_username")]
        public string? TwitterUsername { get; set; }

        [JsonPropertyName("public_repos")]
        public int? PublicRepos { get; set; }

        [JsonPropertyName("public_gists")]
        public int? PublicGists { get; set; }

        [JsonPropertyName("followers")]
        public long Followers { get; set; }

        [JsonPropertyName("following")]
        public long Following { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
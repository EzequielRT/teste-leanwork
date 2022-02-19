using System.Text.Json.Serialization;

namespace App.GitHub.Models
{
    public class GitHubUserRepository
    {
        [JsonPropertyName("id")]
        public long? Id { get; set; }

        [JsonPropertyName("node_id")]
        public string? NodeId { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("full_name")]
        public string? FullName { get; set; }

        [JsonPropertyName("private")]
        public bool? Private { get; set; }

        [JsonPropertyName("owner")]
        public GitHubUser? Owner { get; set; }

        [JsonPropertyName("html_url")]
        public Uri? HtmlUrl { get; set; }
    }
}

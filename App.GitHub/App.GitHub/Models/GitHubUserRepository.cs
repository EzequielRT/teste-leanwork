namespace App.GitHub.Models
{
    public class GitHubUserRepository
    {
        public int id { get; set; }
        public string name { get; set; }
        public GitHubUser owner { get; set; }
        public string html_url { get; set; }
    }
}

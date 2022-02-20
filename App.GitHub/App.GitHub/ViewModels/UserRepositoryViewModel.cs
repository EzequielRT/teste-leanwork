using System.ComponentModel;

namespace App.GitHub.ViewModels
{
    public class UserRepositoryViewModel
    {
        public long? Id { get; set; }

        [DisplayName("Nome")]
        public string? Name { get; set; }

        [DisplayName("Url")]
        public Uri? HtmlUrl { get; set; }
    }
}

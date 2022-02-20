namespace App.GitHub.ViewModels
{
    public class UserDetailsViewModel : UserViewModel
    {
        public DateTime? CreatedAt { get; set; }
        public IEnumerable<UserRepositoryViewModel>? Repositories { get; set; }
    }
}

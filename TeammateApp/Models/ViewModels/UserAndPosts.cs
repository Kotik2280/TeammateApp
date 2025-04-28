namespace TeammateApp.Models.ViewModels
{
    public class UserAndPosts
    {
        public User User { get; set; }
        public List<Post> Posts { get; set; }
        public UserAndPosts(User user, List<Post> posts)
        {
            User = user;
            Posts = posts;
        }
    }
}

namespace TeammateApp.Models.ViewModels
{
    public class PostsAndSort
    {
        public List<Post> Posts { get; set; }
        public SortMethods SortMethod { get; set; }
        public PostsAndSort(List<Post> posts, SortMethods sortMethod)
        {
            Posts = posts;
            SortMethod = sortMethod;
        }
    }

    public enum SortMethods
    {
        DateAscending,
        DateDescending
    }
}

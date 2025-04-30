using System.ComponentModel.DataAnnotations;

namespace TeammateApp.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Необходимо указать заголовок поста")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Заголовок не может быть короче 3 символов и длиннее 30")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Необходимо указать описание поста")]
        [StringLength(300, MinimumLength = 10, ErrorMessage = "Описание не может быть короче 10 символов и длиннее 300")]
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
    }
}

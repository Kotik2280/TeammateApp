using System.ComponentModel.DataAnnotations;

namespace TeammateApp.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Необходимо указать имя")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Имя не должно быть короче 3 символов и длиннее 30")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Необходимо указать пароль")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Пароль не должен быть короче 6 символов и длиннее 50")]
        public string Password { get; set; }
    }
}

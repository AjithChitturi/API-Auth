using System.ComponentModel.DataAnnotations;

namespace UserInterface.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User  Is Requried")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password Is Requried")]
        public string? Password { get; set; }

       
    }
}

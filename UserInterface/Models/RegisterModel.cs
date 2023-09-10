using System.ComponentModel.DataAnnotations;

namespace UserInterface.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Role is Required")]
        public string? Role { get; set; }

        [Required(ErrorMessage = "User Name is Required")]
        public string? UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is Required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string? Password { get; set; }
    }
}

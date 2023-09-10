using System.ComponentModel.DataAnnotations;

namespace UserInterface.Models
{
    public class OTPModel
    {
        [Required(ErrorMessage = "User  Is Requried")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "OTP Is Requried")]
        public string? OTP { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Authentication.Signup
{
    public class RegisterUser
    {
        [Required(ErrorMessage ="Username is Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password not match.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }

    }
}

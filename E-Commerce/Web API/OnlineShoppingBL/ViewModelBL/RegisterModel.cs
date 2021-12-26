using System.ComponentModel.DataAnnotations;

namespace OnlineShoppingBL.ViewModelBL
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "UserName Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Conform Password")]
        [Compare("Password")]
        public string Conform_Pwd { get; set; }

        //  public string Role { get; set; }
    }
}

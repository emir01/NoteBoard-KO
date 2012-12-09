using System.ComponentModel.DataAnnotations;

namespace NK.Web.ViewModels.Account
{
    /// <summary>
    /// The view model used on the UI page for user registration. 
    /// Contains several data anotations to controll the Required fields and the validity of the User Model
    /// </summary>
    public class UserRegisterViewModel
    {
        /// <summary>
        /// The required username
        /// </summary>
        [Required(ErrorMessage = "You must enter your desired username.")]
        public string Username { get; set; }

        /// <summary>
        /// The required initial password value
        /// </summary>
        [Required(ErrorMessage = "You must enter yourv desired password.")]
        public string Password { get; set; }

        /// <summary>
        /// The required second password value that must match the initial password
        /// </summary>
        [Required(ErrorMessage = "You must enter your desired password again.")]
        [Compare("Password",ErrorMessage = "The two passwords must match")]
        public string RepeatPassword { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace NK.Web.ViewModels.Account
{
    /// <summary>
    /// The User Login View Model used on the UI page for user authentication.
    /// View model is decorated with Data Anotations to ensure user model Validity.
    /// </summary>
    public class UserLoginViewModel
    {
        /// <summary>
        /// The required username value the user must enter to authenticate himself.
        /// </summary>
        [Required(ErrorMessage = "You must enter your username to log in.")]
        public string Username { get; set; }

        /// <summary>
        /// The required password value the user must enter to authenticate himself.
        /// </summary>
        [Required(ErrorMessage = "You must enter your password to log in.")]
        public string Password { get; set; }
    }
}
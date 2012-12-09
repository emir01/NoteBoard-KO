using System.Web.Mvc;
using NK.Services.Interface;
using NK.Web.Infrastructure.Authentication;
using NK.Web.Infrastructure.Mappers;
using NK.Web.ViewModels.Account;

namespace NK.Web.Controllers
{
    /// <summary>
    /// The account controller used for basic authentication and registration in the application
    /// </summary>
    public class AccountController : Controller
    {
        #region Properties

        /// <summary>
        ///  The user service providing access to the underlying user information in the system.
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// Wrapper around the authentication framework system used to save user authentication.
        /// </summary>
        private readonly IAuthenticationWrapper _authWrapper;

        #endregion

        #region Constructor 

        /// <summary>
        /// Creates Authentication Controllers using Structure Map(SM) to inject dependencies.
        /// </summary>
        /// <param name="userService">The SM injected use service</param>
        /// <param name="authWrapper">The SM injected authentication wrapper depending on the SM configuration</param>
        public AccountController(IUserService userService, IAuthenticationWrapper authWrapper)
        {
            _userService = userService;
            _authWrapper = authWrapper;
        }

        #endregion

        #region Actions

        #region LogIn
        
        //
        // GET : /Account/LogIn
        public ActionResult LogIn()
        {
            // We check if the user is currently authenticated/logged in.
            // We do not display the Login screen if the user is authenticated
            // TODO : Refactor this taking the authentication wrapper into consideration
            if(!User.Identity.IsAuthenticated)
            {
                // Case : User is not authenticated
                var loginViewModel = new UserLoginViewModel();
                return View(loginViewModel);
            }
            else
            {
                // Case : User is authenticated/logged in so we rediredt it
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // POST: /Account/LogIn
        [HttpPost]
        public ActionResult LogIn(UserLoginViewModel model)
        {
            // Check if the model state is valid based on the Data anotations for the view model
            if(ModelState.IsValid)
            {
                // Try and validate the user using the User Service
                var user = _userService.ValidateUser(model.Username, model.Password);

                // If the service returns a user object the validation/authentication is successful
                if(user != null)
                {
                    // TODO : Refactor this or totally remove the Authentication Wrapper
                    // Set the authentication wrapper helper, based on the used UI/Framework layer
                    _authWrapper.SetAuthenticationHelper(Response);
                    
                    // Sign in the user using the authentication wrapper
                    _authWrapper.SignIn(user.Username, false, user.Id);

                    // Redirect the user to the main application page after logging in
                    return RedirectToAction("Index", "Application");
                }
                else
                {
                    // If there is no user returned from the user service validation call,
                    // there is no user with the given credentials, or something went wrong.
                    ModelState.AddModelError("", "Wrong username or password. Please try again.");

                    return View(model);
                }
            }
            else
            {
                //Case: The model state is not valid
                return View(model);
            }
        }

        #endregion

        #region LogOut
        
        //
        // GET : /Account/LogOut
        public ActionResult LogOut()
        {
            // Sign out the user using the authentication wrapper
            _authWrapper.SignOut();
            
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Register

        //
        // GET : /Account/Register
        public ActionResult Register()
        {
            // Return the register view with an apropriate view model
            var registerViewModel = new UserRegisterViewModel();
            return View(registerViewModel);
        }

        //
        // Post : /Account/Register
        [HttpPost]
        public ActionResult Register(UserRegisterViewModel model)
        {
            // Check if the model state is valid based on the data anotations for the register view model
            if(ModelState.IsValid)
            {
                // Use the User Mapper to map from the Registration view model to the domain User object
                var domainUser = UserMapping.UserFromRegisterViewModel(model);

                // Try and register the user using the user service
                var createdUser = _userService.RegisterUser(domainUser);

                // If the Registration call to the service returns a user the registration was successful
                if(createdUser != null)
                {
                    // Correlctly registered, redirect to login page.
                    return RedirectToAction("LogIn");
                }
                else
                {
                    // The registration failed on the service layer, notify the user tring to register
                    ModelState.AddModelError("","Failed to register your account, please try again later.");
                    return View(model);
                }
            }
            else
            {
                // Something is not valid with the View Model, based on the anotations. Dispalys it.
                return View(model);
            }
        }

        #endregion

        #endregion
    }
}

using System;
using System.Web;
using System.Web.Security;

namespace NK.Web.Infrastructure.Authentication
{
    /// <summary>
    /// Basic Implementation of IAuthenticationWrapper using cookie authentication to store/save authentication state for the user
    /// </summary>
    public class CookieAuthentication : IAuthenticationWrapper
    {
        #region Properties

        private HttpResponseBase _response;

        #endregion

        #region Public Methods

        /// <summary>
        /// The basic method used to create the user authentication meta data and information.
        /// </summary>
        /// <param name="username">The username is the basic user information we want to store in the authentication metadata.</param>
        /// <param name="rememberMe">Boolean value indicating if we want to remember this user authentication on next visits.</param>
        /// <param name="userData">Generic optinal object containing extra data we might want to store</param>
        /// <returns>Boolean value indicating the success of the SignIn operation</returns>
        public bool SignIn(string username, bool rememberMe, object userData = null)
        {
            var cookie = FormsAuthentication.GetAuthCookie(username, rememberMe);
            var ticket = FormsAuthentication.Decrypt(cookie.Value);

            var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate,
                                                          ticket.Expiration, ticket.IsPersistent, userData.ToString(),
                                                          ticket.CookiePath);

            var encTicket = FormsAuthentication.Encrypt(newTicket);
            
            cookie.Value = encTicket;

            _response.Cookies.Add(cookie);
            
            return true;
        }

        /// <summary>
        /// The basic method used to sign out and remove the authentication for the currently authenticated user.
        /// </summary>
        /// <returns></returns>
        public bool SignOut()
        {
            FormsAuthentication.SignOut();
            return true;
        }

        /// <summary>
        /// The Authentication wrapper provides the clients callint it with a way to set an authentication
        /// helper from the client code to be used by the specific implementation of the Authentication wrapper.
        /// </summary>
        /// <param name="authenticationHelper">The authentication helper object that can be used in the specific authentication</param>
        public void SetAuthenticationHelper(object authenticationHelper)
        {
            // In the specific Cookie Authentication the Authentication helper 
            // is the response where we write the custom cookie information
            var response = authenticationHelper as HttpResponseBase;

            if(response == null)
            {
                throw new ArgumentException();
            }
            else
            {
                _response = response;
            }
        }
        
        #endregion
    }
}
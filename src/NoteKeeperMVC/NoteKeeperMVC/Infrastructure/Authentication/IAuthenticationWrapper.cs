namespace NK.Web.Infrastructure.Authentication
{
    /// <summary>
    /// Interface describing the basic authentication process methods.
    /// </summary>
    public interface IAuthenticationWrapper
    {
        /// <summary>
        /// The basic method used to create the user authentication meta data and information.
        /// </summary>
        /// <param name="username">The username is the basic user information we want to store in the authentication metadata.</param>
        /// <param name="rememberMe">Boolean value indicating if we want to remember this user authentication on next visits.</param>
        /// <param name="userData">Generic optinal object containing extra data we might want to store</param>
        /// <returns>Boolean value indicating the success of the SignIn operation</returns>
        bool SignIn(string username, bool rememberMe, object userData = null);

        /// <summary>
        /// The basic method used to sign out and remove the authentication for the currently authenticated user.
        /// </summary>
        /// <returns></returns>
        bool SignOut();

        /// <summary>
        /// The Authentication wrapper provides the clients callint it with a way to set an authentication
        /// helper from the client code to be used by the specific implementation of the Authentication wrapper.
        /// </summary>
        /// <param name="authenticationHelper">The authentication helper object that can be used in the specific authentication</param>
        void SetAuthenticationHelper(object authenticationHelper);
    }
}

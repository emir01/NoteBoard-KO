using NK.Model.Entities;

namespace NK.Services.Interface
{
    /// <summary>
    /// Interface used to manage the users of the application
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Function used to validate user credentials represented by a username and password
        /// </summary>
        /// <param name="username">The username that must be validated against the userbase</param>
        /// <param name="password">The password of the user we are trying to </param>
        /// <returns>Returns a <see cref="User"/> object if the credentials are valid</returns>
        User ValidateUser(string username, string password);

        /// <summary>
        /// Register a new user account in the system
        /// </summary>
        /// <param name="domainUser">The user object we will be registering in the system</param>
        /// <returns>The saved and store <see cref="User"/> object.</returns>
        User RegisterUser(User domainUser);
    }
}
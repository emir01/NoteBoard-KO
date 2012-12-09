using System;
using NK.Model.Entities;

namespace NK.Model.Repositories.Interface
{
    /// <summary>
    /// The repository used to access user information from the dababase using the EF context
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Returns a user with the given user id.
        /// </summary>
        /// <param name="userId">The id of the user we want to retrieve</param>
        /// <returns><see cref="User"/> object if a user with the given id exsists; null if there is no user with the given id.</returns>
        User GetUserById(Guid userId);

        /// <summary>
        /// Return a user with the given credentials
        /// </summary>
        /// <param name="username">The username of the user we want to retrieve</param>
        /// <param name="password">The password of the user we want to retrieve</param>
        /// <returns><see cref="User"/> object if a user with such credentials exsists; null if there is no user with such credentials</returns>
        User GetUserByCredentials(string username, string password);

        /// <summary>
        /// Store the passed in user object in the database. If the user exsists the information will be updated; if there is no user, a new user will be created.
        /// </summary>
        /// <param name="user"><see cref="User"/> object we want to store in the database. Can be a new user or a updated user</param>
        /// <returns>The stored user object with a valid unique id.</returns>
        User SaveUser(User user);
    }
}

using System;
using NK.Model.Entities;
using NK.Model.Repositories.Interface;
using NK.Services.Interface;

namespace NK.Services
{
    // The basc IUserService implementation providing functionality for managing the users in the application
    public class UserService : IUserService
    {
        #region  Properties

        #region Repositories
        
        private readonly IUserRepository _userRepository;

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Constructs the user service using SM to inject dependencies
        /// </summary>
        /// <param name="userRepository">The SM injected user repository dependency</param>
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion

        #region Public Methods

        #region Interface Implementation

        /// <summary>
        /// Used to validate user credentials represented by a username and password against the collection of registered users.
        /// </summary>
        /// <param name="username">The username that must be validated against the userbase.</param>
        /// <param name="password">The password of the user we are trying to validate.</param>
        /// <returns>Returns a <see cref="User"/> object if the credentials are valid; null if the credentials are not valid.</returns>
        public User ValidateUser(string username, string password)
        {
            try
            {
                var user = _userRepository.GetUserByCredentials(username, password);
                return user;
            }
            catch (Exception ex)
            {
                // If something goes wrong return a null value
                // that is the user credentials will not be validated
                return null;
            }
        }

        /// <summary>
        /// Register a new user in the userbase.
        /// </summary>
        /// <param name="domainUser">The user object that will be registered in the application userbase</param>
        /// <returns>The saved  <see cref="User"/> object.</returns>
        public User RegisterUser(User domainUser)
        {
            try
            {
                // Call the user repository Save method
                var user = _userRepository.SaveUser(domainUser);

                // return the user that it returns which could be null (indicating failiure)
                return user;
            }
            catch (Exception ex)
            {
                // return null for an exception
                return null;
            }
        }

        #endregion

        #endregion
    }
}

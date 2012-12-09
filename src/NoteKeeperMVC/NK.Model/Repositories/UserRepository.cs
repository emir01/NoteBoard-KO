using System;
using System.Data;
using System.Linq;
using NK.Model.Context;
using NK.Model.Entities;
using NK.Model.Repositories.Interface;

namespace NK.Model.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region Properties

        /// <summary>
        /// The note context used to talk to the udnerlying database
        /// </summary>
        private readonly NoteContext _noteContext;

        #endregion

        #region Constructor

        public UserRepository()
        {
            _noteContext = new NoteContext();
        }

        #endregion

        #region Public Methods

        #region IUserRepository implementation

        /// <summary>
        /// Returns a user with the given user id.
        /// </summary>
        /// <param name="userId">The id of the user we want to retrieve</param>
        /// <returns><see cref="User"/> object if a user with the given id exsists; null if there is no user with the given id.</returns>
        public User GetUserById(Guid userId)
        {
            var user = _noteContext.Users.FirstOrDefault(x => x.Id == userId);

            return user;
        }

        /// <summary>
        /// Return a user with the given credentials
        /// </summary>
        /// <param name="username">The username of the user we want to retrieve</param>
        /// <param name="password">The password of the user we want to retrieve</param>
        /// <returns><see cref="User"/> object if a user with such credentials exsists; null if there is no user with such credentials</returns>
        public User GetUserByCredentials(string username, string password)
        {
            // query the Users by username and password
            var user = _noteContext.Users.FirstOrDefault(x => x.Username == username && x.Password == password);

            // return the queried user or the default data type default value/reference
            return user;
        }

        /// <summary>
        /// Store the passed in user object in the database. If the user exsists the information will be updated; if there is no user, a new user will be created.
        /// </summary>
        /// <param name="user"><see cref="User"/> object we want to store in the database. Can be a new user or a updated user</param>
        /// <returns>The stored user object with a valid unique id.</returns>
        public User SaveUser(User user)
        {
            if (user.Id == Guid.Empty)
            {
                // set a new guid for the user and add it to the Users  in the note data context
                user.Id = Guid.NewGuid();
                user = _noteContext.Users.Add(user);
            }
            else
            {
                // the user has already been created so we will attach it to the users
                // and set its state to modified
                _noteContext.Users.Attach(user);
                _noteContext.Entry(user).State = EntityState.Modified;
            }

            // Save the changes and return the user object
            _noteContext.SaveChanges();
            return user;
        }

        #endregion

        #endregion
    }
}

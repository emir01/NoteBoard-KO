using NK.Model.Entities;
using NK.Web.ViewModels.Account;

namespace NK.Web.Infrastructure.Mappers
{
    /// <summary>
    /// Simple object mapper for the User entitiy and the various View Models related to the User entity.
    /// </summary>
    public static class UserMapping
    {
        public static User UserFromRegisterViewModel(UserRegisterViewModel registerViewModel)
        {
            var user = new User {Username = registerViewModel.Username, Password = registerViewModel.Password};

            return user;
        }
    }
}
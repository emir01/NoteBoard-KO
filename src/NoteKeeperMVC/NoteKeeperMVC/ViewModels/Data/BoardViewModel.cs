using System;

namespace NK.Web.ViewModels.Data
{
    /// <summary>
    /// The Board View Model class used to display Board entity Data on the client side (UI).
    /// </summary>
    public class BoardViewModel
    {
        #region Properties

        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public String BoardName { get; set; }

        /// <summary>
        /// For public boards indicates if the user has admin privilege to edit and save notes/name etc.
        /// </summary>
        public bool UserIsAdmin { get; set; }

        #endregion
    }
}
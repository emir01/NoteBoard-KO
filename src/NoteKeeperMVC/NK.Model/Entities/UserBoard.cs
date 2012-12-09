using System;
using System.ComponentModel.DataAnnotations;

namespace NK.Model.Entities
{
    /// <summary>
    /// Code first domain object for the mapping between Users and Boards, for 
    /// boards not primarly created by a given user. ( Public boards )
    /// </summary>
    public class UserBoard
    {
        /// <summary>
        /// The primary key
        /// </summary>
        [Key, Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Indicates wether the user has Admin roles for the board
        /// he does not own.
        /// </summary>
        public bool UserIsAdmin { get; set; }

        /// <summary>
        /// The user mapped in the relation
        /// </summary>
        [Required]
        public virtual User User { get; set; }

        /// <summary>
        /// The Board mapped in this relation
        /// </summary>
        [Required]
        public virtual Board Board { get; set; }
    }
}


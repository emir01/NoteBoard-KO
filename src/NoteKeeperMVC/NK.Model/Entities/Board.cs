using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NK.Model.Entities
{
    /// <summary>
    /// The Code First domain model for the Note-Boards. EF code first will create a database table based on this class
    /// </summary>
    public class Board
    {
        /// <summary>
        ///  The primary key of the entity, that will also be the primary key in the database table.
        /// </summary>
        [Key, Required]
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the Board
        /// </summary>
        public string BoardName { get; set; }

        /// <summary>
        /// Each board should have a user that is the owner of the board
        /// </summary>
        public virtual User Owner { get; set; }

        /// <summary>
        /// Each board  should have a collection of notes.
        /// </summary>
        public virtual ICollection<Note> Notes { get; set; }
    }
}

﻿using System;
using System.ComponentModel.DataAnnotations;

namespace NK.Model.Entities
{
    /// <summary>
    /// The Note entity class that will be used by EF Code first to generate the Note database table
    /// </summary>
    public class Note
    {
        /// <summary>
        /// The primary key of the entity as well as the table generated by EF code first.
        /// </summary>
        [Key,Required]
        public Guid Id { get; set; }

        /// <summary>
        /// The top coordinate of the note on the board. Used to remember note position on its parent board.
        /// </summary>
        public string Top { get; set; }

        /// <summary>
        /// The left coordinate of the note on the board. Used to remember note position on its parent board.
        /// </summary>
        public string Left { get; set; }

        /// <summary>
        /// The title of the note
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// The text of the note
        /// </summary>
        public String Text { get; set; }

        /// <summary>
        /// Each note must have a parent board. This reference will be used by EF code first to generate a foreigh key relation to the Board table.
        /// </summary>
        [Required]
        public virtual Board Board { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NK.Model.Entities
{
    /// <summary>
    /// Simple User entity class. Will be used by EF code first to generate the user database table
    /// </summary>
    public class User
    {
        /// <summary>
        /// The primary key of the entity and the database table generated by EF code first.
        /// </summary>
        [Key, Required]
        public Guid Id { get; set; }

        /// <summary>
        ///  The users username used to login in the application.
        /// </summary>
        public String Username { get; set; }

        /// <summary>
        /// The users password used to get authenticated in the application.
        /// </summary>
        public String Password { get; set; }

        /// <summary>
        /// Each user has a collection of boards. This will be used by EF code first to generate foreign key relations
        /// </summary>
        public virtual ICollection<Board> Boards { get; set; }
    }
}
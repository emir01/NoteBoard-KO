using System.Data.Entity;
using NK.Model.Entities;

namespace NK.Model.Context
{
    /// <summary>
    /// The DB Contextclass Used by Code First to generate functionality for managing and connecting to the database
    /// using EF.
    /// </summary>
    public class NoteContext:DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Board> Boards{ get; set; }
        public DbSet<UserBoard> UserBoards { get; set; }
    }
}

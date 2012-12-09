using System;
using System.Linq;
using NK.Model.Entities;

namespace NK.Model.Repositories.Interface
{
    /// <summary>
    /// The interface describing the basic note repository used to manage note data.
    /// </summary>
    public interface INoteRepository
    {
        /// <summary>
        /// Return a queryable list of all notes. This queryable can be used to further filter down notes 
        /// based on specific parameters.
        /// </summary>
        /// <returns>Queryable collection of Note objects</returns>
        IQueryable<Note> GetAll();

        /// <summary>
        ///  Return a single note based on the passed in note id.
        /// </summary>
        /// <param name="noteId">The id of the note that will be retrieved</param>
        /// <returns><see cref="Note"/> object of null if there is no such note.</returns>
        Note GetNote(Guid noteId);

        /// <summary>
        /// Stores the passed in note information. If a note exsists with the passed in note id it will get updated. If a note does not exsist 
        /// with the passed in note id or the id is Guid.Empty a new note will be created. 
        /// </summary>
        /// <param name="note">The note object that will be stored in the database, either created or updated information</param>
        /// <returns>The stored <see cref="Note"/> object</returns>
        Note SaveOrUpdateNote(Note note);

        /// <summary>
        /// Remove a note from the database with the given noteId
        /// </summary>
        /// <param name="noteId">The unique identifier of the note that will be removed.</param>
        /// <returns>Boolean value indicating the success of the operation.</returns>
        bool DeleteNote(Guid noteId);
    }
}

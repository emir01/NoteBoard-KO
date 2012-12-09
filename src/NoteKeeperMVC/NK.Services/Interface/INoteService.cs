using System;
using System.Collections.Generic;
using NK.Model.Entities;

namespace NK.Services.Interface
{
    /// <summary>
    /// The basic note service interface that describes the services for the management of note-boards and notes.
    /// </summary>
    public interface INoteService
    {
        #region Read
        
        /// <summary>
        /// Return all the boards for user with the given userId
        /// </summary>
        /// <param name="userId">The unique identifier of the user we want to get note-boards for</param>
        /// <returns>Enumerable list of <see cref="Board"/> objects</returns>
        IEnumerable<Board> GetBoardsForUser(Guid userId);

        /// <summary>
        /// Get all the notes for the board with the given board id.
        /// </summary>
        /// <param name="boardId">The id of the board for which we will be retrieving all the notes.</param>
        /// <returns> List of <see cref="Note"/> objects connected to a certain board. </returns>
        IEnumerable<Note> GetNotesForBoard(Guid boardId);

        #endregion

        #region Save And Update

        /// <summary>
        /// Stores the note object. If the note is already saved based on the unique id, it will get updated. If there is 
        /// no note with the given Id the note will be created.
        /// </summary>
        /// <param name="note">The note object that will be stored in the database.</param>
        /// <returns>The note object that was stored.</returns>
        /// <remarks>When a new note is created the Note object returned contains the stored unique id for the new note.</remarks>
        Note SaveOrUpdateNote(Note note);

        /// <summary>
        /// Stores the board object. If the board is already present it gets updated. If there is no such board the board will be created.
        /// </summary>
        /// <param name="board">The board object that will be stored.</param>
        /// <param name="userId"> </param>
        /// <returns>The stored board object</returns>
        /// <remarks>If the board object is newly created the board that will be returned will contain the unique id of the created board in the database.</remarks>
        Board SaveOrUpdateBoard(Board board, Guid userId);

        #endregion

        #region Delete

        /// <summary>
        /// Remove the note with the given noteId.
        /// </summary>
        /// <param name="noteId">The id of the note to be removed.</param>
        /// <returns>Boolean parameter indicating the result of the remove operation.</returns>
        bool DeleteNote(Guid noteId);

        /// <summary>
        /// Remove the board with the given boardId.
        /// </summary>
        /// <param name="boardId">The id of the board to be removed.</param>
        /// <returns>Boolean parameter indicating the result of the remove operation.</returns>
        bool DeleteBoard(Guid boardId);

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using NK.Model.Entities;
using NK.Model.Repositories.Interface;
using NK.Services.Interface;

namespace NK.Services
{
    /// <summary>
    /// The basic implementation of the INoteService interface implementing functionality for operation and management on
    /// user-boards and notes.
    /// </summary>
    public class NoteService : INoteService
    {
        #region Properties

        #region Repositories

        /// <summary>
        /// Used to access note data from the database
        /// </summary>
        private readonly INoteRepository _noteRepository;

        /// <summary>
        /// Used to access board data from the database.
        /// </summary>
        private readonly IBoardRepository _boardRepository;

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Constructs the Note service using Structure Map to inject the required repositories
        /// </summary>
        /// <param name="noteRepository">The SM Injected note repository.</param>
        /// <param name="boardRepository">The SM INjected board repository.</param>
        public NoteService(INoteRepository noteRepository, IBoardRepository boardRepository)
        {
            _noteRepository = noteRepository;
            _boardRepository = boardRepository;
        }

        #endregion

        #region Public Methods

        #region INoteService Implementation

        /// <summary>
        /// Return all the boards for the user with the given userId
        /// </summary>
        /// <param name="userId">The unique identifier of the user we want to get note-boards for</param>
        /// <returns>Enumerable list of <see cref="Board"/> objects</returns>
        public IEnumerable<Board> GetBoardsForUser(Guid userId)
        {
            try
            {
                // Get the user boards 
                var userBoards = _boardRepository.GetAll().Where(x => x.Owner.Id == userId);

                // Because the repo returns a IQueryable we enumerate it (call db here) and return a list enumerable
                return userBoards.ToList();
            }
            catch (Exception ex)
            {
                // If something goes wrong we just return an empty list of boards
                return new List<Board>();
            }
        }

        /// <summary>
        /// Get all the notes for the board with the given board id.
        /// </summary>
        /// <param name="boardId">The id of the board for which we will be retrieving all the notes.</param>
        /// <returns> List of <see cref="Note"/> objects connected to a certain board. </returns>
        public IEnumerable<Note> GetNotesForBoard(Guid boardId)
        {
            // if the board id is invalid or empty return a empty list of notes
            if (boardId == Guid.Empty)
            {
                return new List<Note>();
            }
            try
            {
                // get all the notes and filter them using linq to the notes for the given board.
                var notesForBoard = _noteRepository.GetAll().Where(x => x.Board.Id == boardId);

                return notesForBoard.ToList();
            }
            catch (Exception ex)
            {
                // If something ges wrong with the repo call we just return a empty list of notes
                return new List<Note>();
            }
        }

        /// <summary>
        /// Stores the note object. If the note is already saved based on the unique id, it will get updated. If there is 
        /// no note with the given Id the note will be created.
        /// </summary>
        /// <param name="note">The note object that will be stored in the database.</param>
        /// <returns>The note object that was stored.</returns>
        /// <remarks>When a new note is created the Note object returned contains the stored unique id for the new note.</remarks>
        public Note SaveOrUpdateNote(Note note)
        {
            try
            {
                // just call the repository SaveOrUpdate Method for the note
                var savedNote = _noteRepository.SaveOrUpdateNote(note);

                // Return the note we got back from the repo (can be null - failiure)
                return savedNote;
            }
            catch (Exception ex)
            {
                // If something goes wrong with the actual call to the repo, do not throw a exceptopn,
                //just return null
                return null;
            }

        }

        /// <summary>
        /// Stores the board object. If the board is already present it gets updated. If there is no such board the board will be created.
        /// </summary>
        /// <param name="board">The board object that will be stored.</param>
        /// <param name="userId"> </param>
        /// <returns>The stored board object</returns>
        /// <remarks>If the board object is newly created the board that will be returned will contain the unique id of the created board in the database.</remarks>
        public Board SaveOrUpdateBoard(Board board, Guid userId)
        {
            try
            {
                // Calling the board repo Save or Update methos
                var storedBoard = _boardRepository.SaveOrUpdateBoard(board, userId);

                // return the object we got back from the repo which can be null ( failiure )
                return storedBoard;
            }
            catch (Exception ex)
            {
                // If repo call goes wrong return null, instead of throwing an exception
                return null;
            }
        }

        /// <summary>
        /// Remove the note with the given noteId.
        /// </summary>
        /// <param name="noteId">The id of the note to be removed.</param>
        /// <returns>Boolean parameter indicating the result of the remove operation.</returns>
        public bool DeleteNote(Guid noteId)
        {
            try
            {
                // Call the repo Delete method
                var deleteSuccess = _noteRepository.DeleteNote(noteId);

                // Return the success indicator from the repo
                return deleteSuccess;
            }
            catch (Exception ex)
            {
                // Instead of throwing an exception return false
                return false;
            }

        }

        /// <summary>
        /// Remove the board with the given boardId.
        /// </summary>
        /// <param name="boardId">The id of the board to be removed.</param>
        /// <returns>Boolean parameter indicating the result of the remove operation.</returns>
        public bool DeleteBoard(Guid boardId)
        {
            try
            {
                // Call the repo delete method
                var deleteSuccess = _boardRepository.DeleteBoard(boardId);

                // Return the success indicator from the repo
                return deleteSuccess;
            }
            catch(Exception ex)
            {
                // Instead of throwing an exception return false
                // to indicate a failed operation
                return false;
            }
        }

        #endregion

        #endregion
    }
}

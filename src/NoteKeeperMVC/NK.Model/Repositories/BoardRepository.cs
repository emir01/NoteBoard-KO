using System;
using System.Data;
using System.Linq;
using NK.Model.Context;
using NK.Model.Entities;
using NK.Model.Repositories.Interface;

namespace NK.Model.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        #region Propeties

        private readonly NoteContext _noteContext;
        
        #endregion  

        #region Constructor

        public BoardRepository()
        {
            _noteContext = new NoteContext();
            // Disable proxy generation because of bug with EF 5 which is currently beeing used in the project.
            // The bug causes failiure whith json.
            _noteContext.Configuration.ProxyCreationEnabled = false;
        }

        #endregion

        #region Public Methods

        #region IBoardRepository Implementation

        /// <summary>
        /// Return a queryable list of all boards. This queryable list can be used to further filter down boards 
        /// based on specific parameters.
        /// </summary>
        /// <returns>Queryable collection of <see cref="Board"/> objects</returns>
        public IQueryable<Board> GetAll()
        {
            var boards = _noteContext.Boards;
            return boards;
        }

        /// <summary>
        ///  Return a single board based on the passed in boardId.
        /// </summary>
        /// <param name="boardId">The id of the board that will be retrieved</param>
        /// <returns><see cref="Board"/> object of null if there is no such board.</returns>
        public Board GetBoard(Guid boardId)
        {
            var note = _noteContext.Boards.FirstOrDefault(x => x.Id == boardId);
            return note;
        }

        /// <summary>
        /// Stores the passed in board information. If a board exsists with the passed in board id it will get updated. If a board does not exsist 
        /// with the passed in board id or the id is Guid.Empty a new board will be created. 
        /// </summary>
        /// <param name="board">The board object that will be stored in the database, either created or updated information</param>
        /// <param name="userId"> </param>
        /// <returns>The stored <see cref="Board"/> object</returns>
        public Board SaveOrUpdateBoard(Board board, Guid userId)
        {
            // Save a new board if the board id is an empty guid, which means
            // it hasnt been saved before
            if(board.Id == Guid.Empty)
            {
               board.Id = Guid.NewGuid();
               board.Owner = _noteContext.Users.FirstOrDefault(x => x.Id == userId);
               board = _noteContext.Boards.Add(board);
            }
            else
            {
                // if it has an id it has been added before so we attach it and set it in modified state
                board.Owner = _noteContext.Users.FirstOrDefault(x => x.Id == userId);
                _noteContext.Boards.Attach(board);
                _noteContext.Entry(board).State = EntityState.Modified;
            }

            // Either way we must save the changes to the context
            _noteContext.SaveChanges();
            return board;
        }

        /// <summary>
        /// Remove a board from the database with the given boardId
        /// </summary>
        /// <param name="boardId">The unique identifier of the board that will be removed.</param>
        /// <returns>Boolean value indicating the success of the operation.</returns>
        public bool DeleteBoard(Guid boardId)
        {
            // check if such a board exsists 
            var board = _noteContext.Boards.Include("Notes").FirstOrDefault(x => x.Id == boardId);

            if(board != null)
            {
                // if the board exsists (not null) remove it from the boards collection
                // and save the changes.
                
                // before removing the board remove all the notes
                _noteContext.Boards.Remove(board);
                _noteContext.SaveChanges();

                // If we get this far without failing return true
                return true;
            }
            // if the board does not exsist we still return true as the board with the given id does not exsist.
            return true;
        }

        #endregion

        #endregion
    }
}
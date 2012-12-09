using System;
using System.Linq;
using NK.Model.Entities;

namespace NK.Model.Repositories.Interface
{
    /// <summary>
    /// The interface describing the basic board repository used to manage board data.
    /// </summary>
    public interface IBoardRepository
    {
        /// <summary>
        /// Return a queryable list of all boards. This queryable list can be used to further filter down boards 
        /// based on specific parameters.
        /// </summary>
        /// <returns>Queryable collection of <see cref="Board"/> objects</returns>
        IQueryable<Board> GetAll();

        /// <summary>
        ///  Return a single board based on the passed in boardId.
        /// </summary>
        /// <param name="boardId">The id of the board that will be retrieved</param>
        /// <returns><see cref="Board"/> object of null if there is no such board.</returns>
        Board GetBoard(Guid boardId);

        /// <summary>
        /// Stores the passed in board information. If a board exsists with the passed in board id it will get updated. If a board does not exsist 
        /// with the passed in board id or the id is Guid.Empty a new board will be created. 
        /// </summary>
        /// <param name="board">The board object that will be stored in the database, either created or updated information</param>
        /// <param name="userId"> </param>
        /// <returns>The stored <see cref="Board"/> object</returns>
        Board SaveOrUpdateBoard(Board board, Guid userId);
        
        /// <summary>
        /// Remove a board from the database with the given boardId
        /// </summary>
        /// <param name="boardId">The unique identifier of the board that will be removed.</param>
        /// <returns>Boolean value indicating the success of the operation.</returns>
        bool DeleteBoard(Guid boardId);
    }
}

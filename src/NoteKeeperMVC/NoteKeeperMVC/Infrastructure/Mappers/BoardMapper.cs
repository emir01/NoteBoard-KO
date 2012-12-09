using System;
using NK.Model.Entities;
using NK.Web.ViewModels.Data;

namespace NK.Web.Infrastructure.Mappers
{
    /// <summary>
    /// Used to map between the Board entity and the various Board View Models
    /// </summary>
    public static class BoardMapper
    {
        /// <summary>
        /// Transforms the Board entity object to a Board View Model object
        /// </summary>
        /// <param name="board">The entity <see cref="Board"/> object we will transform to a view model.</param>
        /// <returns><see cref="BoardViewModel"/> object.</returns>
        public static BoardViewModel BoardToBoardViewModel(Board board)
        {
            var boardViewModel = new BoardViewModel()
                                     {
                                         BoardName = board.BoardName,
                                         Id = board.Id,
                                         UserId = board.Owner == null? Guid.Empty : board.Owner.Id
                                     };

            return boardViewModel;
        }

        /// <summary>
        /// Basicly transformthe board view model to the Board entity object.
        /// </summary>
        /// <param name="boardViewModel">The <see cref="BoardViewModel"/> object we will transform to a domain object.</param>
        /// <returns>The transformed <see cref="Board"/> object.</returns>
        public static Board BoardViewModelToBoard(BoardViewModel boardViewModel)
        {
            var board = new Board()
                            {
                                Id = boardViewModel.Id,
                                BoardName = boardViewModel.BoardName,
                            };
            return board;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe.GameEngine.Difficulties
{
    /// <summary>
    /// Makes random moves.
    /// </summary>
    internal class EasyDifficulty : IEngineDifficulty
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="board">The board that contains the current position.</param>
        /// <returns>A randomly picked move from the set of available moves.</returns>
        public Move? FindMoveForO(IBoard board)
        {
            return FindMove(board);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="board">The board that contains the current position.</param>
        /// <returns>A randomly picked move from the set of available moves.</returns>
        public Move? FindMoveForX(IBoard board)
        {
            return FindMove(board);
        }

        private Move? FindMove(IBoard board)
        {
            var moves = board.GetAvailableMoves().ToList();

            if (!moves.Any())
                return null;

            var randomIndex = new Random().Next(moves.Count);

            return moves[randomIndex];
        }
    }
}

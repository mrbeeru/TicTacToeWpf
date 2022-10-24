using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.GameEngine
{
    public interface IBoard
    {
        /// <summary>
        /// Evaluates the current board position.
        /// </summary>
        /// <returns>A value representing the current evaluation.</returns>
        int Evaluate();

        /// <summary>
        /// Gets all the empty board squares as moves.
        /// </summary>
        /// <returns>The list of all available board moves.</returns>
        IEnumerable<Move> GetAvailableMoves();

        /// <summary>
        /// Makes a move on the board.
        /// </summary>
        /// <param name="move">The move.</param>
        /// <param name="mark">The mark to set at move position.</param>
        /// <returns>The board.</returns>
        IBoard MakeMove(Move move, BoardMark mark);
    }
}

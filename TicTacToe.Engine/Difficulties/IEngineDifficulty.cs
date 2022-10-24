using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.GameEngine.Difficulties
{
    internal interface IEngineDifficulty
    {
        /// <summary>
        /// Finds a move for X for the given position.
        /// </summary>
        /// <param name="board">The board holds the current position.</param>
        /// <returns>A move.</returns>
        Move? FindMoveForX(IBoard board);

        /// <summary>
        /// Finds a move for O for the given position.
        /// </summary>
        /// <param name="board">The board holds the current position.</param>
        /// <returns>A move.</returns>
        Move? FindMoveForO(IBoard board);
    }
}

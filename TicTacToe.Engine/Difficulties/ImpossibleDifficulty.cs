using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe.GameEngine.Difficulties
{
    /// <summary>
    /// Finds the best move for X or O for a given position.
    /// </summary>
    internal class ImpossibleDifficulty : IEngineDifficulty
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="board">The board that contains the current position.</param>
        /// <returns>The best move from the set of available moves.</returns>
        public Move? FindMoveForO(IBoard board)
        {
            return FindMove(board, false);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="board">The board that contains the current position.</param>
        /// <returns>The best move from the set of available moves.</returns>
        public Move? FindMoveForX(IBoard board)
        {
            return FindMove(board, true);
        }

        private Move? FindMove(IBoard board, bool isMaximizing)
        {
            var moves = board.GetAvailableMoves();
            var moveToValueMap = new Dictionary<Move, int>();

            foreach (var move in moves)
            {
                board.MakeMove(new Move(move.X, move.Y), isMaximizing ? BoardMark.X : BoardMark.O);
                int moveValue = MinMax(board, !isMaximizing);
                board.MakeMove(new Move(move.X, move.Y), BoardMark.Empty);

                moveValue = isMaximizing ? moveValue : -moveValue;
                moveToValueMap.Add(move, moveValue);
            }

            // select randomly the best move if there are multiple best moves
            var bestMoves = moveToValueMap.GroupBy(x => x.Value)
                .OrderByDescending(x => x.Key)
                .FirstOrDefault()?
                .ToList();

            Random r = new Random();

            return bestMoves?[r.Next(bestMoves.Count)].Key;
        }


        private int MinMax(IBoard board, bool isMaximizing)
        {
            return MinMax(board, int.MinValue, int.MaxValue, isMaximizing);
        }

        private int MinMax(IBoard board, int alpha, int beta, bool isMaximizing)
        {
            var availableMoves = board.GetAvailableMoves();
            var score = board.Evaluate();

            //game finished
            if (!availableMoves.Any() || score != 0)
                return score;

            if (isMaximizing)
            {
                var maxEval = int.MinValue;

                foreach (var move in availableMoves)
                {
                    board.MakeMove(new Move(move.X, move.Y), BoardMark.X);

                    var eval = MinMax(board, alpha, beta, false);
                    maxEval = Math.Max(eval, maxEval);
                    alpha = Math.Max(eval, alpha);

                    board.MakeMove(new Move(move.X, move.Y), BoardMark.Empty);

                    if (beta <= alpha)
                        break;
                }

                return maxEval;
            }
            else
            {
                var minEval = int.MaxValue;

                foreach (var move in availableMoves)
                {
                    board.MakeMove(new Move(move.X, move.Y), BoardMark.O);

                    var eval = MinMax(board, alpha, beta, true);
                    minEval = Math.Min(eval, minEval);
                    beta = Math.Min(eval, beta);

                    board.MakeMove(new Move(move.X, move.Y), BoardMark.Empty);

                    if (beta <= alpha)
                        break;
                }

                return minEval;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.GameEngine
{
    public enum BoardMark
    {
        Empty,
        X,
        O
    }

    public class Board : IBoard
    {
        private BoardMark[,] GameBoard = new BoardMark[3, 3];

        public Board()
        {
            InitializeBoard();
        }

        public Board(BoardMark[,] gameBoard)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    GameBoard[i, j] = gameBoard[i, j];
                }
            }
        }

        /// <summary>
        /// 2D indexer
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="col">The column.</param>
        /// <returns>The square value at specified position.</returns>
        public BoardMark this[int row, int col]
        {
            get => GameBoard[row, col];
        }

        /// <summary>
        /// 1D indexer.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The square value at specified position.</returns>
        public BoardMark this[int index]
        {
            get => GameBoard[index/3, index%3];
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns>All available(empty) board squares where a move can be made by X or O</returns>
        public IEnumerable<Move> GetAvailableMoves()
        {
            var output = new List<Move>();

            for (int i = 0; i < 3; i ++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (GameBoard[i, j] == BoardMark.Empty)
                        output.Add(new Move(i, j));
                }
            }

            return output;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns>1 if X won, -1 if O won, 0 if draw</returns>
        public int Evaluate()
        {
            static int eval(BoardMark mark) => mark == BoardMark.X ? 1 : -1;

            for (int i = 0; i < 3; i++)
            {
                //check rows
                if (GameBoard[i, 0] == GameBoard[i, 1] && 
                    GameBoard[i, 1] == GameBoard[i, 2] &&
                    GameBoard[i, 2] != BoardMark.Empty)
                {
                    return eval(GameBoard[i, 0]);
                }

                //check cols
                if (GameBoard[0, i] == GameBoard[1, i] && 
                    GameBoard[1, i] == GameBoard[2, i] &&
                    GameBoard[2, i] != BoardMark.Empty)
                {
                    return eval(GameBoard[0, i]);
                }
            }

            //check diagonals
            if (GameBoard[0,0] == GameBoard[1,1] && 
                GameBoard[1,1] == GameBoard[2,2] && 
                GameBoard[2,2] != BoardMark.Empty)
            {
                return eval(GameBoard[0, 0]);
            }

            if (GameBoard[0, 2] == GameBoard[1, 1] && 
                GameBoard[1, 1] == GameBoard[2, 0] && 
                GameBoard[2, 0] != BoardMark.Empty)
            {
                return eval(GameBoard[0, 2]);
            }

            //draw
            return 0;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="move">The move.</param>
        /// <param name="mark">The mark.</param>
        /// <returns>The board.</returns>
        public IBoard MakeMove(Move move, BoardMark mark)
        {
            GameBoard[move.X, move.Y] = mark;
            return this;
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < GameBoard.LongLength; i++)
            {
                GameBoard[i/3, i%3] = BoardMark.Empty;
            }
        }

        public override string ToString()
        {
            string s = "";

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    s += GameBoard[i, j] == BoardMark.X ? " X " : GameBoard[i, j] == BoardMark.O ? " O " : "_ ";
                }

                s += Environment.NewLine;
            }

            return s;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.GameEngine;

namespace TicTacToe.TttGame
{
    public enum GameMode
    {
        HumanVsComputer,
        ComputerVsHuman,
        HumanVsHuman
    }

    internal class Game
    {
        private readonly Board board;
        private readonly Engine engine;
        private readonly Player playerX;
        private readonly Player playerO;
        private Player currentTurn;
        private bool isFinished = false;

        public event EventHandler<GameResult>? GameFinished;

        public Game(Player playerX, Player playerO)
        {
            board  = new Board();
            engine = new Engine().SetDifficulty(EngineStrength.Easy);
            currentTurn = playerX;
            this.playerX = playerX;
            this.playerO = playerO;
        }

        public bool CanMove(Move move)
        {
            return board[move.X, move.Y] == BoardMark.Empty;
        }

        public bool CanMove(int index)
        {
            return board[index] == BoardMark.Empty;
        }

        public void MakeMove(Move? move)
        {
            if (move == null)
                return;

            if (isFinished)
                return;

            if (currentTurn == playerX)
            {
                board.MakeMove(move, BoardMark.X);
                currentTurn = playerO;
            }
            else if (currentTurn == playerO)
            {
                board.MakeMove(move, BoardMark.O);
                currentTurn = playerX;
            }
            else 
                throw new Exception("Don't know which player's turn is.");

            EvaluatePosition();
        }

        public void MakeMove(int index)
        {
            var move = new Move(index / 3, index % 3);
            MakeMove(move);
        }

        public int EvaluatePosition()
        {
            var evaluation = board.Evaluate();
            var movesStillAvailable = board.GetAvailableMoves().Any();

            isFinished = evaluation != 0 || !movesStillAvailable;
            if (isFinished)
            {
                UpdateScore(evaluation);

                var winner = evaluation > 0 ? BoardMark.X :
                             evaluation < 0 ? BoardMark.O : BoardMark.Empty;

                GameFinished?.Invoke(this, new GameResult() { Winner = winner});
            }

            return evaluation;
        }

        public Move? FindMove()
        {
            if (currentTurn == playerX)
                return engine.FindMoveForX(board);
            else 
                return engine.FindMoveForO(board);
        }

        public bool IsFinished()
        {
            return isFinished;
        }

        public Player? GetWinner()
        {
            if (!IsFinished())
                return null;

            var evaluation = EvaluatePosition();

            if (evaluation == 0)
                return null;

            if (evaluation > 0)
                return playerX;

            if (evaluation < 0)
                return playerO;

            throw new Exception("Can't get winner.");
        }

        public void SetDifficulty(EngineStrength difficulty)
        {
            engine.SetDifficulty(difficulty);
        }

        public Board GetBoard()
        {
            return board;
        }

        private void UpdateScore(int evaluation)
        {
            if (evaluation > 0)
                playerX.Score++;

            if (evaluation < 0)
                playerO.Score++;
        }
    }
}

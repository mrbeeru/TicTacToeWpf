using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.GameEngine.Difficulties;

namespace TicTacToe.GameEngine
{
    public enum EngineStrength
    {
        Easy,
        Impossible
    }

    public class Engine
    {
        private IEngineDifficulty engineDifficulty;

        public Engine()
        {
            //default engine difficulty
            engineDifficulty = new EasyDifficulty();
        }

        /// <summary>
        /// Finds a move for X based on current position.
        /// </summary>
        /// <param name="board">The board.</param>
        /// <returns></returns>
        public Move? FindMoveForX(IBoard board)
        {
            return engineDifficulty.FindMoveForX(board);
        }

        public Move? FindMoveForO(IBoard board)
        {
            return engineDifficulty.FindMoveForO(board);
        }

        public Engine SetDifficulty(EngineStrength strength)
        {
            switch (strength) 
            {
                case EngineStrength.Easy:
                    engineDifficulty = new EasyDifficulty();
                    break;

                case EngineStrength.Impossible:
                    engineDifficulty = new ImpossibleDifficulty();
                    break;
            }

            return this;
        }
    }
}

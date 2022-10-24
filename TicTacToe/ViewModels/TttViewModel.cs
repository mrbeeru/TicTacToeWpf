using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using TicTacToe.GameEngine;
using TicTacToe.TttGame;

namespace TicTacToe.ViewModels
{
    public class TttViewModel : INotifyPropertyChanged
    {
        private Game game;
        private bool isGameFinished;
        private EngineStrength difficulty;
        private GameMode gameMode;

        public Player? PlayerX { get; set; }
        public Player? PlayerO { get; set; }
        public UiBoard UiBoard { get; set; } = new UiBoard();
        public GameResult? GameResult { get; set; }
        public ICommand ButtonClickedCommand { get; set; }
        public ICommand NewGameCommand { get; set; }

        public EngineStrength Difficulty
        {
            get => difficulty;
            set
            {
                difficulty = value;
                game.SetDifficulty(difficulty);
                NotifyPropertyChanged(nameof(Difficulty));
            }
        }

        public GameMode GameMode
        {
            get => gameMode;
            set
            {
                gameMode = value;
                NewGame();
            }
        }

        public bool IsGameFinished
        {
            get => isGameFinished;
            set { isGameFinished = value; NotifyPropertyChanged(nameof(IsGameFinished)); }
        }


        public TttViewModel()
        {
            NewGame();

            ButtonClickedCommand = new MvxCommand<int>(HandleClick);
            NewGameCommand = new MvxCommand(NewGame);
        }


        public void NewGame()
        {
            PlayerX ??= new Player();
            PlayerO ??= new Player();

            if (game != null)
                game.GameFinished -= Game_GameFinished;

            game = new Game(PlayerX, PlayerO);
            game.SetDifficulty(Difficulty);
            game.GameFinished += Game_GameFinished;

            UiBoard.Reset();
            IsGameFinished = false;

            if (GameMode == GameMode.ComputerVsHuman)
            {
                // Computer starts first => make a move
                var move = game.FindMove();
                game.MakeMove(move);
                UiBoard.Update(game.GetBoard());
            }
        }

        private void Game_GameFinished(object? sender, GameResult gameResult)
        {
            GameResult = gameResult;
            NotifyPropertyChanged(nameof(GameResult));
        }

        void HandleClick(int index)
        {
            if (game.IsFinished())
            {
                IsGameFinished = true;
                return;
            }

            if (!game.CanMove(index))
                return;

            game.MakeMove(index);
            UiBoard.Update(game.GetBoard());

            if (game.IsFinished())
            {
                IsGameFinished = true;
                return;
            }

            // no computer move in this game mode
            if (GameMode == GameMode.HumanVsHuman)
                return;

            var move = game.FindMove();

            if (move != null)
            {
                game.MakeMove(move);
                UiBoard.Update(game.GetBoard());
            }

            if (game.IsFinished())
            {
                IsGameFinished = true;
                return;
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

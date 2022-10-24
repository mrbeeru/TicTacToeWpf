using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.GameEngine;

namespace TicTacToe.TttGame
{
    public class GameResult : INotifyPropertyChanged
    {
        private BoardMark winner;
        public BoardMark Winner 
        {
            get => winner; 
            set { winner = value; NotifyPropertyChanged(nameof(Winner)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

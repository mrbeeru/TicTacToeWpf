using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.TttGame
{
    public class Player : INotifyPropertyChanged
    {
        private int score;

        public int Score
        {
            get => score;
            set { score = value; NotifyPropertyChanged(nameof(Score)); }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

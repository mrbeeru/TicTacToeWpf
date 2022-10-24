using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.GameEngine;
using TicTacToe.TttGame;

namespace TicTacToe
{
    public class UiBoard
    {
        public ObservableCollection<UiBoardSquare> BoardSquares { get; set; } = new ObservableCollection<UiBoardSquare>();

        public UiBoard()
        {
            int bt = 4; // border thickness
            BoardSquares.Add(new UiBoardSquare() { BorderThickness = $"0 0 {bt} {bt}"});
            BoardSquares.Add(new UiBoardSquare() { BorderThickness = $"{bt} 0 {bt} {bt}"});
            BoardSquares.Add(new UiBoardSquare() { BorderThickness = $"{bt} 0 0 {bt}"});
            BoardSquares.Add(new UiBoardSquare() { BorderThickness = $"0 {bt} {bt} {bt}"});
            BoardSquares.Add(new UiBoardSquare() { BorderThickness = $"{bt} {bt} {bt} {bt}"});
            BoardSquares.Add(new UiBoardSquare() { BorderThickness = $"{bt} {bt} 0 {bt}"});
            BoardSquares.Add(new UiBoardSquare() { BorderThickness = $"0 {bt} {bt} 0"});
            BoardSquares.Add(new UiBoardSquare() { BorderThickness = $"{bt} {bt} {bt} 0"});
            BoardSquares.Add(new UiBoardSquare() { BorderThickness = $"{bt} {bt} 0 0"});
        }

        public void Update(Board board)
        {
            for (int i = 0; i < BoardSquares.Count; i++)
            {
                var square = BoardSquares[i];
                square.Mark = board[i];
            }
        }

        public void Reset()
        {
            foreach (var square in BoardSquares)
                square.Mark = BoardMark.Empty;
        }
    }

    public class UiBoardSquare : INotifyPropertyChanged
    {
        private BoardMark mark = BoardMark.Empty;

        public string BorderBrush { get; set; } = "Aqua";
        public string BorderThickness { get; set; } = "0 0 0 0";

        public BoardMark Mark
        {
            get => mark;
            set { mark = value; NotifyPropertyChanged(nameof(Mark)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}

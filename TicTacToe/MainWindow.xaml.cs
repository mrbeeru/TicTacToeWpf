using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TicTacToe.TttGame;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Board b = new Board();

            //b.MakeMove(new Move(0,2, BoardMark.X));

            //b.MakeMove(new Move(0,0, BoardMark.O));

            //b.MakeMove(new Move(2,2, BoardMark.O));

            //Engine e = new Engine();
            //var move = e.FindBestMoveForX(b);
            //var move2 = e.FindBestMoveForO(b);

            //var s = b.ToString();
        }
    }
}

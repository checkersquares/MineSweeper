using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace MineSweeper_V2
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Board _board;
        public Board GameBoard
        {
            get
            {
                return _board;
            }
            set
            {
                _board = value;
                OnPropertyChanged(nameof(GameBoard)); 
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            startNewGame();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void startNewGame(int size = 8, int bombs = 0)
        {
            GameBoard = new Board(size, bombs);
           
            DataContext = GameBoard;
            grdBoard.Rows = size;
            grdBoard.Columns = size;
            grdBoard.IsEnabled = true;

            grdBoard.Children.Clear();
            foreach (Cell c in GameBoard.Cells)
            {
                CellModel cm = new CellModel(c);
                grdBoard.Children.Add(cm);
                cm.CellFontSize = grdBoard.Height;
            }
        }

        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings(this);
            settings.ShowDialog();

            if (settings.DialogResult == true)
            {
                startNewGame(settings.Size, settings.Bombs);
            }
        }

        private void btnFinish_Click(object sender, RoutedEventArgs e)
        {
            GameBoard.finish();
            grdBoard.IsEnabled = false;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Width < Height - 70)
            {
                grdBoard.Height = grdBoard.Width = Width - 20;
            }
            else
            {
                grdBoard.Height = grdBoard.Width = Height - 90;
            }
            foreach (CellModel cm in grdBoard.Children)
            {
                cm.CellFontSize = grdBoard.Height;
            }
        }
    }
}

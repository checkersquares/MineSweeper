using System.ComponentModel;
using System.Windows;

namespace MineSweeper_V2
{

    public class Cell: INotifyPropertyChanged
    {
        public Board Board { get; set; } = new Board();
        public int SurroundingBombs { get; set; } = 0;
        public bool IsBomb { get; set; } = false;
        private bool _isFlagged = false;
        public bool IsFlagged
        {
            get
            {
                return _isFlagged;
            }
            set
            {
                _isFlagged = value;
                OnPropertyChanged(nameof(IsFlagged));
            }
        }
        private bool _isCovered = true;
        public bool IsCovered
        {
            get
            {
                return _isCovered;
            }
            set
            {
                _isCovered = value;
                OnPropertyChanged(nameof(IsCovered));
            }
        }
        public int XPos
        {
            get
            {
                return Board.Cells.CoordinatesOf(this).Item1;
            }
        }
        public int YPos
        {
            get
            {
                return Board.Cells.CoordinatesOf(this).Item2;
            }
        }
        public Cell(Board board)
        {
            Board = board;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void countBombs()
        {
            if (!IsBomb)
            {
                SurroundingBombs = 0;
                for (int y = -1; y < 2; y++)
                {
                    for (int x = -1; x < 2; x++)
                    {
                        int checkX = XPos + x;
                        int checkY = YPos + y;
                        if (checkX >= 0 && checkY >= 0 && checkX < Board.Size && checkY < Board.Size)
                        {
                            Cell checkcell = Board.Cells[checkY, checkX];
                            if (checkcell.IsBomb)
                            {
                                SurroundingBombs++;
                            }
                        }
                    }
                }
            }
        }
        public void uncover(bool trigger = false)
        {
            if (!IsFlagged)
            {
                IsCovered = false;
                if (IsBomb && trigger)
                {
                    MessageBox.Show("Game Over", "Boom", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                if (!IsBomb && SurroundingBombs < 1)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        for (int x = -1; x < 2; x++)
                        {
                            int checkX = XPos + x;
                            int checkY = YPos + y;
                            if (checkX >= 0 && checkY >= 0 && checkX < Board.Size && checkY < Board.Size)
                            {
                                Cell checkcell = Board.Cells[checkY, checkX];
                                if (!checkcell.IsBomb && checkcell != this && checkcell.IsCovered)
                                {
                                    //Console.WriteLine("Clicked: x{0}/y{1} - Checking: x{2}/y{3}", XPos + 1, YPos + 1, checkcell.XPos + 1, checkcell.YPos + 1);
                                    checkcell.uncover();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

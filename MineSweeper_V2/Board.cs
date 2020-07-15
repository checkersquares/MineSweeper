using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace MineSweeper_V2
{
    public class Board : INotifyPropertyChanged
    {
        private int _size = 8;
        private int _flags = 0;
        private int _bombs = 0;
        private bool _isLoading = false;
        private bool _isFinishable = false;
        private int _lives = 1;
        public int Lives
        {
            get { return _lives; }
            set { _lives = value; OnPropertyChanged(nameof(Lives)); }
        }

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        public int Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
                OnPropertyChanged(nameof(Size));
            }
        }
        public int Flags
        {
            get
            {
                return _flags;
            }
            set
            {
                _flags = value;
                OnPropertyChanged(nameof(Flags));
            }
        }
        public int Bombs
        {
            get
            {
                return _bombs;
            }
            set
            {
                _bombs = value;
                OnPropertyChanged(nameof(Bombs));
            }
        }
        public bool IsFinishable
        {
            get
            {
                return _isFinishable;
            }
            set
            {
                _isFinishable = value;
                OnPropertyChanged(nameof(IsFinishable));
            }
        }
        public Cell[,] Cells { get; set; }

        public Board(int size = 8, int bombs = 0)
        {
            Size = Math.Abs(size);
            Bombs = bombs;
            buildBoard();
        }
        public Board()
        {

        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void buildBoard()
        {
            Cells = new Cell[Size, Size];
            List<Cell> cellList = new List<Cell>();
            int maxCells = Size * Size;
            // Bombs are about 16% of the cells 
            if (Bombs < 1)
            {
                Bombs = (int)Math.Floor((double)maxCells * 0.16);
            }

            for (int i = 0; i < maxCells; i++)
            {
                Cell c = new Cell(this);
                if (i < Bombs)
                {
                    c.IsBomb = true;
                }
                cellList.Add(c);
            }
            cellList.Shuffle();

            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    Cells[x, y] = cellList[(x*Size)+y];
                }
            }
            foreach (Cell c in Cells)
            {
                c.countBombs();
            }
        }
        public void countFlags()
        {
            Flags = 0;
            foreach (Cell c in Cells)
            {
                if (c.IsFlagged)
                {
                    Flags++;
                }
            }
            IsFinishable = Flags == Bombs;
        }
        public void finish()
        {
            bool win = true;
            foreach (Cell c in Cells)
            {
                if (!c.IsFlagged)
                {
                    c.uncover();
                    if (c.IsBomb)
                    {
                        win = false;
                    }
                }
            }
            if (win)
            {
                MessageBox.Show("Victory!", "Congratulations!");
            }
            else
            {
                MessageBox.Show("Game Over", "Boom", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}

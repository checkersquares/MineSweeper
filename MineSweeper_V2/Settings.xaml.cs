using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace MineSweeper_V2
{
    /// <summary>
    /// Interaktionslogik für Settings.xaml
    /// </summary>
    public partial class Settings : Window, INotifyPropertyChanged
    {
        public Difficulty SelectedDifficulty { get; set; }
        // Fields
        private int _size;
        private int _fields;
        private int _bombs;
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
        public int Fields
        {
            get
            {
                return _fields;
            }
            set
            {
                _fields = value;
                OnPropertyChanged(nameof(Fields));
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
                if (value > 1)
                {
                    _bombs = value;
                }
                else
                {
                    _bombs = 1;
                }
                OnPropertyChanged(nameof(Bombs));
            }
        }
        public Settings(Window parent)
        {
            InitializeComponent();
            Left = parent.Left + ((parent.Width - Width)/2);
            Top = parent.Top + ((parent.Height - Height)/2);
            DataContext = this;
            cbDifficulty.ItemsSource = SelectedDifficulty.GetValues();
            cbDifficulty.SelectedIndex = 0;
        }
        public enum Difficulty
        {
            Easy,
            Medium,
            Hard
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void cbDifficulty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Difficulty d;
            Enum.TryParse(cbDifficulty.SelectedItem.ToString(), out d);
            switch (d)
            {
                case Difficulty.Easy:
                    Size = 8;
                    break;
                case Difficulty.Medium:
                    Size = 12;
                    break;
                case Difficulty.Hard:
                    Size = 16;
                    break;
                default:
                    break;
            }
            Bombs = (int)Math.Floor((double)(Size*Size) * 0.16);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void tbSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            int x;
            if (!int.TryParse(tbSize.Text, out x))
            {
                Size = 8;
            }
            if (Size > 30)
            {
                Size = 30;
            }
            Fields = Size * Size;
            Bombs = (int)Math.Floor((double)(Size * Size) * 0.16);
        }

        private void tbBombs_TextChanged(object sender, TextChangedEventArgs e)
        {
            int x;
            if (!int.TryParse(tbBombs.Text, out x))
            {
                Bombs = 10;
            }
            if (Bombs > Fields / 2)
            {
                Bombs = (int)Math.Floor((double)Fields / 2);
            }
        }
    }
}

using System;
using System.Windows;
using System.Windows.Controls;

namespace MineSweeper_V2
{
    /// <summary>
    /// Interaktionslogik für CellModel.xaml
    /// </summary>
    public partial class CellModel : UserControl
    {
        public Cell Data { get; set; }
        private double _cellFontSize;
        public double CellFontSize
        {
            get
            {
                return _cellFontSize;
            }
            set
            {
                if (IsInitialized)
                {
                    if (double.IsNaN(value))
                    {
                        _cellFontSize = (400 / Data.Board.Size) / 2;
                    }
                    else
                    {
                        _cellFontSize = (value / Data.Board.Size) / 2;
                    }
                    tbCount.FontSize = tbContent.FontSize = btnCover.FontSize = _cellFontSize;
                }
            }
        }
        public CellModel(Cell cell)
        {
            InitializeComponent();
            Data = cell;
            DataContext = Data;
        }
        private void btnCover_Click(object sender, RoutedEventArgs e)
        {
            Data.uncover(true);
        }

        private void btnCover_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (Data.IsFlagged)
            {
                Data.IsFlagged = false;
            }
            else
            {
                Data.IsFlagged = true;
            }
            Data.Board.countFlags();
        }
    }
    public class BoolToStringConverter : BoolToValueConverter<String> { }
}

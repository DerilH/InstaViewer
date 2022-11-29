using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace InstaWatcher
{
    public class ImprovedGrid : Grid
    {
        public int ColumnsCount => ColumnDefinitions.Count;
        public int RowsCount => RowDefinitions.Count;

        public ImprovedGrid() : base()
        {

        }
        public void AddRow(RowDefinition rowDefinition)
        {
            RowDefinitions.Add(rowDefinition);
        }
        public void AddColumn(ColumnDefinition columnDefinition)
        {
            ColumnDefinitions.Add(columnDefinition);
        }
        public void AddChild(UIElement control, int column, int row)
        {
            Children.Add(control);
            SetColumn(control, column);
            SetRow(control, row);
        }
        public void RemoveChild(UIElement control) 
        {
            Children.Remove(control);
        }
        public int IndexOfRow(RowDefinition row)
        {
            return RowDefinitions.IndexOf(row);
        }
        public int IndexOfColumn(ColumnDefinition column)
        {
            return ColumnDefinitions.IndexOf(column);
        }
    }
}

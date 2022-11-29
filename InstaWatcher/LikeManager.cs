using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InstaWatcher
{
    internal class LikeManager
    {
        private MainWindow _window;
        private ViewManager _viewManager;
        private Dictionary<ProfileRow, int> ratedRows = new Dictionary<ProfileRow, int>();
        
        public LikeManager(MainWindow window)
        {
            this._window = window;
        }
        public void SetViewManager(ViewManager viewManager)
        {
            this._viewManager = viewManager;
        }
        public void OnRate(object sender, SelectionChangedEventArgs e) 
        {
            MarkControl senderM = sender as MarkControl;
            int value = senderM.SelectedIndex + 1;

            if (!ratedRows.ContainsKey(senderM.row))
            {
                ratedRows.Add(senderM.row, value);
                _viewManager.AddSortedViewRow(senderM.row);
            }
            else ratedRows[senderM.row] = value;
        }
    }
}

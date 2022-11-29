using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;

namespace InstaWatcher
{
    internal class LikeButton : Button
    {
        private bool _pressed = false;
        public bool Pressed { get => _pressed; }
        public LikeButton(int width, int height) : base() 
        {
            BorderBrush = Brushes.Transparent;
            VerticalAlignment = VerticalAlignment.Center;
            HorizontalAlignment = HorizontalAlignment.Left;
            Cursor = Cursors.Hand;
            Background = GlobalResources.blueLikeBrush;
            this.Width = width;
            this.Height = height;
            Click += OnClick;
        }
        private void OnClick(object sender, RoutedEventArgs e)
        {
            Button senderB = sender as Button;
            if (!Pressed) senderB.Background = GlobalResources.redLikeBrush;
            else senderB.Background = GlobalResources.blueLikeBrush;
            _pressed = !_pressed;
        }
    }
}

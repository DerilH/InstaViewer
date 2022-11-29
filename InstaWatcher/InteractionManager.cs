using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

namespace InstaWatcher
{
    internal class InteractionManager
    {
        private MainWindow _window;
        public InteractionManager(MainWindow window) 
        {
            this._window = window;
            _window.Deactivated += WindowDeactivated;
            _window.MainViewSV.PreviewMouseWheel += MainViewScrollPreviewMouseWheel;
            _window.BestViewSV.PreviewMouseWheel += SortedViewScrollPreviewMouseWheel;
            _window.ImagePopup.MouseLeftButtonUp += ImageOnMouseUp;
            _window.SortedViewImagePopup.MouseLeftButtonUp += ImageOnMouseUp;
        }

        public void ImageOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            LikeableImage senderI = sender as LikeableImage;

            Image img = new Image();
            img.Source = senderI.Source;
            img.Width = 600;

            Rect rect = new Rect(new Size(
            SystemParameters.FullPrimaryScreenWidth,
            SystemParameters.FullPrimaryScreenHeight));

            if (senderI.Like != null)
            {
                _window.ImagePopup.Child = img;
                _window.ImagePopup.IsOpen = true;
                _window.ImagePopup.PlacementRectangle = rect;
            }
            else
            {
                _window.SortedViewImagePopup.Child = img;
                _window.SortedViewImagePopup.IsOpen = true;
                _window.SortedViewImagePopup.PlacementRectangle = rect;
            }
        }
        public void ImageOnMouseUp(object sender, MouseButtonEventArgs e)
        {
                _window.ImagePopup.IsOpen = false;
                _window.ImagePopup.Child = null;
                _window.SortedViewImagePopup.IsOpen = false;
                _window.SortedViewImagePopup.Child = null;
        }

        public void ImageOnMouseEnter(object sender, MouseEventArgs e)
        {
            LikeableImage senderI = sender as LikeableImage;

            Image img = new Image();
            img.Source = senderI.Source;
            img.Width = 600;

            Rect rect = new Rect(new Size(
            SystemParameters.FullPrimaryScreenWidth,
            SystemParameters.FullPrimaryScreenHeight));

            if (senderI.Like != null)
            {
                _window.ImagePopup.Child = img;
                _window.ImagePopup.IsOpen = true;
                _window.ImagePopup.PlacementRectangle = rect;
            }
            else
            {
                _window.SortedViewImagePopup.Child = img;
                _window.SortedViewImagePopup.IsOpen = true;
                _window.SortedViewImagePopup.PlacementRectangle = rect;
            }
        }
        public void ImageOnMouseLeave(object sender, MouseEventArgs e)
        {
            _window.ImagePopup.IsOpen = false;
            _window.ImagePopup.Child = null;
            _window.SortedViewImagePopup.IsOpen = false;
            _window.SortedViewImagePopup.Child = null;
        }

        private void WindowDeactivated(object sender, EventArgs e)
        {
            _window.ImagePopup.IsOpen = false;
            _window.ImagePopup.Child = null;
            _window.SortedViewImagePopup.IsOpen = false;
            _window.SortedViewImagePopup.Child = null;
        }


        private void MainViewScrollPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
            if (Keyboard.IsKeyUp(Key.LeftCtrl))
            {
                _window.MainViewSV.ScrollToVerticalOffset(_window.MainViewSV.VerticalOffset - e.Delta / 3);
            }
            else _window.MainViewSV.ScrollToHorizontalOffset(_window.MainViewSV.HorizontalOffset - e.Delta / 3);
        }
        private void SortedViewScrollPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
            if (Keyboard.IsKeyUp(Key.LeftCtrl))
            {
                _window.BestViewSV.ScrollToVerticalOffset(_window.BestViewSV.VerticalOffset - e.Delta / 3);
            }
            else _window.BestViewSV.ScrollToHorizontalOffset(_window.BestViewSV.HorizontalOffset - e.Delta / 3);
        }
    }
}

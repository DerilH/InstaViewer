using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Net.Cache;

namespace InstaWatcher
{
    internal class LikeableImage : Image
    {
        public bool Liked { get; set; } = false;
        public Button Like { get; set; }
        public LikeableImage(string path, int width, int height, Button like) : base() 
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            Source = bitmap;

            this.Width = width;
            this.Height = height;
            if (like == null) return; 
            this.Like = like;
            this.Like.Click += OnLike;
        }
        public LikeableImage(ImageSource source, int width, int height, Button like) : base()
        {
            this.Source = source;
            this.Width = width;
            this.Height = height;
            if (like == null) return;

            this.Like = like;
            this.Like.Click += OnLike;
        }

        private void OnLike(object sender, RoutedEventArgs e) 
        {
            Liked = !Liked;
        }
    }
}

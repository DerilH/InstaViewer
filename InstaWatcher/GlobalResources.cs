using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace InstaWatcher
{
    internal static class GlobalResources
    {
        public static ImageBrush redLikeBrush { get; set; }
        public static ImageBrush blueLikeBrush { get; set; }
        static GlobalResources() 
        {
            BitmapImage blikeBi = new BitmapImage(new Uri("./blueLike.png", UriKind.RelativeOrAbsolute));
            blueLikeBrush = new ImageBrush();
            blueLikeBrush.ImageSource = blikeBi;
            blueLikeBrush.Stretch = Stretch.Uniform;

            BitmapImage rlikeBi = new BitmapImage(new Uri("./redLike.png", UriKind.RelativeOrAbsolute));
            redLikeBrush = new ImageBrush();
            redLikeBrush.ImageSource = rlikeBi;
            redLikeBrush.Stretch = Stretch.Uniform;
        }
    }
}

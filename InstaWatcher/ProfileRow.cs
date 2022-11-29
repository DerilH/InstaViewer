using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InstaWatcher
{
    internal class ProfileRow
    {
        public MarkControl mark;
        public TextBlock username;
        public LikeableImage avatar;
        public TextBlock description;
        public List<LikeableImage> images;

        public ProfileRow(MarkControl mark, TextBlock username, LikeableImage avatar, TextBlock description, List<LikeableImage> images) 
        {
            this.mark = mark;
            this.mark.row = this;
            this.username = username;
            this.avatar = avatar;
            this.description = description;
            this.images = images;
        }
    }
}

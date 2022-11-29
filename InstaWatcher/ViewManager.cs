using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Web.Profile;
using InstaSharper.Classes.Models;
using System.Reflection;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace InstaWatcher
{
    internal class ViewManager
    {
        private MainWindow _window;
        private InteractionManager _interactionManager;
        private LikeManager _likeManager; 

        private int photoWidth = 200;
        private int photoHeight = 330;

        public List<ProfileRow> profileRows = new List<ProfileRow>();

        public ViewManager(MainWindow window, InteractionManager interactionManager, LikeManager likeManager) 
        {
            this._window = window;
            this._interactionManager = interactionManager;
            this._likeManager = likeManager;
        }

        public void AddMainViewRow(string username, string description, string avatarPath, List<string> photoPathes)
        {
            RowDefinition row = new RowDefinition();
            row.Height = new GridLength(photoHeight);

            _window.MainViewG.AddRow(row);
            int rowIndex = _window.MainViewG.IndexOfRow(row);
            
            MarkControl mark = new MarkControl(5);
            mark.SelectionChanged += _likeManager.OnRate;

            TextBlock usernameTB = new TextBlock()
            {
                Text = username,
                Foreground = Brushes.White,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            TextBlock descriptionTB = new TextBlock()
            {
                Text = description,
                Foreground = Brushes.White,
                VerticalAlignment = VerticalAlignment.Center
            };

            LikeableImage avatar = new LikeableImage(avatarPath, photoWidth - 20, photoHeight - 20, new System.Windows.Controls.Button());
            avatar.MouseLeftButtonDown += _interactionManager.ImageOnMouseDown;
            avatar.MouseLeftButtonUp += _interactionManager.ImageOnMouseUp;

            _window.MainViewG.AddChild(mark, 0, rowIndex);
            _window.MainViewG.AddChild(usernameTB, 1, rowIndex);
            _window.MainViewG.AddChild(avatar, 2, rowIndex);
            _window.MainViewG.AddChild(descriptionTB, 3, rowIndex);

            List<LikeableImage> images = new List<LikeableImage>();
            //Setting photos
            int index = 4;
            foreach (string photoPath in photoPathes)
            {
                LikeButton like = new LikeButton(20, 20);

                LikeableImage image = new LikeableImage(photoPath, photoWidth - 20, photoHeight - 20, like);
                image.HorizontalAlignment = HorizontalAlignment.Right;

                image.MouseLeftButtonDown += _interactionManager.ImageOnMouseDown;
                image.MouseLeftButtonUp += _interactionManager.ImageOnMouseUp;
                //image.MouseEnter += _interactionManager.ImageOnMouseEnter;
                //image.MouseLeave += _interactionManager.ImageOnMouseLeave;
                if (_window.MainViewG.ColumnsCount <= index)
                {
                    ColumnDefinition column = new ColumnDefinition();
                    column.Width = new GridLength(photoWidth);
                    _window.MainViewG.AddColumn(column);

                    TextBlock columnTB = new TextBlock()
                    {
                        Text = "Фото",
                        Foreground = Brushes.White,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        TextAlignment = TextAlignment.Center
                    };

                    Rectangle columnRect = new Rectangle();
                    columnRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF484848"));

                    int columnIndex = _window.MainViewG.IndexOfColumn(column);
                    _window.MainViewG.AddChild(columnRect, columnIndex, 0);
                    _window.MainViewG.AddChild(columnTB, columnIndex, 0);
                }
                
                _window.MainViewG.AddChild(image, index, rowIndex);
                _window.MainViewG.AddChild(like, index, rowIndex);
                images.Add(image);
                index++;
            }
            profileRows.Add(new ProfileRow(mark, usernameTB, avatar, descriptionTB, images));
        }
        public void AddSortedViewRow(ProfileRow profileRow)
        {
            RowDefinition row = new RowDefinition();
            row.Height = new GridLength(photoHeight);

            _window.BestViewG.AddRow(row);
            int rowIndex = _window.BestViewG.IndexOfRow(row);

            TextBlock mark = new TextBlock()
            {
                Foreground = Brushes.White,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Text = (profileRow.mark.SelectedIndex + 1).ToString()
            };

            TextBlock username = new TextBlock()
            {
                Foreground = Brushes.White,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Text = profileRow.username.Text
            };

            LikeableImage avatar = new LikeableImage(profileRow.avatar.Source, (int)profileRow.avatar.Width, (int)profileRow.avatar.Height, null);
            avatar.MouseLeftButtonUp += _interactionManager.ImageOnMouseUp;
            avatar.MouseLeftButtonDown += _interactionManager.ImageOnMouseDown;

            TextBlock description = new TextBlock()
            {
                Foreground = Brushes.White,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Text = profileRow.description.Text
            };

            _window.BestViewG.AddChild(mark, 0, rowIndex);
            _window.BestViewG.AddChild(username, 1, rowIndex);
            _window.BestViewG.AddChild(avatar, 2, rowIndex);
            _window.BestViewG.AddChild(description, 3, rowIndex);

            int index = 4;
            foreach (LikeableImage likedImage in profileRow.images)
            {
                LikeableImage image = new LikeableImage(likedImage.Source, photoWidth - 20, photoHeight - 20, null);
                image.HorizontalAlignment = HorizontalAlignment.Center;
                image.MouseLeftButtonDown += _interactionManager.ImageOnMouseDown;
                image.MouseLeftButtonUp += _interactionManager.ImageOnMouseUp;

                if (_window.BestViewG.ColumnsCount <= index)
                {
                    ColumnDefinition column = new ColumnDefinition();
                    column.Width = new GridLength(photoWidth);
                    _window.BestViewG.AddColumn(column);

                    TextBlock columnTB = new TextBlock()
                    {
                        Text = "Фото",
                        Foreground = Brushes.White,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        TextAlignment = TextAlignment.Center
                    };

                    Rectangle columnRect = new Rectangle();
                    columnRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF484848"));

                    int columnIndex = _window.BestViewG.IndexOfColumn(column);
                    _window.BestViewG.AddChild(columnRect, columnIndex, 0);
                    _window.BestViewG.AddChild(columnTB, columnIndex, 0);
                }

                _window.BestViewG.AddChild(image, index, rowIndex);
                index++;
            }
        }
    }
}

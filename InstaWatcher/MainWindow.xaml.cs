using System.Windows;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Net;
using System.Threading.Tasks;

namespace InstaWatcher
{
    public partial class MainWindow : Window
    {
        private ViewManager _viewManager;
        private InteractionManager _interactionManager;
        private LikeManager _likeManager;
        private InstaManager _instaManager;

        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private void Init() 
        {
            _interactionManager = new InteractionManager(this);
            _likeManager = new LikeManager(this);
            _viewManager = new ViewManager(this, this._interactionManager, this._likeManager);
            _likeManager.SetViewManager(_viewManager);

            string login = ConfigurationManager.AppSettings["login"];
            string password = ConfigurationManager.AppSettings["password"];

            _instaManager = new InstaManager(_viewManager);
            _instaManager.Init(login, password);
        }
    }
}

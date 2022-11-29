using InstaSharper.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;
using mshtml;
using System.Windows.Navigation;
using System.Windows;
using PuppeteerSharp;
using Page = PuppeteerSharp.Page;
using InstaWatcher.Extensions;
using System.Threading;
using System.Xml.Linq;
using PuppeteerSharp.Input;
using System.Runtime.InteropServices;
using System.Windows.Threading;

namespace InstaWatcher  
{
    internal class InstaManager
    {
        private static string InstaURL = "https://www.instagram.com/";
        private static string AccountsPath = "AccountsForLoad.txt";

        private ViewManager _viewManager;
        private BrowserFetcher _browserFetcher;
        private Browser browser;
        private Page page;
        private bool _initialized = false;
        private Dispatcher _uiDispatcher;
        public List<InstaProfile> profiles = new List<InstaProfile>();

        public delegate void ProfilesLoadHandler(InstaProfile profile);
        public event ProfilesLoadHandler ProfileLoaded = (profile) => { };

        public InstaManager(ViewManager viewManager) 
        {
            this._viewManager = viewManager;
            this._browserFetcher = new BrowserFetcher();
            ProfileLoaded += OnProfileLoaded;
        }

        public async Task Init(string login, string password)
        {
            _uiDispatcher = Dispatcher.FromThread(Thread.CurrentThread);
            if (_initialized) return;
            _initialized = true;
            new Thread(async () =>
            {
                //Initializing puppeter
                await _browserFetcher.DownloadAsync();
                browser = (Browser)await Puppeteer.LaunchAsync(new LaunchOptions { Headless = false });
                page = (Page)await browser.NewPageAsync();

                //Login
                await Login(login, password);

                string[] usernames = File.ReadAllLines(AccountsPath);
                while (true)
                {
                    foreach (string username in usernames)
                    {
                        InstaProfile profile = await GetUserProfile(username);
                        profiles.Add(profile);
                        ProfileLoaded.Invoke(profile);
                    }
                }
            }).Start();
        }

        public async Task Login(string login, string password) 
        {
            await page.GoToAsync("https://www.instagram.com/accounts/login/");

            await page.WaitForSelectorAsync("._aa4b._add6._ac4d");
            IElementHandle[] inputs = await page.QuerySelectorAllAsync("._aa4b._add6._ac4d");
            
            await inputs[0].FocusAsync();
            await page.Keyboard.TypeAsync(login);
            await inputs[1].FocusAsync();
            await page.Keyboard.TypeAsync(password);

            IElementHandle button = await page.QuerySelectorAsync("._acan._acap._acas");
            await button.ClickAsync();
            Thread.Sleep(6000);
        }

        public async Task<InstaProfile> GetUserProfile(string username, int mediaCount = 10)
        {
            try
            {
                await page.GoToAsync(InstaURL + username);

                await page.WaitForSelectorAsync(".x5yr21d.xu96u03.x10l6tqk.x13vifvy.x87ps6o.xh8yej3");
                IElementHandle[] images = await page.QuerySelectorAllAsync(".x5yr21d.xu96u03.x10l6tqk.x13vifvy.x87ps6o.xh8yej3");
                IElementHandle[] avatars = await page.QuerySelectorAllAsync(".x6umtig.x1b1mbwd.xaqea5y.xav7gou.xk390pu.x5yr21d.xpdipgo.xdj266r.x11i5rnm.xat24cr.x1mh8g0r.xexx8yu.x4uap5.x18d9i69.xkhd6sd.x11njtxf.xh8yej3");
                if (avatars[1] == null) 
                {
                    avatars[1] = await page.QuerySelectorAsync("._aadp");
                }

                List<string> imageURLs = new List<string>();
                int index = 0;
                foreach (var image in images) 
                {
                    if (index == mediaCount) break; 
                    string url = await image.GetPropAsync("src");
                    imageURLs.Add(url);
                    index++;
                }

                InstaProfile profile = new InstaProfile();
                profile.Username = username;
                profile.AvatarURL = await avatars[1].GetPropAsync("src");
                profile.ImageURLs = imageURLs;
                return profile;
            }
            catch (Exception e)
            {
                MessageBox.Show("GerUser: "+ e.ToString());
                return null;
            }
        }

        private void OnProfileLoaded(InstaProfile profile)
        {
            _uiDispatcher.InvokeAsync(() => 
            {
                _viewManager.AddMainViewRow(profile.Username, "", profile.AvatarURL, profile.ImageURLs);
            });
        }
    }
}
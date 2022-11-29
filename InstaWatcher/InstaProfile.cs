using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaWatcher
{
    public class InstaProfile
    {
        public string Username {get;set;}
        public string Biography { get; set; }
        public string AvatarURL { get; set; }
        public List<string> ImageURLs { get; set; }

        public InstaProfile() 
        {
            this.ImageURLs = new List<string>();
        }
        public InstaProfile(string username, string biography, string avatarURL, List<string> imageLinks) 
        {
            this.Username = username;
            this.Biography = biography;
            this.AvatarURL = avatarURL;
            this.ImageURLs = imageLinks;
        }
    }
}

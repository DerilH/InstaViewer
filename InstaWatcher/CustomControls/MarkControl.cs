using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Runtime.InteropServices;

namespace InstaWatcher
{
    internal class MarkControl : ComboBox
    {
        public ProfileRow row { get; set; }
        public MarkControl(int marks) : base() 
        {
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
            
            for (int i = 1; i != marks + 1; i++) 
            {
                Items.Add(i);
            }
        }
    }
}

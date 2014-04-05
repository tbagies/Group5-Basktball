using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace templateDemo
{
    public static class Switcher
    {
        public static PageSwitch pageSwitch;

        public static void Switch(UserControl newPage) 
        {
            pageSwitch.Navigate(newPage);
        }
        public static void Switch(UserControl newPage, object state)
        {
            pageSwitch.Navigate(newPage, state);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Hoops
{
    public static class Switcher
    {
        public static PageSwitch pageSwitch;
        public static UserControl _newPage;

        public static void Switch(UserControl newPage) 
        {
            _newPage = newPage;
            pageSwitch.Navigate(_newPage);
        }
        public static void Switch(UserControl newPage, object state)
        {
            _newPage = newPage;
            pageSwitch.Navigate(_newPage, state);
        }
    }
}

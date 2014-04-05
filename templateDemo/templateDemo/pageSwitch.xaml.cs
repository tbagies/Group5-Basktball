using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace templateDemo
{
    /// <summary>
    /// Interaction logic for PageSwitch.xaml
    /// </summary>
    public partial class PageSwitch : Window
    {
        public PageSwitch()
        {
            InitializeComponent();
            Switcher.pageSwitch = this;
            
            //home page goes here
            Switcher.Switch(new templateDemo.Screens.Title());
        }
        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        public void Navigate(UserControl nextPage, object state)
        {
            this.Content = nextPage;
            ISwitchable s = nextPage as ISwitchable;

            if (s != null)
                s.UtilizeState(state);
            else
                throw new ArgumentException("The next page is not switchable" + nextPage.Name.ToString());
        }
    }
}

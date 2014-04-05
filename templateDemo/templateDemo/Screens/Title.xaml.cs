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

namespace templateDemo.Screens
{
    /// <summary>
    /// Interaction logic for Title.xaml
    /// </summary>
    public partial class Title : UserControl, ISwitchable
    {
        public Title()
        {
            InitializeComponent();
            
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        //event handler for going to next screen, for now it uses a button
        private void NEXT_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new templateDemo.Screens.TipOff());
        }
    }
}

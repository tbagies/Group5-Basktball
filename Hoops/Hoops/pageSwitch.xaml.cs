﻿using System;
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
using Hoops.Screens;
using Microsoft.Kinect.Toolkit;

namespace Hoops
{
    /// <summary>
    /// Interaction logic for PageSwitch.xaml
    /// </summary>
   public partial class PageSwitch : Window
    {
       private Title title = new Title();
        public PageSwitch()
        {
            InitializeComponent();
       //     App.Current.Properties["Team"] = "chicago";
            App.Current.Properties["Player"] = "Derrick Rose";
            Switcher.pageSwitch = this;
            Switcher.Switch(title);
        }
        public void Navigate(UserControl nextPage)
        {
            this.grid0.Children.Clear();
            this.grid0.Children.Add(nextPage);
            
        }

        public void Navigate(UserControl nextPage, object state)
        {
           this.grid0.Children.Clear();
            this.grid0.Children.Add(nextPage);
            ISwitchable s = nextPage as ISwitchable;

            if (s != null)
                s.UtilizeState(state);
            else
                throw new ArgumentException("The next page is not switchable" + nextPage.Name.ToString());
        }
    }
}

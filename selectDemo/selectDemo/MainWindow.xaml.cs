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

using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.Controls;

namespace selectDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// This is a demo of the player select screen based on a tutorial on:
    /// http://dotneteers.net/blogs/vbandi/archive/2013/03/25/kinect-interactions-with-wpf-part-i-getting-started.aspx
    /// and on a demo done by: Alfredo Castillo
    /// </summary>
    public partial class MainWindow : Window
    {
        private KinectSensorChooser sensorChooser;

        public interface ISwitchable
        {
            void UtilizeState(object state);
        }

        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            this.sensorChooser = new KinectSensorChooser();
            this.sensorChooser.KinectChanged +=sensorChooser_KinectChanged;
            this.sensorChooserUi.KinectSensorChooser = this.sensorChooser;
            this.sensorChooser.Start();

            //test adding a player
            addPlayer("p1", "Derick Rose", "resources/playerPhotos/CHI/1_derrick_rose.png");
        }

        private void sensorChooser_KinectChanged(object sender, KinectChangedEventArgs args)
        {
            MessageBox.Show(args.NewSensor == null ? "No Kinnect" : args.NewSensor.Status.ToString());
            bool error = false;

            if (args.OldSensor != null)
            {
                try
                {
                    args.OldSensor.DepthStream.Range = Microsoft.Kinect.DepthRange.Default;
                    args.OldSensor.SkeletonStream.EnableTrackingInNearRange = false;
                    args.OldSensor.DepthStream.Disable();
                    args.OldSensor.SkeletonStream.Disable();
                }
                catch (InvalidOperationException) { error = true; }
            }

            if (args.NewSensor != null)
            {
                try
                {
                    args.NewSensor.DepthStream.Enable(
                        Microsoft.Kinect.DepthImageFormat.Resolution640x480Fps30);
                    args.NewSensor.SkeletonStream.Enable();

                }
                catch (InvalidOperationException) { error = true; }
            }
            if (!error)
                kinectRegion.KinectSensor = args.NewSensor;
        }

        //this method will add the picture to the corresponding tile
        void addPlayer(string tilePositition, string playerName, string photoLocation)
        {
            BitmapImage photo = new BitmapImage();
            photo.BeginInit();
            photo.UriSource = new Uri(@"resources/playerPhotos/CHI/1_derrick_rose.png", UriKind.RelativeOrAbsolute);
            photo.EndInit();

            Image img = new Image();
            img.Stretch = Stretch.Fill;
            img.Source = photo;

            switch(tilePositition)
            {
                case "p1":
                    p1.Content = img;

                    p1.Background = new ImageBrush(photo);
                    p1.Tag = playerName;
                    p1.Opacity = 100;
                    break;
                default:
                    break;
            }

            //BitmapImage bi = new BitmapImage();
            //bi.BeginInit();
            //bi.UriSource = new Uri(a, UriKind.Relative);
            //bi.EndInit();

            //var button = new KinectTileButton
            //{
            //    Background = new ImageBrush(bi),
            //    BorderThickness = new Thickness(0),
            //    Height = 130,
            //    Width = 130,
            //    Foreground = Brushes.White,
            //    Tag = a
            //};
            
            //button.Click += tilebuttononclick;
            //stackpanelwithbutton.children.add(button);
        }

        /*This method will be called once a user selects a player*/
        private void playerSelected(object sender, RoutedEventArgs e)
        {
            var temp = (KinectTileButton)sender;
            string name = (string)temp.Tag;
            MessageBox.Show(name);

        }

        /*making switchable pages following a demo from:
         * http://azerdark.wordpress.com/2010/04/23/multi-page-application-in-wpf/
         * */
        private void Navigate(UserControl next)
        {
            this.Content = next;
        }

        private void Navigate(UserControl next, object state)
        {
            this.Content = next;
            ISwitchable s = next as ISwitchable;
            if (s != null)
                s.UtilizeState(state);
            else
                throw new ArgumentException("This page is not switchable: " + next.Name.ToString());

        }
    }
}

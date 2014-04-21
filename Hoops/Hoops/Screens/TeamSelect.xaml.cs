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
using Microsoft.Kinect.Toolkit.Interaction;

namespace Hoops.Screens
{
    /// <summary>
    /// Interaction logic for TeamSelect.xaml
    /// </summary>
    public partial class TeamSelect : UserControl, ISwitchable
    {
        private KinectSensorChooser sensorChooser;
        public TeamSelect()
        {
            InitializeComponent();
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            //gifs sources are here
           // timeOutGif.Source = new Uri("../../resources/tech.gif", UriKind.RelativeOrAbsolute);
           // passGif.Source = new Uri("../../resources/pass.gif", UriKind.RelativeOrAbsolute);

            sensorChooser = new KinectSensorChooser();
            sensorChooser.KinectChanged += SensorChooserOnKinectChanged;
            sensorChooserUi.KinectSensorChooser = sensorChooser;
            sensorChooser.Start();

            addTile("../../resources/Screen3/Logos/East/boston.png"); // runtime error here, cant find source
            addTile("../../resources/Screen3/Logos/East/atlanta.png");
            addTile("../../resources/Screen3/Logos/East/brooklyn.png");
            addTile("../../resources/Screen3/Logos/East/charlotte.png");
            addTile("../../resources/Screen3/Logos/East/chicago.png");
            addTile("../../resources/Screen3/Logos/East/cleveland.png");
            addTile("../../resources/Screen3/Logos/East/detroit.png");
            addTile("../../resources/Screen3/Logos/East/indiana.png");
            addTile("../../resources/Screen3/Logos/East/miami.png");
            addTile("../../resources/Screen3/Logos/East/milwaukee.png");
            addTile("../../resources/Screen3/Logos/East/newyork.png");
            addTile("../../resources/Screen3/Logos/East/orlando.png");
            addTile("../../resources/Screen3/Logos/East/philadelphia.png");
            addTile("../../resources/Screen3/Logos/East/toronto.png");
            addTile("../../resources/Screen3/Logos/East/washington.png");


            addTile2("../../resources/Screen3/Logos/West/dallas.png");
            addTile2("../../resources/Screen3/Logos/West/denver.png");
            addTile2("../../resources/Screen3/Logos/West/goldenstate.png");
            addTile2("../../resources/Screen3/Logos/West/houston.png");
            addTile2("../../resources/Screen3/Logos/West/lac.png");
            addTile2("../../resources/Screen3/Logos/West/lal.png");
            addTile2("../../resources/Screen3/Logos/West/memphis.png");
            addTile2("../../resources/Screen3/Logos/West/minnesota.png");
            addTile2("../../resources/Screen3/Logos/West/neworleans.png");
            addTile2("../../resources/Screen3/Logos/West/okc.png");
            addTile2("../../resources/Screen3/Logos/West/phoenix.png");
            addTile2("../../resources/Screen3/Logos/West/portland.png");
            addTile2("../../resources/Screen3/Logos/West/sacramento.png");
            addTile2("../../resources/Screen3/Logos/West/sanantonio.png");
            addTile2("../../resources/Screen3/Logos/West/utah.png");
        }

        private void SensorChooserOnKinectChanged(object sender, KinectChangedEventArgs args)
        {
            bool error = false;  
            if (args.OldSensor != null)   
            {  
                try  
                {   
                    args.OldSensor.DepthStream.Range = DepthRange.Default; 
                    args.OldSensor.SkeletonStream.EnableTrackingInNearRange = false; 
                    args.OldSensor.DepthStream.Disable();  
                    args.OldSensor.SkeletonStream.Disable();  
                }  
                catch (InvalidOperationException)  
                {    
                    error = true;  
                } 
            }  
            if (args.NewSensor != null)  
            {
                try
                {
                    args.NewSensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                    args.NewSensor.SkeletonStream.Enable();
                    try
                    {
                        args.NewSensor.DepthStream.Range = DepthRange.Near;
                        args.NewSensor.SkeletonStream.EnableTrackingInNearRange = true;
                        args.NewSensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
                    }
                    catch (InvalidOperationException)
                    {
                        args.NewSensor.DepthStream.Range = DepthRange.Default;
                        args.NewSensor.SkeletonStream.EnableTrackingInNearRange = false;
                        error = true;
                    }
                }
                catch (InvalidOperationException)
                {
                    error = true;
                }
            }
            if (!error)
            {
                kinectRegion.KinectSensor = args.NewSensor;
         //       kinectRegion2.KinectSensor = args.NewSensor;
            }
        }

        // adds the team buttons for the Eastern Conference 
        void addTile(string a)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(a, UriKind.Relative);
            bi.EndInit();

            var button = new KinectTileButton
            {
                Background = new ImageBrush(bi),
                BorderThickness = new Thickness(0),
                Height = 200,
                Width = 200,
                Foreground = Brushes.White,
                Tag = a
            };
            button.Click += tileButtonOnClick;
      //      EastContent.Children.Add(button);
        }

        // adds the team buttons for the Western Conference 
        void addTile2(string a)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(a, UriKind.Relative);
            bi.EndInit();

            var button = new KinectTileButton
            {
                Background = new ImageBrush(bi),
                BorderThickness = new Thickness(0),
                Height = 200,
                Width = 200,
                Foreground = Brushes.White,
                Tag = a
                
            };
            button.Click += tileButtonOnClick;
            WestContent.Children.Add(button);
        }

        private void tileButtonOnClick(object sender, RoutedEventArgs e)
        {
            var temp = (KinectTileButton)sender;
         //  
            String[] delimiter = new String[]{"/","."};
            String[] str = (temp.Tag.ToString()).Split(delimiter,StringSplitOptions.RemoveEmptyEntries);
            App.Current.Properties["Team"] = str[str.Length-2];
            sensorChooser.Stop();
            Switcher.Switch(new PlayerSelect()); 
        }

        //gif loop stuff
        private void timeOutGif_MediaEnded(object sender, RoutedEventArgs e)
        {
            timeOutGif.Position = new TimeSpan(0, 0, 1);
            timeOutGif.Play();
        }
        private void passGif_MediaEnded(object sender, RoutedEventArgs e)
        {
            passGif.Position = new TimeSpan(0, 0, 1);
            passGif.Play();
        }
    }
}

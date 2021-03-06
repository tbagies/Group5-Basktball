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
        //  private static KinectSensorChooser sensorChooser;

        private KinectSensorChooser sensorChooser;
        private int counter = 0;

        public KinectSensorChooser PassedSensorChooser
        {
            set
            {
                if (value != null)
                    this.sensorChooser = value;
                this.sensorChooserUi.KinectSensorChooser = this.sensorChooser;
            }
        }
        public TeamSelect()
        {
            InitializeComponent();
            Console.WriteLine("TeamSelect Cons");
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {

            Console.WriteLine("TeamSelect Loaded");
            kinectRegion.KinectSensor = sensorChooser.Kinect;
             sensorChooser.Kinect.SkeletonFrameReady += Kinect_SkeletonFrameReady;
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

        // adds the team buttons for the Eastern Conference 
        void addTile(string a)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(a, UriKind.Relative);
            bi.EndInit();

            // ADD: THIS SO PICTURE IS NOT SKEWED
            ImageBrush picture = new ImageBrush(bi);
            picture.Stretch = Stretch.Uniform;

            var button = new KinectTileButton
            {
                //ADD: equal to the picture
                Background = picture,
                BorderThickness = new Thickness(0),
                Height = 200,
                Width = 200,
                Foreground = Brushes.White,
                Tag = a
            };
            button.Click += tileButtonOnClick;

            //ADD: Add the east coast teams
            EastContent.Children.Add(button);
        }

        // adds the team buttons for the Western Conference 
        void addTile2(string a)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(a, UriKind.Relative);
            bi.EndInit();

            // ADD: THIS SO PICTURE IS NOT SKEWED
            ImageBrush picture = new ImageBrush(bi);
            picture.Stretch = Stretch.Uniform;

            var button = new KinectTileButton
            {
                // ADD: equal to the picture
                Background = picture,
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
            String[] delimiter = new String[]{"/","."};
            String[] str = (temp.Tag.ToString()).Split(delimiter,StringSplitOptions.RemoveEmptyEntries);
            App.Current.Properties["Team"] = str[str.Length-2];

            Console.WriteLine(str[str.Length - 2]);
            Console.WriteLine("FROM TITLEBUTTON");
            PlayerSelect p = new PlayerSelect();
            p.PassedSensorChooser = sensorChooser;
            
            Switcher.playClick();
            Switcher.Switch(p); 

        }

        void Kinect_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (var frame = e.OpenSkeletonFrame())
            {
                if (frame != null)
                {
                    Skeleton[] skeletons = new Skeleton[frame.SkeletonArrayLength];

                    frame.CopySkeletonDataTo(skeletons);

                    if (skeletons.Length > 0)
                    {
                        var user = skeletons.Where(
                                   u => u.TrackingState == SkeletonTrackingState.Tracked).FirstOrDefault();

                        if (user == null)
                        {
                            counter++;
                            if (counter > 150)
                            {
                                Console.WriteLine(" TEAMSELECT Counter " + counter);
                                sensorChooser.Kinect.SkeletonFrameReady -= Kinect_SkeletonFrameReady;
                                frame.Dispose();

                                sensorChooser.Stop();
                                kinectRegion.KinectSensor.Stop();
                                kinectRegion.KinectSensor.Dispose();
                                Title t = new Title();
                                Switcher.Switch(t);
                            }
                        }
                        else
                        {
                            counter--;
                        }
                    }
                }
            }
        }
       
    }
}

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
using Gestures;
using Microsoft.Kinect.Toolkit;

namespace Hoops.Screens
{
    /// <summary>
    /// Interaction logic for TipOff.xaml
    /// </summary>
    public partial class TipOff : UserControl, ISwitchable
    {
        private KinectSensorChooser sensorChooser;
        public KinectSensorChooser PassedSensorChooser
        {
            set
            {
                if (value != null)
                    this.sensorChooser = value;
                this.sensorChooserUi.KinectSensorChooser = sensorChooser;
            }
        }
        private JumpGesture _gesture = new JumpGesture();
        private TeamSelect teamSelect = new TeamSelect();
        private bool gestureRecognized = false;
        public TipOff()
        {
            InitializeComponent();
            Console.WriteLine("TippOFF");
        }

        public void UtilizeState(object state)
        {
            Console.WriteLine("UtilizeState");
           Loaded += TipOff_Loaded;
        }

        void TipOff_Loaded(object sender, RoutedEventArgs e)
        {
            //play song
            Switcher.playTheme();

            Console.WriteLine("TippOFF Loadded sensorChooser = " + sensorChooser);
            if (!gestureRecognized)
            {
                Console.WriteLine("IF");
                sensorChooser.Kinect.SkeletonFrameReady += sensorChooser_SkeletonFrameReady;
                _gesture.GestureRecognized += Gesture_GestureRecognized;
            }
            else
            {
                Console.WriteLine("ELSE");
                teamSelect.PassedSensorChooser = sensorChooser;
                Switcher.Switch(teamSelect);
            }
            Console.WriteLine("AfterSensorStartingStatment");
        }

        void sensorChooser_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            Console.WriteLine("Skeleton");
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
                            if (user != null)
                            {
                                _gesture.Update(user);
                            }
                        }
                    }
                }
            
        }
       
      void Gesture_GestureRecognized(object sender, EventArgs e)
        {
            gestureRecognized = true;
            Console.WriteLine("From JUMPING TipOff Screen");
            
            teamSelect.PassedSensorChooser = sensorChooser;
            sensorChooser.Kinect.SkeletonFrameReady -= sensorChooser_SkeletonFrameReady;
            Switcher.Switch(teamSelect);
        }

      private void StopKinect(KinectSensor sensor)
      {
          if (sensor != null)
          {
              if (sensor.IsRunning)
              {
                  sensor.Stop();
              }
          }
      }
    }
}

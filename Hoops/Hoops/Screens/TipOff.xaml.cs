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

namespace Hoops.Screens
{
    /// <summary>
    /// Interaction logic for TipOff.xaml
    /// </summary>
    public partial class TipOff : UserControl, ISwitchable
    {
       private KinectSensor sensor;
       private static JumpGesture _gesture = new JumpGesture();
       private static TeamSelect teamSelect = new TeamSelect();
       private bool recognized = false;
        public TipOff()
        {
            InitializeComponent();
        }
        public void UtilizeState(object state)
        {
           Loaded += TipOff_Loaded;
        }

        void TipOff_Loaded(object sender, RoutedEventArgs e)
        {
            sensor = KinectSensor.KinectSensors.Where(
                                    s => s.Status == KinectStatus.Connected).FirstOrDefault();
            if (sensor != null)
            {
                sensor.SkeletonStream.Enable();
                sensor.SkeletonFrameReady += Sensor_SkeletonFrameReady;
                if(_gesture!= null)
                    _gesture.GestureRecognized += Gesture_GestureRecognized;
                sensor.Start();
            }
            
        }

        //event handler for going to next screen, for now it uses a button
        static void Sensor_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
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
                        if (user != null && _gesture != null)
                        {
                            _gesture.Update(user);
                        }
                    }
                }
            }
        }
       
      void Gesture_GestureRecognized(object sender, EventArgs e)
        {
            recognized = true;
            _gesture = null;
            stopKinect();
            Switcher.Switch(teamSelect);
        }

       private void stopKinect()
       {
           sensor.Stop();
           sensor.Dispose();
        //   this.unl.grid1.Children.Clear();
         //  this.unl.grid2.Children.Clear();
       }

       //debug stuff!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
       private void goNext(object sender, RoutedEventArgs e)
       {
           recognized = true;
           _gesture = null;
           stopKinect();
           Switcher.Switch(teamSelect);
       }
    }
}

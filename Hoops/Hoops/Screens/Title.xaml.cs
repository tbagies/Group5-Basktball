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

namespace Hoops.Screens
{
    /// <summary>
    /// Interaction logic for Title.xaml
    /// </summary>
    public partial class Title : UserControl, ISwitchable
    {
        private static KinectSensor sensor;
        private static TipOff tipOff = new TipOff();      
        public Title()
        {
            InitializeComponent(); 
        }

       public void UtilizeState(object state)
        {
            Loaded += Title_Loaded;
        }

        private void Title_Loaded(object sender, RoutedEventArgs e)
        {
            sensor = KinectSensor.KinectSensors.Where(
                                    s => s.Status == KinectStatus.Connected).FirstOrDefault();
            if (sensor != null)
            {
                sensor.SkeletonStream.Enable();
                sensor.SkeletonFrameReady += Sensor_SkeletonFrameReady;
                sensor.Start();
            }
        }
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

                        if (user != null)
                        {
                            sensor.Stop();
                            sensor.Dispose();
                            Switcher.Switch(tipOff);
                        }
                    }
                }
            }
        }
    }
}

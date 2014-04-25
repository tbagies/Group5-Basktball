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

namespace Hoops.Screens
{
    /// <summary>
    /// Interaction logic for Title.xaml
    /// </summary>
    public partial class Title : UserControl, ISwitchable
    {
        private KinectSensorChooser sensorChooser;

        private TipOff tipOff = new TipOff();
        public Title()
        {
            InitializeComponent();
            sensorChooser = new KinectSensorChooser();
            sensorChooserUi.KinectSensorChooser = sensorChooser;
        }

       public void UtilizeState(object state)
        {
            Loaded += Title_Loaded;
        }

        private void Title_Loaded(object sender, RoutedEventArgs e)
        {
            Switcher.stopTheme();
            Console.WriteLine(" loaded ");
            sensorChooser.KinectChanged += new EventHandler<KinectChangedEventArgs>(sensorChooser_KinectChanged);
            sensorChooser.Start(); 
        }

        void sensorChooser_KinectChanged(object sender, KinectChangedEventArgs e)
        {
            Console.WriteLine(" KinectChanged");
    //        StopKinect(e.OldSensor);

            if (e.NewSensor == null)
            {
                Console.WriteLine(" e.NewSensor = null");
                return;
            }

            e.NewSensor.SkeletonStream.Enable();
            e.NewSensor.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(sensorChooser_SkeletonFrameReady);
            e.NewSensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
            e.NewSensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
            try
            {
                e.NewSensor.Start();
                Console.WriteLine(" e.NewSensor  is started");
            }
            catch (System.IO.IOException)
            {
                Console.WriteLine(" Exception");

            }
        }
      
        void sensorChooser_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
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
                            tipOff.PassedSensorChooser = sensorChooser;
                            sensorChooser.Kinect.SkeletonFrameReady -= new EventHandler<SkeletonFrameReadyEventArgs>(sensorChooser_SkeletonFrameReady);
                            Switcher.Switch(tipOff);
                           
                        }
                    }
                }
            }
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

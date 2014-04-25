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

        private int counter = 0;
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
            sensorChooser.Kinect.SkeletonFrameReady += sensorChooser_SkeletonFrameReady;
            _gesture.GestureRecognized += Gesture_GestureRecognized;         
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
                                counter--;
                                Console.WriteLine(" user != null COunter " + counter);
                                _gesture.Update(user);
                            }
                            else
                            {
                                counter++;
                                if(counter>150)
                                {
                                    Console.WriteLine(" TipOff COunter " + counter);
                                    sensorChooser.Kinect.SkeletonFrameReady -= sensorChooser_SkeletonFrameReady;
                                    frame.Dispose();                               
                                    sensorChooser.Kinect.Stop();
                                    sensorChooser.Kinect.Dispose();
                                    sensorChooser.Stop();
                                    Title t = new Title();
                                    Switcher.Switch(t);
                                }
                            }
                        }
                        
                    }
                }
        }
       
      void Gesture_GestureRecognized(object sender, EventArgs e)
        {
            Switcher.playCheers();
            Console.WriteLine("From JUMPING TipOff Screen"); 
            TeamSelect teamSelect = new TeamSelect();
            teamSelect.PassedSensorChooser = sensorChooser;
            sensorChooser.Kinect.SkeletonFrameReady -= sensorChooser_SkeletonFrameReady;
            Switcher.Switch(teamSelect);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit.Controls;
using Microsoft.Kinect.Toolkit.Interaction;
using Microsoft.Kinect.Toolkit;

namespace Hoops
{
    public static class Switcher
    {
        public static PageSwitch pageSwitch;
        public static  UserControl _newPage;
        
        public static void Switch(UserControl newPage) 
        {
            _newPage = newPage;
            pageSwitch.Navigate(_newPage);
        }
        public static void Switch(UserControl newPage, object state)
        {
            _newPage = newPage;
            pageSwitch.Navigate(_newPage, state);
        }
    }

    public static class HoopsSensor
    {
        public static KinectSensor sensor = KinectSensor.KinectSensors.Where(s => s.Status == KinectStatus.Connected).FirstOrDefault();
    }
}

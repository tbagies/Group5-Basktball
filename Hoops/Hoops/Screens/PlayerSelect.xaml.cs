using System;
using System.IO;
using System.Collections;
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
using Gestures;


namespace Hoops.Screens
{
    /// <summary>
    /// Interaction logic for PlayerSelect.xaml
    /// </summary>
    public partial class PlayerSelect : UserControl, ISwitchable
    {
        private KinectSensorChooser sensorChooser;
        public KinectSensorChooser PassedSensorChooser
        {
            set
            {
                if (value != null)
                    this.sensorChooser = value;
                this.sensorChooserUi.KinectSensorChooser = this.sensorChooser;
            }
        }
       // private TimeOutGesture timeOutGesture = new TimeOutGesture();
       // private PassingGesture passingGesture = new PassingGesture();
        private TeamSelect page = new TeamSelect();      

        private string teamFolder = "CHI";
        private string playerPhotoLoc = "resources/playerPhotos/";
        private string[] photos = new string [20];                  //15 should be fine but 20 to be safe

        public PlayerSelect()
        {
            InitializeComponent();
            page.PassedSensorChooser = sensorChooser;
        }

        public void UtilizeState(object state)
        {
            Loaded += PlayerSelect_Loaded;
        }

        void PlayerSelect_Loaded(object sender, RoutedEventArgs e)
        {
            kinectRegion.KinectSensor = sensorChooser.Kinect;
            sensorChooser.Kinect.SkeletonFrameReady += Kinect_SkeletonFrameReady;
           // timeOutGesture.GestureRecognized += timeOutGesture_GestureRecognized;
           // passingGesture.GestureRecognized += passingGesture_GestureRecognized; 
            
            teamFolderName();
            readRoster();
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

                        if (user != null)
                        {
                            //timeOutGesture.Update(user);
                            //passingGesture.Update(user);
                        }
                    }
                }
            }
        }

        //void timeOutGesture_GestureRecognized(object sender, EventArgs e)
        //{
        //    sensorChooser.Kinect.SkeletonFrameReady -= Kinect_SkeletonFrameReady;
        //    TeamSelect t = new TeamSelect();
        //    t.PassedSensorChooser = sensorChooser;
        //    Switcher.Switch(t);
        //}

        //void passingGesture_GestureRecognized(object sender, EventArgs e)
        //{
        //    sensorChooser.Kinect.SkeletonFrameReady -= Kinect_SkeletonFrameReady;
        //    TeamSelect p = new TeamSelect();
        //    p.PassedSensorChooser = sensorChooser;
        //    Switcher.Switch(p);
        //}

        void teamFolderName()
        {
            String team = Application.Current.Properties["Team"].ToString();
            switch (team)
            {
                case "atlanta": 
                    teamFolder = "ATL";
                    break;
                case "boston":
                    teamFolder = "BOS";
                    break;
                case "brooklyn":
                    teamFolder = "BRK";
                    break;
                case "charlotte":
                    teamFolder = "CHA";
                    break;
                case "chicago":
                    teamFolder = "CHI";
                    break;
                case "cleveland":
                    teamFolder = "CLE";
                    break;
                case "detroit":
                    teamFolder = "DET";
                    break;
                case "indiana":
                    teamFolder = "IND";
                    break;
                case "miami":
                    teamFolder = "MIA";
                    break;
                case "milwaukee":
                    teamFolder = "MIL";
                    break;
                case "newyork":
                    teamFolder = "NYK";
                    break;
                case "orlando":
                    teamFolder = "ORL";
                    break;
                case "philadelphia":
                    teamFolder = "PHI";
                    break;
                case "toronto": 
                    teamFolder = "TOR";
                    break;
                case "washington":
                    teamFolder = "WAS";
                    break;
                case "dallas":
                    teamFolder = "DAL";
                    break;
                case "denver":
                    teamFolder = "DEN";
                    break;
                case "goldenstate":
                    teamFolder = "GSW";
                    break;
                case "houston":
                    teamFolder = "HOU";
                    break;
                case "lac":
                    teamFolder = "LAC";
                    break;
                case "lal":
                    teamFolder = "LAL";
                    break;
                case "memphis":
                    teamFolder = "MEM";
                    break;
                case "minnesota":
                    teamFolder = "MIN";
                    break;
                case "neworleans":
                    teamFolder = "NOP";
                    break;
                case "okc":
                    teamFolder = "OKC";
                    break;
                case "phoenix":
                    teamFolder = "PHO";
                    break;
                case "portland":
                    teamFolder = "POR";
                    break;
                case "sacramento":
                    teamFolder = "SAC";
                    break;
                case "sanantonio":
                    teamFolder = "SAS";
                    break;
                case "utah":
                    teamFolder = "UTA";
                    break;
                default:
                    teamFolder = "UTA";
                    break;
            }
        }
        /// <summary>
        /// This method will add the picture to the corresponding tile
        /// </summary>
        /// <param name="tilePosition"></param>
        /// <param name="playerName"></param>
        /// <param name="photoLocation"></param>
        void addPlayer(int tilePosition, string playerName, string photoLocation)
        {
            BitmapImage photo = new BitmapImage();
            photo.BeginInit();
            photo.UriSource = new Uri(@photoLocation, UriKind.RelativeOrAbsolute);
            photo.EndInit();
            Image img = new Image();
            img.Stretch = Stretch.Uniform;
            img.Source = photo;

            switch (tilePosition)
            {
                case 1:
                    p1.Content = img;
                    p1.Tag = playerName;
                    p1.Opacity = 100;
                    p1.Visibility = System.Windows.Visibility.Visible;
                    p1.IsEnabled = true;

                    t1.Width = p1.Width;
                    t1.Margin = new Thickness(p1.Margin.Left, p1.Margin.Top + p1.Height, p1.Margin.Right, 0);
                    t1.TextAlignment = TextAlignment.Center;
                    t1.Text = playerName;
                    break;
                case 2:
                    p2.Content = img;
                    p2.Tag = playerName;
                    p2.Opacity = 100;
                    p2.Visibility = System.Windows.Visibility.Visible;
                    p2.IsEnabled = true;

                    //why does this NOT work!
                    t2.Width = p2.Width;
                    t2.Margin = new Thickness(p2.Margin.Left, p2.Margin.Top + p2.Height, p2.Margin.Right, 0);
                    t2.TextAlignment = TextAlignment.Center;
                    t2.Text = playerName;
                    break;
                case 3:
                    p3.Content = img;
                    p3.Tag = playerName;
                    p3.Opacity = 100;
                    p3.Visibility = System.Windows.Visibility.Visible;
                    p3.IsEnabled = true;

                    t3.Width = p3.Width;
                    t3.Margin = new Thickness(p3.Margin.Left, p3.Margin.Top + p3.Height, p3.Margin.Right, 0);
                    t3.TextAlignment = TextAlignment.Center;
                    t3.Text = playerName;
                    break;
                case 4:
                    p4.Content = img;
                    p4.Tag = playerName;
                    p4.Opacity = 100;
                    p4.Visibility = System.Windows.Visibility.Visible;
                    p4.IsEnabled = true;

                    t4.Width = p4.Width;
                    t4.Margin = new Thickness(p4.Margin.Left, p4.Margin.Top + p4.Height, p4.Margin.Right, 0);
                    t4.TextAlignment = TextAlignment.Center;
                    t4.Text = playerName;
                    break;
                case 5:
                    p5.Content = img;
                    p5.Tag = playerName;
                    p5.Opacity = 100;
                    p5.Visibility = System.Windows.Visibility.Visible;
                    p5.IsEnabled = true;

                    t5.Width = p1.Width;
                    t5.Margin = new Thickness(p5.Margin.Left, p5.Margin.Top + p5.Height, p5.Margin.Right, 0);
                    t5.TextAlignment = TextAlignment.Center;
                    t5.Text = playerName;
                    break;
                case 6:
                    p6.Content = img;
                    p6.Tag = playerName;
                    p6.Opacity = 100;
                    p6.Visibility = System.Windows.Visibility.Visible;
                    p6.IsEnabled = true;

                    t6.Width = p6.Width;
                    t6.Margin = new Thickness(p6.Margin.Left, p6.Margin.Top + p6.Height, p6.Margin.Right, 0);
                    t6.TextAlignment = TextAlignment.Center;
                    t6.Text = playerName;
                    break;
                case 7:
                    p7.Content = img;
                    p7.Tag = playerName;
                    p7.Opacity = 100;
                    p7.Visibility = System.Windows.Visibility.Visible;
                    p7.IsEnabled = true;

                    t7.Width = p1.Width;
                    t7.Margin = new Thickness(p7.Margin.Left, p7.Margin.Top + p7.Height, p7.Margin.Right, 0);
                    t7.TextAlignment = TextAlignment.Center;
                    t7.Text = playerName;
                    break;
                case 8:
                    p8.Content = img;
                    p8.Tag = playerName;
                    p8.Opacity = 100;
                    p8.Visibility = System.Windows.Visibility.Visible;
                    p8.IsEnabled = true;

                    t8.Width = p8.Width;
                    t8.Margin = new Thickness(p8.Margin.Left, p8.Margin.Top + p8.Height, p8.Margin.Right, 0);
                    t8.TextAlignment = TextAlignment.Center;
                    t8.Text = playerName;
                    break;
                case 9:
                    p9.Content = img;
                    p9.Tag = playerName;
                    p9.Opacity = 100;
                    p9.Visibility = System.Windows.Visibility.Visible;
                    p9.IsEnabled = true;

                    t9.Width = p9.Width;
                    t9.Margin = new Thickness(p9.Margin.Left, p9.Margin.Top + p9.Height, p9.Margin.Right, 0);
                    t9.TextAlignment = TextAlignment.Center;
                    t9.Text = playerName;
                    break;
                case 10:
                    p10.Content = img;
                    p10.Tag = playerName;
                    p10.Opacity = 100;
                    p10.Visibility = System.Windows.Visibility.Visible;
                    p10.IsEnabled = true;

                    t10.Width = p10.Width;
                    t10.Margin = new Thickness(p10.Margin.Left, p10.Margin.Top + p10.Height, p10.Margin.Right, 0);
                    t10.TextAlignment = TextAlignment.Center;
                    t10.Text = playerName;
                    break;
                case 11:
                    p11.Content = img;
                    p11.Tag = playerName;
                    p11.Opacity = 100;
                    p11.Visibility = System.Windows.Visibility.Visible;
                    p11.IsEnabled = true;

                    t11.Width = p11.Width;
                    t11.Margin = new Thickness(p11.Margin.Left, p11.Margin.Top + p11.Height, p11.Margin.Right, 0);
                    t11.TextAlignment = TextAlignment.Center;
                    t11.Text = playerName;
                    break;
                case 12:
                    p12.Content = img;
                    p12.Tag = playerName;
                    p12.Opacity = 100;
                    p12.Visibility = System.Windows.Visibility.Visible;
                    p12.IsEnabled = true;

                    t12.Width = p12.Width;
                    t12.Margin = new Thickness(p12.Margin.Left, p12.Margin.Top + p12.Height, p12.Margin.Right, 0);
                    t12.TextAlignment = TextAlignment.Center;
                    t12.Text = playerName;
                    break;
                case 13:
                    p13.Content = img;
                    p13.Tag = playerName;
                    p13.Opacity = 100;
                    p13.Visibility = System.Windows.Visibility.Visible;
                    p13.IsEnabled = true;

                    t13.Width = p13.Width;
                    t13.Margin = new Thickness(p13.Margin.Left, p13.Margin.Top + p13.Height, p13.Margin.Right, 0);
                    t13.TextAlignment = TextAlignment.Center;
                    t13.Text = playerName;
                    break;
                case 14:
                    p14.Content = img;
                    p14.Tag = playerName;
                    p14.Opacity = 100;
                    p14.Visibility = System.Windows.Visibility.Visible;
                    p14.IsEnabled = true;

                    t14.Width = p14.Width;
                    t14.Margin = new Thickness(p14.Margin.Left, p14.Margin.Top + p14.Height, p14.Margin.Right, 0);
                    t14.TextAlignment = TextAlignment.Center;
                    t14.Text = playerName;
                    break;
                case 15:
                    p15.Content = img;
                    p15.Tag = playerName;
                    p15.Opacity = 100;
                    p15.Visibility = System.Windows.Visibility.Visible;
                    p15.IsEnabled = true;

                    t15.Width = p15.Width;
                    t15.Margin = new Thickness(p15.Margin.Left, p15.Margin.Top + p15.Height, p15.Margin.Right, 0);
                    t15.TextAlignment = TextAlignment.Center;
                    t15.Text = playerName;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// readRoster() will first check to see if the selected team is correct. Then read the roster
        /// of the selected team. If the roster contains the player jersey number it will use that to 
        /// sort the players. It will then call addPlayer() to add the player to the screen
        /// </summary>
        private void readRoster()
        {
            //error check
            //Not the best way to do the relative path but since for now the code executes either in debug, it will work
            if (!Directory.Exists("../../" + playerPhotoLoc + teamFolder))
            {
                MessageBox.Show(playerPhotoLoc + teamFolder + " directory does not exist");
                return;
            }
            //if it does exist then we check the roster file
            if (!File.Exists("../../" + playerPhotoLoc + teamFolder + "/roster.txt"))
            {
                MessageBox.Show("The roster file for " + Application.Current.Properties[""] + " does not exist");
                return;
            }

            string[] lines = System.IO.File.ReadAllLines("../../" + playerPhotoLoc + teamFolder + "/roster.txt");
            string[] delimiter = new string[] { "," };
            string[] first = lines[0].Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
            int tmp;

            //if the roster contains the jersey numbers
            if (int.TryParse(first[0], out tmp))
            {
                //do a bucket sort
                string[][] sorted = new string[100][];
                for (int x = 0; x < lines.Length; x++)
                {
                    string[] cur = lines[x].Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                    int jersey;
                    int.TryParse(cur[0], out jersey);
                    sorted[jersey] = new string[3];
                    sorted[jersey] = cur;
                }

                //add the players
                int count = 0;
                for (int x = 0; x < sorted.Length; x++)
                {
                    if (sorted[x] != null)
                    {
                        count++;
                        addPlayer(count, sorted[x][1], "../" + playerPhotoLoc + teamFolder + "/" + sorted[x][2]);
                        photos[x] = sorted[x][2];
                    }

                }
            }

            //the roster does not contain the jersey numbers
            else
            {
                //add the player in the order that they appear on the roster file
                for (int x = 0; x < lines.Length; x++)
                {
                    string[] cur = lines[x].Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                    addPlayer(x+1, cur[0], "../" + playerPhotoLoc + teamFolder + "/" + cur[1]);
                    photos[x] = cur[1];
                }
            }
        }

        /// <summary> 
        /// This method will be called once a user selects a player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playerSelected(object sender, RoutedEventArgs e)
        {
            var temp = (KinectTileButton)sender;
            string name = (string)temp.Tag;

            App.Current.Properties["PlayerPhoto"] = getPhotoLocation(temp.Name.ToString());
            App.Current.Properties["Player"] = (String)temp.Tag.ToString();
            
            Console.WriteLine("IN PLAYER SELECT: App.Current.Properties['Player'] " + App.Current.Properties["Player"].ToString());

            Shooting p = new Shooting();
            p.PassedSensorChooser = sensorChooser;
            sensorChooser.Kinect.SkeletonFrameReady -= Kinect_SkeletonFrameReady;
            Switcher.Switch(p);
        }

        private string getPhotoLocation(string tile)
        {
            string fileName = "";
            switch (tile)
            {
                case "p1":
                    fileName = photos[0];
                    break;
                case "p2":
                    fileName = photos[1];
                    break;
                case "p3":
                    fileName = photos[2];
                    break;
                case "p4":
                    fileName = photos[3];
                    break;
                case "p5":
                    fileName = photos[4];
                    break;
                case "p6":
                    fileName = photos[5];
                    break;
                case "p7":
                    fileName = photos[6];
                    break;
                case "p8":
                    fileName = photos[7];
                    break;
                case "p9":
                    fileName = photos[8];
                    break;
                case "p10":
                    fileName = photos[9];
                    break;
                case "p11":
                    fileName = photos[10];
                    break;
                case "p12":
                    fileName = photos[11];
                    break;
                case "p13":
                    fileName = photos[12];
                    break;
                case "p14":
                    fileName = photos[13];
                    break;
                case "p15":
                    fileName = photos[14];
                    break;
                default:
                    break;
            }
            return fileName;
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            sensorChooser.Kinect.SkeletonFrameReady -= Kinect_SkeletonFrameReady;
            TeamSelect p = new TeamSelect();
            p.PassedSensorChooser = sensorChooser;
            Switcher.Switch(p);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            sensorChooser.Kinect.SkeletonFrameReady -= Kinect_SkeletonFrameReady;
            TeamSelect t = new TeamSelect();
            t.PassedSensorChooser = sensorChooser;
            Switcher.Switch(t);
        }
    }
}

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
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;
using Gestures;

namespace Hoops.Screens
{
    /// <summary>
    /// Interaction logic for Shooting.xaml
    /// </summary>
    public partial class Shooting : UserControl, ISwitchable
    {
        static ShootingGesture _gesture = new ShootingGesture();
        static TimeOutGesture timeOutGesture = new TimeOutGesture();
        static PassingGesture passingGesture = new PassingGesture();
        string[] bioStats = new string[10];

        public Shooting()
        {
            InitializeComponent();

            loadFromDatabase();
            load((string)App.Current.Properties["Team"], (string)App.Current.Properties["Player"], bioStats[1]);

        }
        public void UtilizeState(object state)
        {
            Loaded +=Shooting_Loaded;
        }
        private void Shooting_Loaded(object sender, RoutedEventArgs e)
        {
            var sensor = KinectSensor.KinectSensors.Where(
                                   s => s.Status == KinectStatus.Connected).FirstOrDefault();

            if (sensor != null)
            {
                sensor.SkeletonStream.Enable();
                sensor.SkeletonFrameReady += Sensor_SkeletonFrameReady;
                _gesture.GestureRecognized += Gesture_GestureRecognized;
                timeOutGesture.GestureRecognized += timeOutGesture_GestureRecognized;
                passingGesture.GestureRecognized += passingGesture_GestureRecognized;  
                sensor.Start();
            }
        }

        void timeOutGesture_GestureRecognized(object sender, EventArgs e)
        {
            Switcher.Switch(new PlayerSelect());
        }

        void passingGesture_GestureRecognized(object sender, EventArgs e)
        {
            Switcher.Switch(new TeamSelect());
        }
        private void load(string team, string player, string number)
        {
            DoubleAnimation labelAnimation = new DoubleAnimation();
            labelAnimation.To = 60;
            labelAnimation.AutoReverse = true;
            labelAnimation.RepeatBehavior = RepeatBehavior.Forever;
            labelAnimation.Duration = new Duration(TimeSpan.FromSeconds(.3));
            label.BeginAnimation(FontSizeProperty, labelAnimation);

            // Set the text for the name 
            if (player.Length > 13)
                playerNameLabel.FontSize = 120;
            playerNameLabel.Text = player;
            playerNumberLabel.Text = number;

            string shortTeam = "";
            string teamName = "";
            string conference = "";
            string color1 = "";
            string color2 = "";

            if (team == "atlanta")
            {
                shortTeam = "ATL";
                conference = "East";
                color1 = "#01244C";
                color2 = "#D21933";
                teamName = "hawks";
            }
            else if (team == "boston")
            {
                shortTeam = "BOS";
                conference = "East";
                color1 = "#FFFFFF";
                color2 = "#009E60";
                teamName = "celtics";
            }
            else if (team == "brooklyn")
            {
                shortTeam = "BRK";
                conference = "East";
                color1 = "#FFFFFF";
                color2 = "#8A8D8F";
                teamName = "nets";
            }
            else if (team == "charlotte")
            {
                shortTeam = "CHA";
                conference = "East";
                color1 = "#002B5C";
                color2 = "#F26531";
                teamName = "bobcats";
            }
            else if (team == "chicago")
            {
                shortTeam = "CHI";
                conference = "East";
                color1 = "#FFFFFF";
                color2 = "#D4001F";
                teamName = "bulls";
            }
            else if (team == "cleveland")
            {
                shortTeam = "CLE";
                conference = "East";
                color1 = "#B99D6A";
                color2 = "#9F1425";
                teamName = "cavaliers";
            }
            else if (team == "detroit")
            {
                shortTeam = "DET";
                conference = "East";
                color1 = "#00519A";
                color2 = "#EB003C";
                teamName = "pistons";
            }
            else if (team == "indiana")
            {
                shortTeam = "IND";
                conference = "East";
                color1 = "#092C57";
                color2 = "#FFC322";
                teamName = "pacers";
            }
            else if (team == "miami")
            {
                shortTeam = "MIA";
                conference = "East";
                color1 = "#FFFFFF";
                color2 = "#B62630";
                teamName = "heat";
            }
            else if (team == "milwaukee")
            {
                shortTeam = "MIL";
                conference = "East";
                color1 = "#003614";
                color2 = "#E32636";
                teamName = "bucks";
            }
            else if (team == "newyork")
            {
                shortTeam = "NYK";
                conference = "East";
                color1 = "#0953A0";
                color2 = "#FF7518";
                teamName = "knicks";
            }
            else if (team == "orlando")
            {
                shortTeam = "ORL";
                conference = "East";
                color1 = "#FFFFFF";
                color2 = "#0047AB";
                teamName = "magic";
            }
            else if (team == "philadelphia")
            {
                shortTeam = "PHI";
                conference = "East";
                color1 = "#D0103A";
                color2 = "#0046AD";
                teamName = "76ers";
            }
            else if (team == "toronto")
            {
                shortTeam = "TOR";
                conference = "East";
                color1 = "#FFFFFF";
                color2 = "#CD1041";
                teamName = "raptors";
            }
            else if (team == "washington")
            {
                shortTeam = "WAS";
                conference = "East";
                color1 = "#002244";
                color2 = "#F0163A";
                teamName = "wizards";
            }
            else if (team == "dallas")
            {
                shortTeam = "DAL";
                conference = "West";
                color1 = "#A9A9A9";
                color2 = "#0B60AD";
                teamName = "mavericks";
            }
            else if (team == "denver")
            {
                shortTeam = "DEN";
                conference = "West";
                color1 = "#4B90CD";
                color2 = "#FDB827";
                teamName = "nuggets";
            }
            else if (team == "goldenstate")
            {
                shortTeam = "GSW";
                conference = "West";
                color1 = "#04529C";
                color2 = "#FFCC33";
                teamName = "warriors";
            }
            else if (team == "houston")
            {
                shortTeam = "HOU";
                conference = "West";
                color1 = "#FFFFFF";
                color2 = "#CE1138";
                teamName = "rockets";
            }
            else if (team == "lac")
            {
                shortTeam = "LAC";
                conference = "West";
                color1 = "#EE2944";
                color2 = "#146AA2";
                teamName = "clippers";
            }
            else if (team == "lal")
            {
                shortTeam = "LAL";
                conference = "West";
                color1 = "#4A2583";
                color2 = "#F5AF1B";
                teamName = "lakers";
            }
            else if (team == "memphis")
            {
                shortTeam = "MEM";
                conference = "West";
                color1 = "#001F70";
                color2 = "#7399C6";
                teamName = "grizzlies";
            }
            else if (team == "minnesota")
            {
                shortTeam = "MIN";
                conference = "West";
                color1 = "#0F4D92";
                color2 = "#8C92AC";
                teamName = "timberwolves";
            }
            else if (team == "neworleans")
            {
                shortTeam = "NOP";
                conference = "West";
                color1 = "#002B5C";
                color2 = "#B4975A";
                teamName = "pelicans";
            }
            else if (team == "okc")
            {
                shortTeam = "OKC";
                conference = "West";
                color1 = "#007DC3";
                color2 = "#F05133";
                teamName = "thunder";
            }
            else if (team == "phoenix")
            {
                shortTeam = "PHO";
                conference = "West";
                color1 = "#1C105E";
                color2 = "#E65F20";
                teamName = "suns";
            }
            else if (team == "portland")
            {
                shortTeam = "POR";
                conference = "West";
                color1 = "#FFFFFF";
                color2 = "#F0163A";
                teamName = "trail blazers";
            }
            else if (team == "sacramento")
            {
                shortTeam = "SAC";
                conference = "West";
                color1 = "#753BBD";
                color2 = "#8A8D8F";
                teamName = "kings";
            }
            else if (team == "sanantonio")
            {
                shortTeam = "SAS";
                conference = "West";
                color1 = "#FFFFFF";
                color2 = "#B1B3B3";
                teamName = "spurs";
            }
            else if (team == "utah")
            {
                shortTeam = "UTA";
                conference = "West";
                color1 = "#00275D";
                color2 = "#FF9100";
                teamName = "jazz";
            }

            // Set the team picture in the back of player photo
            string path1 = "../resources/Screen3/Logos/" + conference + "/" + team + ".png";
            BitmapImage teamPic = new BitmapImage();
            teamPic.BeginInit();
            teamPic.UriSource = new Uri(path1, UriKind.Relative);
            teamPic.EndInit();
            teamLogo.Source = teamPic;

            // Set the player picture
            string temp;
            temp = player.Replace(" ", "_");
            string path2 = "../resources/playerPhotos/" + shortTeam + "/" + temp + ".png";
            BitmapImage playerPhoto = new BitmapImage();
            playerPhoto.BeginInit();
            playerPhoto.UriSource = new Uri(path2, UriKind.Relative);
            playerPhoto.EndInit();
            playerPic.Source = playerPhoto;

            //Update text
            teamLabel.Text = team + " " + teamName;
            numberLabel.Text = "number: " + bioStats[1];
            positionLabel.Text = "position: " + bioStats[3];
            birthdate.Text = "birthdate: " + bioStats[6];
            height.Text = "height: " + bioStats[4].Replace("-", "'") + "\"";
            weight.Text = "weight: " + bioStats[5] + "lbs";
            college.Text = bioStats[8];
            experience.Text = "experience: " + bioStats[7];
            salary.Text = "salary: " + bioStats[9];

        }

        void loadFromDatabase()
        {
            // LOAD INFO FROM DATABASE TO ARRAY

            // Sample data
            bioStats[0] = "LAL";
            bioStats[1] = "5";
            bioStats[2] = "Kobe Bryant";
            bioStats[3] = "SG";
            bioStats[4] = "6-11";
            bioStats[5] = "260";
            bioStats[6] = "July 29 1982";
            bioStats[7] = "2";
            bioStats[8] = "Duke";
            bioStats[9] = "$17000000";
        
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
                            _gesture.Update(user);
                            timeOutGesture.Update(user);
                            passingGesture.Update(user);
                        }
                    }
                }
            }
        }

        static void Gesture_GestureRecognized(object sender, EventArgs e)
        {
            Switcher.Switch(new Stats());
        }

       
       
    }
}

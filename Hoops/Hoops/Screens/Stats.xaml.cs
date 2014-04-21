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
using System.Media;
using Gestures;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;


namespace Hoops.Screens
{
    /// <summary>
    /// Interaction logic for Stats.xaml
    /// </summary>
    public partial class Stats : UserControl, ISwitchable
    {
        int count = 0;
        TranslateTransform[,] pointTransformsArray = new TranslateTransform[8, 3];
        Rectangle[,] graphsArray = new Rectangle[8, 2];
        TextBlock[] labelsArray = new TextBlock[8];
        TextBlock[] statslabelsArray = new TextBlock[8];
        string[] labelsInfobox = new string[8];
        SoundPlayer dribbleSound;
        TeamColors teamColors;
        double totalNBAPlayers;
        double totalTeamPlayers;
        string playerNumber;

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
        private TimeOutGesture timeOutGesture = new TimeOutGesture();
        private PassingGesture passingGesture = new PassingGesture();
        private BouncingGesture bouncingGesture = new BouncingGesture();
        private BouncingGestureBack bouncingGestureBack = new BouncingGestureBack();

        string[,] statsArray = new string[8, 3];

        // Class to have team colors to bind it in the xaml fie
        class TeamColors
        {
            public Brush color1 { get; set; }
            public Brush color2 { get; set; }
        }
        public Stats()
        {
            InitializeComponent();
            populateArrays();
            loadStats();
            loadInfo((string)App.Current.Properties["Team"], (string)App.Current.Properties["Player"], playerNumber);
        }

        /* *************** GESTURES ******************************/
        private void Stats_Loaded(object sender, RoutedEventArgs e)
        {
                sensorChooser.Kinect.SkeletonFrameReady += Sensor_SkeletonFrameReady;
                timeOutGesture.GestureRecognized += timeOutGesture_GestureRecognized;
                passingGesture.GestureRecognized += passingGesture_GestureRecognized;
                bouncingGesture.GestureRecognized += bouncingGesture_GestureRecognized;
                bouncingGestureBack.GestureRecognized += bouncingGestureBack_GestureRecognized;
        }

        void bouncingGestureBack_GestureRecognized(object sender, EventArgs e)
        {
            backward();
            Console.WriteLine("BACKWARD counter " + count);
        }

        void bouncingGesture_GestureRecognized(object sender, EventArgs e)
        {
            forward();
            Console.WriteLine("FORWARD counter " + count);
        }

        void timeOutGesture_GestureRecognized(object sender, EventArgs e)
        {
            sensorChooser.Kinect.SkeletonFrameReady -= Sensor_SkeletonFrameReady;
            TeamSelect t = new TeamSelect();
            t.PassedSensorChooser = sensorChooser;
            Switcher.Switch(t);
        }

        void passingGesture_GestureRecognized(object sender, EventArgs e)
        {
            sensorChooser.Kinect.SkeletonFrameReady -= Sensor_SkeletonFrameReady;
            PlayerSelect p = new PlayerSelect();
            p.PassedSensorChooser = sensorChooser;
            Switcher.Switch(p);
        }
        void Sensor_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
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
                            timeOutGesture.Update(user);
                            passingGesture.Update(user);
                            bouncingGesture.Update(user);
                        }
                    }
                }
            }
        }


        /************************END GESTURES **************************/

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void PREV_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Hoops.Screens.Shooting());
        }

      //  private void Button_Click(object sender, RoutedEventArgs e)
        private void forward()
        {
            if (count < 8)
            {
                // Playsound
                dribbleSound.Play();

                //Random random = new Random();
                //double randomNum1 = random.Next(0, 330);
                //double randomNum2 = random.Next(0, 330);

                // Get the ranking to adjust the bar graph heights
                string rankString1 = statsArray[count, 1];
                double dRank1 = Convert.ToDouble(rankString1);
                double percent1 = dRank1 / totalTeamPlayers;
                double result1 = 330.0 * percent1;
                result1 = 330.0 - result1;

                string rankString2 = statsArray[count, 2];
                double dRank2 = Convert.ToDouble(rankString2);
                double percent2 = dRank2 / totalNBAPlayers;
                double result2 = 330.0 * percent2;
                result2 = 330.0 - result2;

                // Update the ranking labels in the right box
                rank1.Text = rankString1 + " / " + totalTeamPlayers.ToString();
                rank2.Text = rankString2 + " / " + totalNBAPlayers.ToString();

                //double changeL = randomNum1;
                //double changeR = randomNum2;
                double changeL = result1;
                double changeR = result2;
                double changeP = 0;

                // Max size of 330 for the graphs
                if (changeL > 330)
                    changeL = 330;
                if (changeR > 330)
                    changeR = 330;

                // Move point a little bit over the biggest graph
                if (changeL > changeR)
                    changeP = -(changeL + 5);
                else
                    changeP = -(changeR + 5);

                // Animation for Team Graph
                DoubleAnimation graphAnimation1 = new DoubleAnimation();
                graphAnimation1.To = changeL;
                graphAnimation1.Duration = new Duration(TimeSpan.FromSeconds(1));
                // Animation for NBA Graph
                DoubleAnimation graphAnimation2 = new DoubleAnimation();
                graphAnimation2.To = changeR;
                graphAnimation2.Duration = new Duration(TimeSpan.FromSeconds(1));
                // Animation for points in graph
                DoubleAnimation pointAnimation = new DoubleAnimation();
                pointAnimation.To = changeP;
                pointAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
                // Animation for selector
                DoubleAnimation selectorAnimation = new DoubleAnimation();
                selectorAnimation.To = (count) * 100;
                selectorAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(500));
                // Animation for labels in bottom
                DoubleAnimation labelsAnimation = new DoubleAnimation();
                labelsAnimation.To = 40;
                labelsAnimation.AutoReverse = true;
                labelsAnimation.RepeatBehavior = new RepeatBehavior(2);
                labelsAnimation.Duration = new Duration(TimeSpan.FromSeconds(.2));
                // Animation label on right

                DoubleAnimation statsAnimation = new DoubleAnimation();
                statsAnimation.To = 80;
                statsAnimation.AutoReverse = true;
                statsAnimation.RepeatBehavior = new RepeatBehavior(2);
                statsAnimation.Duration = new Duration(TimeSpan.FromSeconds(.2));

                // Animate point
                pointTransformsArray[count, 0].BeginAnimation(TranslateTransform.YProperty, pointAnimation);
                pointTransformsArray[count, 1].BeginAnimation(TranslateTransform.YProperty, pointAnimation);
                pointTransformsArray[count, 2].BeginAnimation(TranslateTransform.YProperty, pointAnimation);
                // Animate graphs
                graphsArray[count, 0].BeginAnimation(HeightProperty, graphAnimation1);
                graphsArray[count, 1].BeginAnimation(HeightProperty, graphAnimation2);
                // Animate selector
                selectorTrans.BeginAnimation(TranslateTransform.XProperty, selectorAnimation);
                //Animate labels
                labelsArray[count].BeginAnimation(FontSizeProperty, labelsAnimation);
                // Update info box on right
                StatLabel2.Text = statslabelsArray[count].Text;
                StatLabel.Text = labelsInfobox[count];
                //Animate stats on right box
                StatLabel2.BeginAnimation(FontSizeProperty, statsAnimation);
                count += 1;
            }

        }
      //  private void Button_Click_1(object sender, RoutedEventArgs e)
        private void backward()
        {
            if (count > 1)
            {
                // Play Sound
                dribbleSound.Play();

                count -= 1;

                DoubleAnimation selectorAnimation = new DoubleAnimation();
                selectorAnimation.To = (count) * 100 - 100;
                selectorAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(500));
                selectorTrans.BeginAnimation(TranslateTransform.XProperty, selectorAnimation);

                // Animation for labels in bottom
                DoubleAnimation labelsAnimation = new DoubleAnimation();
                labelsAnimation.To = 40;
                labelsAnimation.AutoReverse = true;
                labelsAnimation.RepeatBehavior = new RepeatBehavior(2);
                labelsAnimation.Duration = new Duration(TimeSpan.FromSeconds(.2));
                // Animation label on right
                DoubleAnimation statsAnimation = new DoubleAnimation();
                statsAnimation.To = 80;
                statsAnimation.AutoReverse = true;
                statsAnimation.RepeatBehavior = new RepeatBehavior(2);
                statsAnimation.Duration = new Duration(TimeSpan.FromSeconds(.2));

                //Animate labels
                labelsArray[count - 1].BeginAnimation(FontSizeProperty, labelsAnimation);

                StatLabel.Text = labelsInfobox[count - 1];
                StatLabel2.Text = statslabelsArray[count - 1].Text;
                //Animate stats on right box
                StatLabel2.BeginAnimation(FontSizeProperty, statsAnimation);

                string rankString1 = statsArray[count - 1, 1];
                string rankString2 = statsArray[count - 1, 2];
                // Update the ranking labels in the right box
                rank1.Text = rankString1 + " / " + totalTeamPlayers.ToString();
                rank2.Text = rankString2 + " / " + totalNBAPlayers.ToString();
            }
        }


        private void populateArrays()
        {
            // Store points, bars and their transforms for animations
            pointTransformsArray[0, 0] = point1Trans;
            pointTransformsArray[0, 1] = point1Trans2;
            pointTransformsArray[0, 2] = stat1Trans;
            graphsArray[0, 0] = Graph1_Team;
            graphsArray[0, 1] = Graph1_Nba;

            pointTransformsArray[1, 0] = point2Trans;
            pointTransformsArray[1, 1] = point2Trans2;
            pointTransformsArray[1, 2] = stat2Trans;
            graphsArray[1, 0] = Graph2_Team;
            graphsArray[1, 1] = Graph2_Nba;

            pointTransformsArray[2, 0] = point3Trans;
            pointTransformsArray[2, 1] = point3Trans2;
            pointTransformsArray[2, 2] = stat3Trans;
            graphsArray[2, 0] = Graph3_Team;
            graphsArray[2, 1] = Graph3_Nba;

            pointTransformsArray[3, 0] = point4Trans;
            pointTransformsArray[3, 1] = point4Trans2;
            pointTransformsArray[3, 2] = stat4Trans;
            graphsArray[3, 0] = Graph4_Team;
            graphsArray[3, 1] = Graph4_Nba;

            pointTransformsArray[4, 0] = point5Trans;
            pointTransformsArray[4, 1] = point5Trans2;
            pointTransformsArray[4, 2] = stat5Trans;
            graphsArray[4, 0] = Graph5_Team;
            graphsArray[4, 1] = Graph5_Nba;

            pointTransformsArray[5, 0] = point6Trans;
            pointTransformsArray[5, 1] = point6Trans2;
            pointTransformsArray[5, 2] = stat6Trans;
            graphsArray[5, 0] = Graph6_Team;
            graphsArray[5, 1] = Graph6_Nba;

            pointTransformsArray[6, 0] = point7Trans;
            pointTransformsArray[6, 1] = point7Trans2;
            pointTransformsArray[6, 2] = stat7Trans;
            graphsArray[6, 0] = Graph7_Team;
            graphsArray[6, 1] = Graph7_Nba;

            pointTransformsArray[7, 0] = point8Trans;
            pointTransformsArray[7, 1] = point8Trans2;
            pointTransformsArray[7, 2] = stat8Trans;
            graphsArray[7, 0] = Graph8_Team;
            graphsArray[7, 1] = Graph8_Nba;

            // Store the labels in the bottom to update them when you move to them
            labelsArray[0] = label1;
            labelsArray[1] = label2;
            labelsArray[2] = label3;
            labelsArray[3] = label4;
            labelsArray[4] = label5;
            labelsArray[5] = label6;
            labelsArray[6] = label7;
            labelsArray[7] = label8;

            //Store the stats labels to update them when you move to them
            statslabelsArray[0] = stat1;
            statslabelsArray[1] = stat2;
            statslabelsArray[2] = stat3;
            statslabelsArray[3] = stat4;
            statslabelsArray[4] = stat5;
            statslabelsArray[5] = stat6;
            statslabelsArray[6] = stat7;
            statslabelsArray[7] = stat8;

            // Fill in the Labels that will be shown in the info box on the right
            labelsInfobox[0] = "Points Per Game";
            labelsInfobox[1] = "Assists Per Game";
            labelsInfobox[2] = "Rebounds Per Game";
            labelsInfobox[3] = "3 Pointers Per Game";
            labelsInfobox[4] = "Blocks Per Game";
            labelsInfobox[5] = "Steals Per Game";
            labelsInfobox[6] = "Field Goals Per Game";
            labelsInfobox[7] = "Free Throws Per Game";

            // Create the sound clip for dribble
            dribbleSound = new SoundPlayer("./resources/Screen6/dribble.wav");

        }

        private void loadStats()
        {
            //***************************************************************************
            //
            // GET DATA FROM DATABASE HERE
            //
            //***************************************************************************
            statsArray[0, 0] = "12.3";
            statsArray[0, 1] = "1";
            statsArray[0, 2] = "275";
            statsArray[1, 0] = "5.2";
            statsArray[1, 1] = "7";
            statsArray[1, 2] = "1";
            statsArray[2, 0] = "6.1";
            statsArray[2, 1] = "1";
            statsArray[2, 2] = "275";
            statsArray[3, 0] = "3.5";
            statsArray[3, 1] = "7";
            statsArray[3, 2] = "1";
            statsArray[4, 0] = "0.7";
            statsArray[4, 1] = "1";
            statsArray[4, 2] = "275";
            statsArray[5, 0] = "4.6";
            statsArray[5, 1] = "7";
            statsArray[5, 2] = "1";
            statsArray[6, 0] = "10.3";
            statsArray[6, 1] = "1";
            statsArray[6, 2] = "275";
            statsArray[7, 0] = "6.6";
            statsArray[7, 1] = "7";
            statsArray[7, 2] = "1";

            //***********************************************
            // Get the total number of players for rankings
            //***********************************************
            totalNBAPlayers = 550;
            totalTeamPlayers = 15;

            for (int i = 0; i < 8; i++)
            {
                statslabelsArray[i].Text = statsArray[i, 0];
            }

            //***********************************
            // Get player number from database
            //***********************************
            playerNumber = "24";

        }

        private void loadInfo(string team, string player, string number)
        {
            if (player.Length > 13)
                playerNameLabel.FontSize = 120;
            playerNameLabel.Text = player;
            playerNumberLabel.Text = number;

            string shortTeam = "";
            string conference = "";
            string color1 = "";
            string color2 = "";

            if (team == "atlanta")
            {
                shortTeam = "ATL";
                conference = "East";
                color1 = "#01244C";
                color2 = "#D21933";
            }
            else if (team == "boston")
            {
                shortTeam = "BOS";
                conference = "East";
                color1 = "#FFFFFF";
                color2 = "#009E60";
            }
            else if (team == "brooklyn")
            {
                shortTeam = "BRK";
                conference = "East";
                color1 = "#FFFFFF";
                color2 = "#8A8D8F";
            }
            else if (team == "charlotte")
            {
                shortTeam = "CHA";
                conference = "East";
                color1 = "#002B5C";
                color2 = "#F26531";
            }
            else if (team == "chicago")
            {
                shortTeam = "CHI";
                conference = "East";
                color1 = "#FFFFFF";
                color2 = "#D4001F";
            }
            else if (team == "cleveland")
            {
                shortTeam = "CLE";
                conference = "East";
                color1 = "#B99D6A";
                color2 = "#9F1425";
            }
            else if (team == "detroit")
            {
                shortTeam = "DET";
                conference = "East";
                color1 = "#00519A";
                color2 = "#EB003C";
            }
            else if (team == "indiana")
            {
                shortTeam = "IND";
                conference = "East";
                color1 = "#092C57";
                color2 = "#FFC322";
            }
            else if (team == "miami")
            {
                shortTeam = "MIA";
                conference = "East";
                color1 = "#FFFFFF";
                color2 = "#B62630";
            }
            else if (team == "milwaukee")
            {
                shortTeam = "MIL";
                conference = "East";
                color1 = "#003614";
                color2 = "#E32636";
            }
            else if (team == "newyork")
            {
                shortTeam = "NYK";
                conference = "East";
                color1 = "#0953A0";
                color2 = "#FF7518";
            }
            else if (team == "orlando")
            {
                shortTeam = "ORL";
                conference = "East";
                color1 = "#FFFFFF";
                color2 = "#0047AB";
            }
            else if (team == "philadelphia")
            {
                shortTeam = "PHI";
                conference = "East";
                color1 = "#D0103A";
                color2 = "#0046AD";
            }
            else if (team == "toronto")
            {
                shortTeam = "TOR";
                conference = "East";
                color1 = "#FFFFFF";
                color2 = "#CD1041";
            }
            else if (team == "washington")
            {
                shortTeam = "WAS";
                conference = "East";
                color1 = "#002244";
                color2 = "#F0163A";
            }
            else if (team == "dallas")
            {
                shortTeam = "DAL";
                conference = "West";
                color1 = "#A9A9A9";
                color2 = "#0B60AD";
            }
            else if (team == "denver")
            {
                shortTeam = "DEN";
                conference = "West";
                color1 = "#4B90CD";
                color2 = "#FDB827";
            }
            else if (team == "goldenstate")
            {
                shortTeam = "GSW";
                conference = "West";
                color1 = "#04529C";
                color2 = "#FFCC33";
            }
            else if (team == "houston")
            {
                shortTeam = "HOU";
                conference = "West";
                color1 = "#FFFFFF";
                color2 = "#CE1138";
            }
            else if (team == "lac")
            {
                shortTeam = "LAC";
                conference = "West";
                color1 = "#EE2944";
                color2 = "#146AA2";
            }
            else if (team == "lal")
            {
                shortTeam = "LAL";
                conference = "West";
                color1 = "#4A2583";
                color2 = "#F5AF1B";
            }
            else if (team == "memphis")
            {
                shortTeam = "MEM";
                conference = "West";
                color1 = "#001F70";
                color2 = "#7399C6";
            }
            else if (team == "minnesota")
            {
                shortTeam = "MIN";
                conference = "West";
                color1 = "#0F4D92";
                color2 = "#8C92AC";
            }
            else if (team == "neworleans")
            {
                shortTeam = "NOP";
                conference = "West";
                color1 = "#002B5C";
                color2 = "#B4975A";
            }
            else if (team == "okc")
            {
                shortTeam = "OKC";
                conference = "West";
                color1 = "#007DC3";
                color2 = "#F05133";
            }
            else if (team == "phoenix")
            {
                shortTeam = "PHO";
                conference = "West";
                color1 = "#1C105E";
                color2 = "#E65F20";
            }
            else if (team == "portland")
            {
                shortTeam = "POR";
                conference = "West";
                color1 = "#FFFFFF";
                color2 = "#F0163A";
            }
            else if (team == "sacramento")
            {
                shortTeam = "SAC";
                conference = "West";
                color1 = "#753BBD";
                color2 = "#8A8D8F";
            }
            else if (team == "sanantonio")
            {
                shortTeam = "SAS";
                conference = "West";
                color1 = "#FFFFFF";
                color2 = "#B1B3B3";
            }
            else if (team == "utah")
            {
                shortTeam = "UTA";
                conference = "West";
                color1 = "#00275D";
                color2 = "#FF9100";
            }

            // Set team colors in graphs and graph points
            BrushConverter converter1 = new System.Windows.Media.BrushConverter();
            BrushConverter converter2 = new System.Windows.Media.BrushConverter();
            Brush teamColor1 = (Brush)converter1.ConvertFromString(color1);
            Brush teamColor2 = (Brush)converter2.ConvertFromString(color2);
            teamColors = new TeamColors { color1 = teamColor1, color2 = teamColor2 };
            this.DataContext = teamColors;
            if (color1 == "#FFFFFF")
                keyLabel.Foreground = System.Windows.Media.Brushes.Black;

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

        }

    }
}

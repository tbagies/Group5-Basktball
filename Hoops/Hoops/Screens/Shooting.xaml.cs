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

namespace Hoops.Screens
{
    /// <summary>
    /// Interaction logic for Shooting.xaml
    /// </summary>
    public partial class Shooting : UserControl, ISwitchable
    {
        public Shooting()
        {
            InitializeComponent();
            load((string)App.Current.Properties["Team"], (string)App.Current.Properties["Player"], "1");
        }
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        //event handler for going to next screen, for now it uses a button
        private void NEXT_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Stats());
        }

        private void PREV_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new PlayerSelect());
        }

        private void load(string team, string player, string number)
        {
            // Animate the label in the bottom
            DoubleAnimation labelAnimation = new DoubleAnimation();
            labelAnimation.To = 60;
            labelAnimation.AutoReverse = true;
            labelAnimation.RepeatBehavior = RepeatBehavior.Forever;
            labelAnimation.Duration = new Duration(TimeSpan.FromSeconds(.3));
            label.BeginAnimation(FontSizeProperty, labelAnimation);

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

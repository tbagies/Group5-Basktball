using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;

namespace DatabaseConnectionClass
{
    class Program
    {
        static void Main(string[] args)
        {
            // Demo
            string[] testBio = GetPlayerBio("ATL", "Kyle Korver");
            for (int i = 0; i < testBio.Length; i++)
            {
                Console.WriteLine(testBio[i]);
            }
            Console.WriteLine();
            Console.Write("Total NBA Players: ");
            Console.WriteLine(TotalNBAPlayers());
            Console.WriteLine();

            Console.Write("Total ATL Players: ");
            Console.WriteLine(TotalTeamPlayers("ATL"));

            Console.WriteLine();
            string[,] testRank = GetPlayerStatsRankings("ATL", "Kyle Korver");
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(testRank[i, j] + " ");
                    if (j == 2)
                    {
                        Console.WriteLine();
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
        }

        // Total number of players in the NBA
        public static string TotalNBAPlayers()
        {
            string Total = "";

            // Create the connection string
            SqlConnection myConnection = new SqlConnection("user id=David-PC/David;" +
                                                    "Password=validpassword;Server=localhost;" +
                                                    "Trusted_Connection=yes;" +
                                                    "Database=Hoops; " +
                                                    "Connection Timeout=30");

            // Get connected
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            // Create SQL command
            SqlCommand myCommand = new SqlCommand("Command String", myConnection);

            // Give it a command to perform
            StringBuilder BuildCommand = new StringBuilder();
            BuildCommand.Append("select COUNT(*) as Total from dbo.Payroll");

            myCommand.CommandText = BuildCommand.ToString();

            // read the data
            try
            {
                SqlDataReader myReader = null;
                myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    Total = myReader["Total"].ToString().Trim();
                    break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            // close it
            try
            {
                myConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return Total;

        }

        // Total number of players on a team
        public static string TotalTeamPlayers(string TeamName)
        {
            string Total = "";

            // Create the connection string
            SqlConnection myConnection = new SqlConnection("user id=David-PC/David;" +
                                                    "Password=validpassword;Server=localhost;" +
                                                    "Trusted_Connection=yes;" +
                                                    "Database=Hoops; " +
                                                    "Connection Timeout=30");

            // Get connected
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            // Create SQL command
            SqlCommand myCommand = new SqlCommand("Command String", myConnection);

            // Give it a command to perform
            StringBuilder BuildCommand = new StringBuilder();
            BuildCommand.Append("select COUNT(*) as Total from dbo.Rosters Where team = '");
            BuildCommand.Append(TeamName);
            BuildCommand.Append("'");

            myCommand.CommandText = BuildCommand.ToString();

            // read the data
            try
            {
                SqlDataReader myReader = null;
                myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    Total = myReader["Total"].ToString().Trim();
                    break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            // close it
            try
            {
                myConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return Total;

        }

        // Given the Team Name and Player name, return a 2-D array with the 8 stats and the rankings on the team and in the NBA
        // For example, [8][3] is returned in the following order:
        // [FG% stat, team ranking, NBA ranking]
        // [3P% stat, team ranking, NBA ranking]
        // [FT% stat, team ranking, NBA ranking]
        // [RB stat, team ranking, NBA ranking]
        // [AST stat, team ranking, NBA ranking]
        // [STL stat, team ranking, NBA ranking]
        // [BLK stat, team ranking, NBA ranking]
        // [PTS stat, team ranking, NBA ranking]
        public static string[,] GetPlayerStatsRankings(string TeamName, string PlayerName)
        {
            string[,] returnRanks = new string[8,3];

            // Create the connection string
            SqlConnection myConnection = new SqlConnection("user id=David-PC/David;" +
                                                    "Password=validpassword;Server=localhost;" +
                                                    "Trusted_Connection=yes;" +
                                                    "Database=Hoops; " +
                                                    "Connection Timeout=30");

            // Get connected
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            // Create SQL command
            SqlCommand myCommand = new SqlCommand("Command String", myConnection);

            string[] playerStats = new string[8];

            // Give it a command to perform
            StringBuilder BuildCommand = new StringBuilder();
            BuildCommand.Append("Select Top 1 ");
            BuildCommand.Append("R.FGP, R.ThreePP, R.FTP, R.TRB, R.AST, R.STL, R.BLK, R.PTS ");
            BuildCommand.Append("From dbo.Stats as R ");
            BuildCommand.Append("Where R.Team = '");
            BuildCommand.Append(TeamName.Trim());
            BuildCommand.Append("' And R.Player = '");
            BuildCommand.Append(PlayerName.Trim());
            BuildCommand.Append("'");

            myCommand.CommandText = BuildCommand.ToString();

            // read the data
            try
            {
                SqlDataReader myReader = null;
                myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    playerStats[0] = myReader["FGP"].ToString().Trim();
                    playerStats[1] = myReader["ThreePP"].ToString().Trim();
                    playerStats[2] = myReader["FTP"].ToString().Trim();
                    playerStats[3] = myReader["TRB"].ToString().Trim();
                    playerStats[4] = myReader["AST"].ToString().Trim();
                    playerStats[5] = myReader["STL"].ToString().Trim();
                    playerStats[6] = myReader["BLK"].ToString().Trim();
                    playerStats[7] = myReader["PTS"].ToString().Trim();
                    break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            // close it
            try
            {
                myConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            for (int i = 0; i < 8; i++)
            {
                returnRanks[i, 0] = playerStats[i];
            }

            return returnRanks;
        }

        // Get Player Bio info from team and player names
        // Return Array is: Team, Number, Player, Position, Height, Weight, Birthdate, Experience, College, Salary
        // Change user id value in myConnection to match the user's id for SQL Server Management Studio
        public static string[] GetPlayerBio(string TeamName, string PlayerName)
        {
            string[] returnBio = new string[10];

            // Create the connection string
            SqlConnection myConnection = new SqlConnection("user id=David-PC/David;" +
                                                    "Password=validpassword;Server=localhost;" +
                                                    "Trusted_Connection=yes;" +
                                                    "Database=Hoops; " +
                                                    "Connection Timeout=30");

            // Get connected
            try
            {
                myConnection.Open();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            // Create SQL command
            SqlCommand myCommand = new SqlCommand("Command String", myConnection);

            // Give it a command to perform
            StringBuilder BuildCommand = new StringBuilder();
            BuildCommand.Append("Select Top 1 ");
            BuildCommand.Append("R.Team, R.Number, R.Player, R.Position, R.Height, R.Weight, R.Birthdate, R.Experience, R.College, P.Salary ");
            BuildCommand.Append("From dbo.Rosters as R ");
            BuildCommand.Append("Inner Join dbo.Payroll as P ");
            BuildCommand.Append("On R.Team = P.Team ");
            BuildCommand.Append("Where R.Team = '");
            BuildCommand.Append(TeamName.Trim());
            BuildCommand.Append("' And R.Player = '");
            BuildCommand.Append(PlayerName.Trim());
            BuildCommand.Append("'");

            myCommand.CommandText = BuildCommand.ToString();

            // read the data
            try{
                SqlDataReader myReader = null;
                myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    returnBio[0] = myReader["Team"].ToString().Trim();
                    returnBio[1] = myReader["Number"].ToString().Trim();
                    returnBio[2] = myReader["Player"].ToString().Trim();
                    returnBio[3] = myReader["Position"].ToString().Trim();
                    returnBio[4] = myReader["Height"].ToString().Trim();
                    returnBio[5] = myReader["Weight"].ToString().Trim();
                    returnBio[6] = myReader["Birthdate"].ToString().Trim();
                    returnBio[7] = myReader["Experience"].ToString().Trim();
                    returnBio[8] = myReader["College"].ToString().Trim();
                    returnBio[9] = myReader["Salary"].ToString().Trim();
                    break;
                }
            }
            catch(Exception e) 
            {
                Console.WriteLine(e.ToString());
            }

            // close it
            try {
                myConnection.Close();
            }
            catch(Exception e) 
            {
                Console.WriteLine(e.ToString());
            }
            return returnBio;
        }
    }
}

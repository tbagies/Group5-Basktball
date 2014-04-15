Dave Bai CS422 Hoops Project

1) Place both HoopsData.DLL and data.txt files in the same directory as your executable. ***IMPORTANT, Please do this exactly***

2) Add a reference to the HoopsData.DLL file. Also, add "using HoopsData" to the top of your code. 

3) To run use the class in your program, use Class1.METHODNAMEHERE();

4) The following are the methods: 

******************************************************************************************************************
a) string TotalNBAPlayers(); 
	Output is the total number of players in the NBA.

b) string TotalTeamPlayers(string TeamName);
	Input is the team name.
	Output is the total number of players on the given team.

c) string[] GetPlayerBio(string TeamName, string PlayerName);
	Input is the team name, player name. 
	Output is string[10] = {Team, Number, Player, Position, Height, Weight, Birthdate, Experience, College, Salary}.

	- NOTE: Returns [,,,,,,,,,] if no bio info available. 
	(i.e. if output[0,0] == "", then no bio is available for the player)

d) string[,] GetPlayerStats(string TeamName, string PlayerName);
	Input is the team name, player name. 
	Output is string[11,3] = [FG%, 0, 0]
				 [3P%, 0, 0]
				 [FT%, 0, 0]
				 [TRB, Team Rank, NBA Rank]
				 [AST, Team Rank, NBA Rank]
				 [STL, Team Rank, NBA Rank]
				 [BLK, Team Rank, NBA Rank]
				 [PTS, Team Rank, NBA Rank]
				 [FG,  Team Rank, NBA Rank]
				 [3P,  Team Rank, NBA Rank]
				 [FT,  Team Rank, NBA Rank]
	
	- NOTE: Returns array of: 
				 [, 0, 0]
				 [, 0, 0]
				 [, 0, 0]
				 [,,,]
				 [,,,]
				 [,,,]
				 [,,,]
				 [,,,]
				 [,,,]
				 [,,,]
				 [,,,]

	 			 if no stats are available. 
				 (ie. if output[0,0] == "", then no stats are available for the player)
******************************************************************************************************************

5) For example, to use a method, you would have: 
	
	string[] bio = Class1.GetPlayerBio("CHI", "Joakim Noah");
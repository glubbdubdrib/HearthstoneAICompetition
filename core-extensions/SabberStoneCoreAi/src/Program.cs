#region copyright
// SabberStone, Hearthstone Simulator in C# .NET Core
// Copyright (C) 2017-2019 SabberStone Team, darkfriend77 & rnilva
//
// SabberStone is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License.
// SabberStone is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
#endregion

// this is just a random comment
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SabberStoneCore.Config;
using SabberStoneCore.Enums;
using SabberStoneCoreAi.POGame;
using SabberStoneCoreAi.Agent.ExampleAgents;
using SabberStoneCoreAi.Agent;

namespace SabberStoneCoreAi
{
	internal class Program
	{

		private static void Main()
		{
			Console.WriteLine("Setup gameConfig");
			GameConfig gameConfig = gameConfigCoevoluationary(args);

			Console.WriteLine("Setup POGameHandler");
			//AbstractAgent player1agent = new ParametricGreedyAgent();
			//((ParametricGreedyAgent)player1agent).setAgeintWeightsFromString(args[2]);
			Console.WriteLine("Attempting to istantiate AlvaroMCTS agent...");
			//AbstractAgent player1agent = new EVA();
			//((EVA)player1agent).InitializeAgent();
			AbstractAgent player1agent = new AlvaroAgent();
			//AbstractAgent player2agent = new ParametricGreedyAgent();
			//((ParametricGreedyAgent)player2agent).setAgeintWeightsFromString(args[5]);
			AbstractAgent player2agent = new EVA();
			((EVA)player2agent).InitializeAgent();
			POGameHandler gameHandler = new POGameHandler(gameConfig, player1agent, player2agent, debug:false);
			gameConfig.StartPlayer = -1; //Pick random start player

			Console.WriteLine("STARTING GAMES");
			int numGames = Int32.Parse(args[6]);

			gameHandler.PlayGames(numGames);
			GameStats gameStats = gameHandler.getGameStats();
			//gameStats.printResults();
			int p1wins = gameStats.PlayerA_Wins;
			int p2wins = gameStats.PlayerB_Wins;
			Console.WriteLine(p1wins+" "+p2wins+" "+ numGames+ " " +
				gameStats.PlayerA_TurnsToWin+" "+
				gameStats.PlayerA_TurnsToLose+" "+
				gameStats.PlayerA_HealthDifferenceWinning + " " +
				gameStats.PlayerA_HealthDifferenceLosing
				);

//			Console.WriteLine("Setup gameConfig");
//
//			var gameConfig = new GameConfig()
//			{
//				StartPlayer = 1,
//				Player1HeroClass = CardClass.MAGE,
//				Player2HeroClass = CardClass.MAGE,
//				FillDecks = true,
//				Shuffle = true,
//				Logging = false
//			};
//
//			Console.WriteLine("Setup POGameHandler");
//			AbstractAgent player1 = new GreedyAgent();
//			AbstractAgent player2 = new MyAgent();
//			var gameHandler = new POGameHandler(gameConfig, player1, player2, repeatDraws:false);
//
//			Console.WriteLine("Simulate Games");
//			//gameHandler.PlayGame();
//			gameHandler.PlayGames(nr_of_games:1, addResultToGameStats:true, debug:false);
//			GameStats gameStats = gameHandler.getGameStats();
//
//			gameStats.printResults();
//
//			Console.WriteLine("Test successful");
//			Console.ReadLine();
		}
	}
}

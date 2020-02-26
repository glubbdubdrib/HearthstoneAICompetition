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
using SabberStoneCoreAi.Meta;
using SabberStoneCore.Model;

namespace SabberStoneCoreAi
{
	internal class Program
	{
		private static CardClass stringToCardClass(string c ) {
			if (c.Equals("MAGE"))
				return CardClass.MAGE;
			if (c.Equals("PALADIN"))
				return CardClass.PALADIN;
			if (c.Equals("PRIEST"))
				return CardClass.PRIEST;

			if (c.Equals("DRUID"))
				return CardClass.DRUID;
			if (c.Equals("HUNTER"))
				return CardClass.HUNTER;
			if (c.Equals("ROGUE"))
				return CardClass.ROGUE;

			if (c.Equals("SHAMAN"))
				return CardClass.SHAMAN;
			if (c.Equals("WARLOCK"))
				return CardClass.WARLOCK;
			if (c.Equals("WARRIOR"))
				return CardClass.WARRIOR;


			throw new Exception("CARD CLASS NOT VALID");

		}

		private static List<Card> stringToDeck(string c)
		{
			if (c.Equals("RenoKazakusMage"))
				return Decks.RenoKazakusMage;
			if (c.Equals("MidrangeJadeShaman"))
				return Decks.MidrangeJadeShaman;
			if (c.Equals("AggroPirateWarrior"))
				return Decks.AggroPirateWarrior;
			throw new Exception("DECK DOES NOT EXIST");
		}

		private static void Main(string[] args)
		{
			Console.WriteLine("Setup gameConfig");

			AbstractAgent agent1 = null;
			AbstractAgent agent2 = null;
			if (args[0].Equals("EVA"))
			{
				Console.WriteLine("EVA loaded for agent 1");
				agent1 = new EVA();
				((EVA)agent1).InitializeAgent();
			}
			else if (args[0].Equals("AlvaroAgent"))
			{
				agent1 = new AlvaroAgent();
				((AlvaroAgent)agent1).InitializeAgent();
			}
			if (args[3].Equals("EVA"))
			{
				Console.WriteLine("EVA loaded for agent 2");
				agent2 = new EVA();
				((EVA)agent2).InitializeAgent();
			}
			else if (args[3].Equals("AlvaroAgent"))
			{
				agent2 = new AlvaroAgent();
				((AlvaroAgent)agent2).InitializeAgent();
			}

			Console.WriteLine("Agents loaded!");

			var gameConfig = new GameConfig()
			{
				StartPlayer = -1, // set to -1 should randomize starting player every game
//				Player1HeroClass = CardClass.MAGE,
//				Player2HeroClass = CardClass.MAGE,
				FillDecks = true,
				Shuffle = true,
				Logging = false,
				Player1HeroClass = stringToCardClass(args[2]),
				Player2HeroClass = stringToCardClass(args[5]),
//				FillDecks = false,
//				Logging = false,
				Player1Deck = stringToDeck(args[1]),
				Player2Deck = stringToDeck(args[4]) //RenoKazakusMage
			};

			int numGames = Int32.Parse(args[6]);

			Console.WriteLine("Setup POGameHandler");
			var gameHandler = new POGameHandler(gameConfig, agent1, agent2, repeatDraws:false);

			Console.WriteLine("Simulate Games");
			//gameHandler.PlayGame();
			gameHandler.PlayGames(nr_of_games:numGames, addResultToGameStats:true, debug:false);
			GameStats gameStats = gameHandler.getGameStats();

			gameStats.printResults();
			Console.WriteLine("Player 1: " + args[0] + " with deck " + args[1]);
			Console.WriteLine("Player 2: " + args[3] + " with deck " + args[4]);

			Console.WriteLine("Test successful");
//			Console.ReadLine();
		}
	}
}

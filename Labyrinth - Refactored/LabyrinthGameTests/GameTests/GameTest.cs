using System.IO;
using LabyrinthGame.Game;
using LabyrinthGameTests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LabyrinthGameTests.GameTests
{
	[TestClass]
	public class GameTest
	{
		[TestMethod]
		public void TestMain_StartNewGame_WelcomeMessagePrinted()
		{
			string consoleInput = "exit\n";
			StringWriter consoleWriter;
			TestUtilities.RedirectConsoleStram(consoleInput, out consoleWriter);
			Game_Accessor.Main();
			string actual = TestUtilities.GetConsoleOutput(consoleWriter);
			string expected =
							 "Welcome to “Labirinth” game. Please try to escape." +
							 " Use \'top\' to view the top scoreboard, \'restart\' to start a new game and \'exit\' to quit the game.";
			bool isMessageDisplayed = actual.Contains(expected);
			Assert.IsTrue(isMessageDisplayed);
		}
	}
}
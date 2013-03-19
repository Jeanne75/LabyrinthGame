using System;
using System.IO;
using LabyrinthGameTests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LabyrinthGame;
using LabyrinthGame.Game;
using LabyrinthGame.Utilities;

namespace LabyrinthGameTests.GameTests
{
	[TestClass]
	public class LabyrinthTest
	{
		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestInitializeLabyrinthMap_CreateGame0x7_ArgumnetExceptionThrown()
		{
			Labyrinth_Accessor target = new Labyrinth_Accessor(0, 7, 3, 3);
			target.InitializeLabyrinthMap();
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestInitializeLabyrinthMap_CreateGameInvalidRowsCount_ArgumnetExceptionThrown()
		{
			Labyrinth_Accessor target = new Labyrinth_Accessor(-1, 7, 3, 3);
			target.InitializeLabyrinthMap();
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestInitializeLabyrinthMap_CreateGameInvalidColumnsCount_ArgumnetExceptionThrown()
		{
			Labyrinth_Accessor target = new Labyrinth_Accessor(7, -1, 3, 3);
			target.InitializeLabyrinthMap();
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestInitializeLabyrinthMap_PlayerPositionRowInvalid_ArgumnetExceptionThrown()
		{
			Labyrinth_Accessor target = new Labyrinth_Accessor(7, 7, -1, 3);
			target.InitializeLabyrinthMap();
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestInitializeLabyrinthMap_PlayerPositionRowOutOfRange_ArgumnetExceptionThrown()
		{
			Labyrinth_Accessor target = new Labyrinth_Accessor(7, 7, 7, 3);
			target.InitializeLabyrinthMap();
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestInitializeLabyrinthMap_PlayerPositionColumnInvalid_ArgumnetExceptionThrown()
		{
			Labyrinth_Accessor target = new Labyrinth_Accessor(7, 7, 3, -1);
			target.InitializeLabyrinthMap();
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestInitializeLabyrinthMap_PlayerPositionColumnOutOfRange_ArgumnetExceptionThrown()
		{
			Labyrinth_Accessor target = new Labyrinth_Accessor(7, 7, 3, 7);
			target.InitializeLabyrinthMap();
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestInitializeLabyrinthMap_CreateGame7x7_LabyrinthSizeIs7Rows()
		{
			Labyrinth_Accessor target = new Labyrinth_Accessor(7, 7, 3, 3);
			target.InitializeLabyrinthMap();
			int expected = 7;
			int actual = target.labyrinthMap.GetLength(0);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestInitializeLabyrinthMap_CreateGame7x7_LabyrinthSizeIs7Columns()
		{
			Labyrinth_Accessor target = new Labyrinth_Accessor(7, 7, 3, 3);
			target.InitializeLabyrinthMap();
			int expected = 7;
			int actual = target.labyrinthMap.GetLength(1);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestInitializeLabyrinthMapInitial_CreateGame7x7_PlayerPositionIsAt3x3()
		{
			Labyrinth_Accessor target = new Labyrinth_Accessor(7, 7, 3, 3);
			target.InitializeLabyrinthMap();
			int expected = 0;
			int actual = target.labyrinthMap[3, 3];
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestInitializeLabyrinthMapInitial_CreateGame7x7_LabyrinthIsSolvable()
		{
			Labyrinth_Accessor target = new Labyrinth_Accessor(7, 7, 3, 3);
			target.InitializeLabyrinthMap();
			PlayerPosition playerInitialPosition = new PlayerPosition(3,3);
			bool isLabirinthSolvable = GameUtilities_Accessor.IsLabyrinthSolvable(target.labyrinthMap, playerInitialPosition);
			Assert.IsTrue(isLabirinthSolvable);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestPrintLabyrinth_CreateGame7x7_DisplayLabyrinthMap()
		{
			Labyrinth_Accessor target = new Labyrinth_Accessor(7, 7, 3, 3);
			target.labyrinthMap = new int[7, 7]
			{
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 0, 0, 0 },
				{ 1, 1, 1, 1, 0, 1, 1 },
				{ 1, 1, 1, 2, 0, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
			};

			string consoleInput = string.Empty;
			StringWriter consoleWriter;
			TestUtilities.RedirectConsoleStram(consoleInput, out consoleWriter);
			target.PrintLabyrinth();
			string actual = TestUtilities.GetConsoleOutput(consoleWriter);
			string expected =
							 "X X X X X X X \r\n" +
							 "X X X X - - - \r\n" +
							 "X X X X - X X \r\n" +
							 "X X X * - X X \r\n" +
							 "X X X X X X X \r\n" +
							 "X X X X X X X \r\n" +
							 "X X X X X X X \r\n";
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestPlay_EnterExitCommand_ExitMessagePrinted()
		{
			Labyrinth_Accessor target = new Labyrinth_Accessor(7, 7, 3, 3);
			string consoleInput = "exit\n";
			StringWriter consoleWriter;
			TestUtilities.RedirectConsoleStram(consoleInput, out consoleWriter);
			target.Play();
			string actual = TestUtilities.GetConsoleOutput(consoleWriter);
			string expected =
							 "Enter your move (L=left, R=right, U=up, D=down): " +
							 "Good Bye!\r\n";
			bool isMessageDisplayed = actual.Contains(expected);
			Assert.IsTrue(isMessageDisplayed);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestPlay_MoveRightSmallLetter_PayerPositionMoved()
		{
			Labyrinth_Accessor target = new Labyrinth_Accessor(7, 7, 3, 3);
			target.labyrinthMap = new int[7, 7]
			{
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 0, 0, 0 },
				{ 1, 1, 0, 0, 0, 1, 1 },
				{ 1, 1, 0, 0, 0, 1, 1 },
				{ 1, 1, 1, 0, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
			};
			string consoleInput = "r\r\n" + "exit\n";
			StringWriter consoleWriter;
			TestUtilities.RedirectConsoleStram(consoleInput, out consoleWriter);
			target.Play();
			string actual = TestUtilities.GetConsoleOutput(consoleWriter);
			string expected =
							 "Enter your move (L=left, R=right, U=up, D=down): " +
							 "X X X X X X X \r\n" +
							 "X X X X - - - \r\n" +
							 "X X - - - X X \r\n" +
							 "X X - - * X X \r\n" +
							 "X X X - X X X \r\n" +
							 "X X X X X X X \r\n" +
							 "X X X X X X X \r\n";
			bool isMessageDisplayed = actual.Contains(expected);
			Assert.IsTrue(isMessageDisplayed);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestPlay_MoveRightBigLetter_PayerPositionMoved()
		{
			Labyrinth_Accessor target = new Labyrinth_Accessor(7, 7, 3, 3);
			target.labyrinthMap = new int[7, 7]
			{
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 0, 0, 0 },
				{ 1, 1, 0, 0, 0, 1, 1 },
				{ 1, 1, 0, 0, 0, 1, 1 },
				{ 1, 1, 1, 0, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
			};
			string consoleInput = "R\r\n" + "exit\n";
			StringWriter consoleWriter;
			TestUtilities.RedirectConsoleStram(consoleInput, out consoleWriter);
			target.Play();
			string actual = TestUtilities.GetConsoleOutput(consoleWriter);
			string expected =
							 "Enter your move (L=left, R=right, U=up, D=down): " +
							 "X X X X X X X \r\n" +
							 "X X X X - - - \r\n" +
							 "X X - - - X X \r\n" +
							 "X X - - * X X \r\n" +
							 "X X X - X X X \r\n" +
							 "X X X X X X X \r\n" +
							 "X X X X X X X \r\n";
			bool isMessageDisplayed = actual.Contains(expected);
			Assert.IsTrue(isMessageDisplayed);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestPlay_MoveLeftSmallLetter_PayerPositionMoved()
		{
			Labyrinth_Accessor target = new Labyrinth_Accessor(7, 7, 3, 3);
			target.labyrinthMap = new int[7, 7]
			{
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 0, 0, 0 },
				{ 1, 1, 0, 0, 0, 1, 1 },
				{ 1, 1, 0, 0, 0, 1, 1 },
				{ 1, 1, 1, 0, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
			};
			string consoleInput = "l\r\n" + "exit\n";
			StringWriter consoleWriter;
			TestUtilities.RedirectConsoleStram(consoleInput, out consoleWriter);
			target.Play();
			string actual = TestUtilities.GetConsoleOutput(consoleWriter);
			string expected =
							 "Enter your move (L=left, R=right, U=up, D=down): " +
							 "X X X X X X X \r\n" +
							 "X X X X - - - \r\n" +
							 "X X - - - X X \r\n" +
							 "X X * - - X X \r\n" +
							 "X X X - X X X \r\n" +
							 "X X X X X X X \r\n" +
							 "X X X X X X X \r\n";
			bool isMessageDisplayed = actual.Contains(expected);
			Assert.IsTrue(isMessageDisplayed);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestPlay_MoveLeftBigLetter_PayerPositionMoved()
		{
			Labyrinth_Accessor target = new Labyrinth_Accessor(7, 7, 3, 3);
			target.labyrinthMap = new int[7, 7]
			{
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 0, 0, 0 },
				{ 1, 1, 0, 0, 0, 1, 1 },
				{ 1, 1, 0, 0, 0, 1, 1 },
				{ 1, 1, 1, 0, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
			};
			string consoleInput = "L\r\n" + "exit\n";
			StringWriter consoleWriter;
			TestUtilities.RedirectConsoleStram(consoleInput, out consoleWriter);
			target.Play();
			string actual = TestUtilities.GetConsoleOutput(consoleWriter);
			string expected =
							 "Enter your move (L=left, R=right, U=up, D=down): " +
							 "X X X X X X X \r\n" +
							 "X X X X - - - \r\n" +
							 "X X - - - X X \r\n" +
							 "X X * - - X X \r\n" +
							 "X X X - X X X \r\n" +
							 "X X X X X X X \r\n" +
							 "X X X X X X X \r\n";
			bool isMessageDisplayed = actual.Contains(expected);
			Assert.IsTrue(isMessageDisplayed);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestPlay_MoveUpSmallLetter_PayerPositionMoved()
		{
			Labyrinth_Accessor target = new Labyrinth_Accessor(7, 7, 3, 3);
			target.labyrinthMap = new int[7, 7]
			{
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 0, 0, 0 },
				{ 1, 1, 0, 0, 0, 1, 1 },
				{ 1, 1, 0, 0, 0, 1, 1 },
				{ 1, 1, 1, 0, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
			};
			string consoleInput = "u\r\n" + "exit\n";
			StringWriter consoleWriter;
			TestUtilities.RedirectConsoleStram(consoleInput, out consoleWriter);
			target.Play();
			string actual = TestUtilities.GetConsoleOutput(consoleWriter);
			string expected =
							 "Enter your move (L=left, R=right, U=up, D=down): " +
							 "X X X X X X X \r\n" +
							 "X X X X - - - \r\n" +
							 "X X - * - X X \r\n" +
							 "X X - - - X X \r\n" +
							 "X X X - X X X \r\n" +
							 "X X X X X X X \r\n" +
							 "X X X X X X X \r\n";
			bool isMessageDisplayed = actual.Contains(expected);
			Assert.IsTrue(isMessageDisplayed);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestPlay_MoveUpBigLetter_PayerPositionMoved()
		{
			Labyrinth_Accessor target = new Labyrinth_Accessor(7, 7, 3, 3);
			target.labyrinthMap = new int[7, 7]
			{
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 0, 0, 0 },
				{ 1, 1, 0, 0, 0, 1, 1 },
				{ 1, 1, 0, 0, 0, 1, 1 },
				{ 1, 1, 1, 0, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
			};
			string consoleInput = "U\r\n" + "exit\n";
			StringWriter consoleWriter;
			TestUtilities.RedirectConsoleStram(consoleInput, out consoleWriter);
			target.Play();
			string actual = TestUtilities.GetConsoleOutput(consoleWriter);
			string expected =
							 "Enter your move (L=left, R=right, U=up, D=down): " +
							 "X X X X X X X \r\n" +
							 "X X X X - - - \r\n" +
							 "X X - * - X X \r\n" +
							 "X X - - - X X \r\n" +
							 "X X X - X X X \r\n" +
							 "X X X X X X X \r\n" +
							 "X X X X X X X \r\n";
			bool isMessageDisplayed = actual.Contains(expected);
			Assert.IsTrue(isMessageDisplayed);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestPlay_MoveDownSmallLetter_PayerPositionMoved()
		{
			Labyrinth_Accessor target = new Labyrinth_Accessor(7, 7, 3, 3);
			target.labyrinthMap = new int[7, 7]
			{
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 0, 0, 0 },
				{ 1, 1, 0, 0, 0, 1, 1 },
				{ 1, 1, 0, 0, 0, 1, 1 },
				{ 1, 1, 1, 0, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
			};
			string consoleInput = "d\r\n" + "exit\n";
			StringWriter consoleWriter;
			TestUtilities.RedirectConsoleStram(consoleInput, out consoleWriter);
			target.Play();
			string actual = TestUtilities.GetConsoleOutput(consoleWriter);
			string expected =
							 "Enter your move (L=left, R=right, U=up, D=down): " +
							 "X X X X X X X \r\n" +
							 "X X X X - - - \r\n" +
							 "X X - - - X X \r\n" +
							 "X X - - - X X \r\n" +
							 "X X X * X X X \r\n" +
							 "X X X X X X X \r\n" +
							 "X X X X X X X \r\n";
			bool isMessageDisplayed = actual.Contains(expected);
			Assert.IsTrue(isMessageDisplayed);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestPlay_MoveDownBigLetter_PayerPositionMoved()
		{
			Labyrinth_Accessor target = new Labyrinth_Accessor(7, 7, 3, 3);
			target.labyrinthMap = new int[7, 7]
			{
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 0, 0, 0 },
				{ 1, 1, 0, 0, 0, 1, 1 },
				{ 1, 1, 0, 0, 0, 1, 1 },
				{ 1, 1, 1, 0, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
			};
			string consoleInput = "D\r\n" + "exit\n";
			StringWriter consoleWriter;
			TestUtilities.RedirectConsoleStram(consoleInput, out consoleWriter);
			target.Play();
			string actual = TestUtilities.GetConsoleOutput(consoleWriter);
			string expected =
							 "Enter your move (L=left, R=right, U=up, D=down): " +
							 "X X X X X X X \r\n" +
							 "X X X X - - - \r\n" +
							 "X X - - - X X \r\n" +
							 "X X - - - X X \r\n" +
							 "X X X * X X X \r\n" +
							 "X X X X X X X \r\n" +
							 "X X X X X X X \r\n";
			bool isMessageDisplayed = actual.Contains(expected);
			Assert.IsTrue(isMessageDisplayed);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestPlay_EnterInvalidCommand_InvalidMoveMessagePrinted()
		{
			Labyrinth_Accessor target = new Labyrinth_Accessor(7, 7, 3, 3);
			string consoleInput = "invalid\n" + "exit\n";
			StringWriter consoleWriter;
			TestUtilities.RedirectConsoleStram(consoleInput, out consoleWriter);
			target.Play();
			string actual = TestUtilities.GetConsoleOutput(consoleWriter);
			string expected =
							 "Enter your move (L=left, R=right, U=up, D=down): " +
							 "Invalid move!\r\n" +
							 "Enter your move (L=left, R=right, U=up, D=down): " +
							 "Good Bye!\r\n";
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestPlay_MoveRightToBlockedCell_InvalidMoveMEssagePrinted()
		{
			Labyrinth_Accessor target = new Labyrinth_Accessor(7, 7, 3, 3);
			target.labyrinthMap = new int[7, 7]
			{
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 0, 0, 0 },
				{ 1, 1, 0, 1, 0, 1, 1 },
				{ 1, 1, 1, 2, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
			};
			string consoleInput = "R\r\n" + "exit\n";
			StringWriter consoleWriter;
			TestUtilities.RedirectConsoleStram(consoleInput, out consoleWriter);
			target.Play();
			string actual = TestUtilities.GetConsoleOutput(consoleWriter);
			string expected =
							 "Enter your move (L=left, R=right, U=up, D=down): " +
							 "Invalid move!\r\n" +
							 "Enter your move (L=left, R=right, U=up, D=down): " +
							 "Good Bye!\r\n";
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestPlay_MoveLeftToBlockedCell_InvalidMoveMEssagePrinted()
		{
			Labyrinth_Accessor target = new Labyrinth_Accessor(7, 7, 3, 3);
			target.labyrinthMap = new int[7, 7]
			{
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 0, 0, 0 },
				{ 1, 1, 0, 1, 0, 1, 1 },
				{ 1, 1, 1, 2, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
			};
			string consoleInput = "L\r\n" + "exit\n";
			StringWriter consoleWriter;
			TestUtilities.RedirectConsoleStram(consoleInput, out consoleWriter);
			target.Play();
			string actual = TestUtilities.GetConsoleOutput(consoleWriter);
			string expected =
							 "Enter your move (L=left, R=right, U=up, D=down): " +
							 "Invalid move!\r\n" +
							 "Enter your move (L=left, R=right, U=up, D=down): " +
							 "Good Bye!\r\n";
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestPlay_MoveUpToBlockedCell_InvalidMoveMEssagePrinted()
		{
			Labyrinth_Accessor target = new Labyrinth_Accessor(7, 7, 3, 3);
			target.labyrinthMap = new int[7, 7]
			{
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 0, 0, 0 },
				{ 1, 1, 0, 1, 0, 1, 1 },
				{ 1, 1, 1, 2, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
			};
			string consoleInput = "U\r\n" + "exit\n";
			StringWriter consoleWriter;
			TestUtilities.RedirectConsoleStram(consoleInput, out consoleWriter);
			target.Play();
			string actual = TestUtilities.GetConsoleOutput(consoleWriter);
			string expected =
							 "Enter your move (L=left, R=right, U=up, D=down): " +
							 "Invalid move!\r\n" +
							 "Enter your move (L=left, R=right, U=up, D=down): " +
							 "Good Bye!\r\n";
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestPlay_MoveDownToBlockedCell_InvalidMoveMEssagePrinted()
		{
			Labyrinth_Accessor target = new Labyrinth_Accessor(7, 7, 3, 3);
			target.labyrinthMap = new int[7, 7]
			{
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 0, 0, 0 },
				{ 1, 1, 0, 1, 0, 1, 1 },
				{ 1, 1, 1, 2, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
			};
			string consoleInput = "D\r\n" + "exit\n";
			StringWriter consoleWriter;
			TestUtilities.RedirectConsoleStram(consoleInput, out consoleWriter);
			target.Play();
			string actual = TestUtilities.GetConsoleOutput(consoleWriter);
			string expected =
							 "Enter your move (L=left, R=right, U=up, D=down): " +
							 "Invalid move!\r\n" +
							 "Enter your move (L=left, R=right, U=up, D=down): " +
							 "Good Bye!\r\n";
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestPlay_SolveTheGameFiveMoves_ScoreRecordedScoreboeadPrinted()
		{
			Labyrinth_Accessor target = new Labyrinth_Accessor(7, 7, 3, 3);
			target.labyrinthMap = new int[7, 7]
			{
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 0, 0, 0 },
				{ 1, 1, 0, 0, 0, 1, 1 },
				{ 1, 1, 0, 0, 0, 1, 1 },
				{ 1, 1, 1, 0, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
			};
			string consoleInput =
								 "U\r\n" +
								 "R\r\n" +
								 "U\r\n" +
								 "R\r\n" +
								 "R\r\n" +
								 "Player 1\n" +
								 "exit\n";
			StringWriter consoleWriter;
			TestUtilities.RedirectConsoleStram(consoleInput, out consoleWriter);
			target.Play();
			string actual = TestUtilities.GetConsoleOutput(consoleWriter);
			string expected =
							 "Enter your move (L=left, R=right, U=up, D=down): " +
							 "X X X X X X X \r\n" +
							 "X X X X - - - \r\n" +
							 "X X - * - X X \r\n" +
							 "X X - - - X X \r\n" +
							 "X X X - X X X \r\n" +
							 "X X X X X X X \r\n" +
							 "X X X X X X X \r\n" +
							 "Enter your move (L=left, R=right, U=up, D=down): " +
							 "X X X X X X X \r\n" +
							 "X X X X - - - \r\n" +
							 "X X - - * X X \r\n" +
							 "X X - - - X X \r\n" +
							 "X X X - X X X \r\n" +
							 "X X X X X X X \r\n" +
							 "X X X X X X X \r\n" +
							 "Enter your move (L=left, R=right, U=up, D=down): " +
							 "X X X X X X X \r\n" +
							 "X X X X * - - \r\n" +
							 "X X - - - X X \r\n" +
							 "X X - - - X X \r\n" +
							 "X X X - X X X \r\n" +
							 "X X X X X X X \r\n" +
							 "X X X X X X X \r\n" +
							 "Enter your move (L=left, R=right, U=up, D=down): " +
							 "X X X X X X X \r\n" +
							 "X X X X - * - \r\n" +
							 "X X - - - X X \r\n" +
							 "X X - - - X X \r\n" +
							 "X X X - X X X \r\n" +
							 "X X X X X X X \r\n" +
							 "X X X X X X X \r\n" +
							 "Enter your move (L=left, R=right, U=up, D=down): " +
							 "Congratulations! You escaped in 5 moves.\r\n" +
							 "Please enter your name for the top scoreboard: " +
							 "Scoreboard:\r\n" +
							 "1. Player 1 --> 5 moves";
			bool isMessageDisplayed = actual.Contains(expected);
			Assert.IsTrue(isMessageDisplayed);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestPlay_SolveTheGameOneMove_ScoreRecordedScoreboeadPrinted()
		{
			Labyrinth_Accessor target = new Labyrinth_Accessor(7, 7, 1, 5);
			target.labyrinthMap = new int[7, 7]
			{
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 0, 0, 0 },
				{ 1, 1, 0, 0, 0, 1, 1 },
				{ 1, 1, 0, 0, 0, 1, 1 },
				{ 1, 1, 1, 0, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
				{ 1, 1, 1, 1, 1, 1, 1 },
			};
			string consoleInput =
								 "R\r\n" +
								 "Player 1\n" +
								 "exit\n";
			StringWriter consoleWriter;
			TestUtilities.RedirectConsoleStram(consoleInput, out consoleWriter);
			target.Play();
			string actual = TestUtilities.GetConsoleOutput(consoleWriter);
			string expected =
							 "Enter your move (L=left, R=right, U=up, D=down): " +
							 "Congratulations! You escaped in 1 moves.\r\n" +
							 "Please enter your name for the top scoreboard: " +
							 "Scoreboard:\r\n" +
							 "1. Player 1 --> 1 moves";
			bool isMessageDisplayed = actual.Contains(expected);
			Assert.IsTrue(isMessageDisplayed);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestPlay_EnterShowScoreboadCommand_MessageScoreBoardIsEmpty()
		{
			Labyrinth_Accessor target = new Labyrinth_Accessor(7, 7, 3, 3);
			string consoleInput = "top\n" + "exit\n";
			StringWriter consoleWriter;
			TestUtilities.RedirectConsoleStram(consoleInput, out consoleWriter);
			target.Play();
			string actual = TestUtilities.GetConsoleOutput(consoleWriter);
			string expected =
							 "Enter your move (L=left, R=right, U=up, D=down): " +
							 "The scoreboard is empty.\r\n" +
							 "Enter your move (L=left, R=right, U=up, D=down): " +
							 "Good Bye!\r\n";
			Assert.AreEqual(expected, actual);
		}
	}
}
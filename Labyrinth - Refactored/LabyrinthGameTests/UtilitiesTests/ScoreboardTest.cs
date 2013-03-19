using System;
using LabyrinthGame.Utilities;
using LabyrinthGameTests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace LabyrinthGameTests.UtilitiesTests
{
	[TestClass]
	public class ScoreboardTest
	{
		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestScoreboardConstructor_InvalidCound_ThrowException()
		{
			int scoreboardRecordsCount = 0;
			new Scoreboard(scoreboardRecordsCount);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestScoreboardConstructor_SetMaximumRecords5_ScoreRecordsCountIs5()
		{
			Scoreboard_Accessor scoreboard = new Scoreboard_Accessor(5);
			int expected = 5;
			int actual = scoreboard.maximumScoreboeardSize;
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestAddScore_1Record_NumberOfStoredREcordsIs1()
		{
			Scoreboard_Accessor scoreboard = new Scoreboard_Accessor(5);
			scoreboard.AddScore("Record Name", 10);
			int expected = 1;
			int actual = scoreboard.scoreRecordsList.Count;
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestAddScore_3Record_NumberOfStoredREcordsIs3()
		{
			Scoreboard_Accessor scoreboard = new Scoreboard_Accessor(2);
			scoreboard.AddScore("Record Name 1", 10);
			scoreboard.AddScore("Record Name 2", 15);
			scoreboard.AddScore("Record Name 3", 20);
			int expected = 2;
			int actual = scoreboard.scoreRecordsList.Count;
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void TestAddScore_Store3Rrecords_RecordsSortedAscending()
		{
			Scoreboard_Accessor scoreboard = new Scoreboard_Accessor(5);
			scoreboard.AddScore("Record Name Lower", 15);
			scoreboard.AddScore("Record Name Hightr", 10);
			ScoreRecord highScore = (ScoreRecord)scoreboard.scoreRecordsList[0];
			string expected = "Record Name Hightr";
			string actual = highScore.PlayerName;
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestAddScore_StoreRecords10and5and15_LowestRecordIs15()
		{
			Scoreboard_Accessor scoreboard = new Scoreboard_Accessor(5);
			scoreboard.AddScore("Record Name Lower", 10);
			scoreboard.AddScore("Record Name Hightr", 5);
			scoreboard.AddScore("Record Name Lowest", 15);
			int expected = 15;
			int actual = scoreboard.GetLowestScore(scoreboard.scoreRecordsList);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestIsHightScore_StoredRecordIs20MaxRecords1_15IsNewHighScore()
		{
			Scoreboard_Accessor scoreboard = new Scoreboard_Accessor(1);
			scoreboard.AddScore("Record Name Lower", 20);
			bool isHighScore = scoreboard.IsHightScore(15);
			Assert.IsTrue(isHighScore);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestIsHightScore_StoredRecordIs10MaxRecords1_15IsNotNewHighScore()
		{
			Scoreboard_Accessor scoreboard = new Scoreboard_Accessor(1);
			scoreboard.AddScore("Record Name Higher", 10);
			bool isHighScore = scoreboard.IsHightScore(15);
			Assert.IsFalse(isHighScore);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestIsHightScore_StoredRecordIs2MaxRecords5_5IsNewHighScore()
		{
			Scoreboard_Accessor scoreboard = new Scoreboard_Accessor(5);
			scoreboard.AddScore("Record Name Lower", 2);
			bool isHighScore = scoreboard.IsHightScore(5);
			Assert.IsTrue(isHighScore);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestPrintScoreboard_Store1Record_1LinePrinted()
		{
			Scoreboard_Accessor scoreboard = new Scoreboard_Accessor(5);
			scoreboard.AddScore("Record Name Lower", 10);
			string consoleInput = string.Empty;
			StringWriter consoleWriter;
			TestUtilities.RedirectConsoleStram(consoleInput, out consoleWriter);
			scoreboard.PrintScoreboard();
			string actual = TestUtilities.GetConsoleOutput(consoleWriter);
			string expected =
							 "Scoreboard:\r\n" +
							 "1. Record Name Lower --> 10 moves.\r\n";
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		[DeploymentItem("LabyrinthGame.exe")]
		public void TestPrintScoreboard_EmptyList_EmptyScoreboardPrinted()
		{
			Scoreboard_Accessor scoreboard = new Scoreboard_Accessor(5);
			string consoleInput = string.Empty;
			StringWriter consoleWriter;
			TestUtilities.RedirectConsoleStram(consoleInput, out consoleWriter);
			scoreboard.PrintScoreboard();
			string actual = TestUtilities.GetConsoleOutput(consoleWriter);
			string expected = "The scoreboard is empty.\r\n";
			Assert.AreEqual(expected, actual);
		}
	}
}
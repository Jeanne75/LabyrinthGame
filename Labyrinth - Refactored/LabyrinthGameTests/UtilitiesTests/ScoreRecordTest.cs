using System;
using LabyrinthGame.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LabyrinthGameTests.UtilitiesTests
{
	[TestClass]
	public class ScoreRecordTest
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void TestScoreRecordConstructor_CreateInvalidRecord_ExpectedExcetion()
		{
			new ScoreRecord("", 10);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void TestCompareTo_CompareNullObject_ExpectedExcetion()
		{
			ScoreRecord testedHighScore = new ScoreRecord("TestName1", 10);
			testedHighScore.CompareTo(null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestCompareTo_CompareWrongObject_ExpectedExcetion()
		{
			ScoreRecord testedHighScore = new ScoreRecord("TestName1", 10);
			Scoreboard comparableObject = new Scoreboard(1);
			testedHighScore.CompareTo(comparableObject);
		}

		[TestMethod]
		public void TestCompareTo_Compare10to9_SecondIsHigher()
		{
			ScoreRecord testedHighScore1 = new ScoreRecord("TestName1", 10);
			ScoreRecord testedHighScore2 = new ScoreRecord("TestName1", 9);
			int actual = testedHighScore1.CompareTo(testedHighScore2);
			int expected = 1;
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void TestCompareTo_Compare10to9_FirstIsHigher()
		{
			ScoreRecord testedHighScore1 = new ScoreRecord("TestName1", 9);
			ScoreRecord testedHighScore2 = new ScoreRecord("TestName1", 10);
			int actual = testedHighScore1.CompareTo(testedHighScore2);
			int expected = -1;
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void TestCompareTo_Compare9to10_AreEqual()
		{
			ScoreRecord testedHighScore1 = new ScoreRecord("TestName1", 10);
			ScoreRecord testedHighScore2 = new ScoreRecord("TestName1", 10);
			int actual = testedHighScore1.CompareTo(testedHighScore2);
			int expected = 0;
			Assert.AreEqual(expected, actual);
		}
	}
}
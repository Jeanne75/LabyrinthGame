using LabyrinthGame.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LabyrinthGameTests.UtilitiesTests
{
	[TestClass]
	public class GameUtilitiesTest
	{ 
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void TestCheckLabyrinthMapValidity_InvalidLabyrinthMapObject_ThrownException()
		{
			int[,] labyrinthMap = null; 
			GameUtilities_Accessor.CheckLabyrinthMapValidity(labyrinthMap);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestCheckLabyrinthMapValidity_InvalidLabyrinthMapSize_ThrownException()
		{
			int[,] labyrinthMap = new int[0, 0];
			GameUtilities_Accessor.CheckLabyrinthMapValidity(labyrinthMap);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void TestCheckPlayerPositionValidityInLabyrinthMap_InvalidPlayerPositionObject_ThrownException()
		{
			int[,] labyrinthMap = new int[,]
			{
				{ 0, 0, 0 },
				{ 0, 0, 0 },
				{ 0, 0, 0 },
			};
			PlayerPosition playerStartPosition = null;
			GameUtilities_Accessor.CheckPlayerPositionValidityInLabyrinthMap(labyrinthMap, playerStartPosition);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestCheckPlayerPositionValidityInLabyrinthMap_PlayerPositionOutOfRange_ThrownException()
		{
			int[,] labyrinthMap = new int[,]
			{
				{ 0, 0, 0 },
				{ 0, 0, 0 },
				{ 0, 0, 0 },
			};
			PlayerPosition playerStartPosition = new PlayerPosition(3, 3);
			GameUtilities_Accessor.CheckPlayerPositionValidityInLabyrinthMap(labyrinthMap, playerStartPosition);
		}

		[TestMethod]
		public void TestIsPlayerPositionInLabyrinthRange_PlayerPositionInRange_ReturnsTrue()
		{
			int[,] labyrinthMap = new int[,]
			{
				{ 0, 0, 0 },
				{ 0, 0, 0 },
				{ 0, 0, 0 },
			};
			PlayerPosition playerStartPosition = new PlayerPosition(1, 1);
			bool isPlayerPositionInRange = GameUtilities.IsPlayerPositionInLabyrinthRange(labyrinthMap, playerStartPosition);
			Assert.IsTrue(isPlayerPositionInRange);
		}

		[TestMethod]
		public void TestIsPlayerPositionInLabyrinthRange_PlayerPositionOutRange_ReturnsFalse()
		{
			int[,] labyrinthMap = new int[,]
			{
				{ 0, 0, 0 },
				{ 0, 0, 0 },
				{ 0, 0, 0 },
			};
			PlayerPosition playerStartPosition = new PlayerPosition(3, 3);
			bool isPlayerPositionInRange = GameUtilities.IsPlayerPositionInLabyrinthRange(labyrinthMap, playerStartPosition);
			Assert.IsFalse(isPlayerPositionInRange);
		}

		[TestMethod]
		public void TestIsPlayerPositionInLabyrinthRange_PlayerPositionAtLeftBorder_ReturnsTrue()
		{
			int[,] labyrinthMap = new int[,]
			{
				{ 0, 0, 0 },
				{ 0, 0, 0 },
				{ 0, 0, 0 },
			};
			PlayerPosition playerStartPosition = new PlayerPosition(0, 1);
			bool isPlayerPositionInRange = GameUtilities.IsPlayerPositionInLabyrinthRange(labyrinthMap, playerStartPosition);
			Assert.IsTrue(isPlayerPositionInRange);
		}

		[TestMethod]
		public void TestIsPlayerPositionInLabyrinthRange_PlayerPositionAtRightBorder_ReturnsTrue()
		{
			int[,] labyrinthMap = new int[,]
			{
				{ 0, 0, 0 },
				{ 0, 0, 0 },
				{ 0, 0, 0 },
			};
			PlayerPosition playerStartPosition = new PlayerPosition(2, 1);
			bool isPlayerPositionInRange = GameUtilities.IsPlayerPositionInLabyrinthRange(labyrinthMap, playerStartPosition);
			Assert.IsTrue(isPlayerPositionInRange);
		}

		[TestMethod]
		public void TestIsPlayerPositionInLabyrinthRange_PlayerPositionAtTopBorder_ReturnsTrue()
		{
			int[,] labyrinthMap = new int[,]
			{
				{ 0, 0, 0 },
				{ 0, 0, 0 },
				{ 0, 0, 0 },
			};
			PlayerPosition playerStartPosition = new PlayerPosition(1, 0);
			bool isPlayerPositionInRange = GameUtilities.IsPlayerPositionInLabyrinthRange(labyrinthMap, playerStartPosition);
			Assert.IsTrue(isPlayerPositionInRange);
		}

		[TestMethod]
		public void TestIsPlayerPositionInLabyrinthRange_PlayerPositionAtBottomBorder_ReturnsTrue()
		{
			int[,] labyrinthMap = new int[,]
			{
				{ 0, 0, 0 },
				{ 0, 0, 0 },
				{ 0, 0, 0 },
			};
			PlayerPosition playerStartPosition = new PlayerPosition(1, 2);
			bool isPlayerPositionInRange = GameUtilities.IsPlayerPositionInLabyrinthRange(labyrinthMap, playerStartPosition);
			Assert.IsTrue(isPlayerPositionInRange);
		}

		[TestMethod]
		public void TestIsPlayerPositionAtLabyrintBorder_PlayerPositionNotAtBorder_ReturnsFalse()
		{
			int[,] labyrinthMap = new int[,]
			{
				{ 0, 0, 0 },
				{ 0, 0, 0 },
				{ 0, 0, 0 },
			};
			PlayerPosition playerStartPosition = new PlayerPosition(1, 1);
			bool isPlayerPositionAtRange = GameUtilities.IsPlayerPositionAtLabyrintBorder(labyrinthMap, playerStartPosition);
			Assert.IsFalse(isPlayerPositionAtRange);
		}

		[TestMethod]
		public void TestIsPlayerPositionAtLabyrintBorder_PlayerPositionAtLeftBorder_ReturnsTrue()
		{
			int[,] labyrinthMap = new int[,]
			{
				{ 0, 0, 0 },
				{ 0, 0, 0 },
				{ 0, 0, 0 },
			};
			PlayerPosition playerStartPosition = new PlayerPosition(0, 1);
			bool isPlayerPositionAtRange = GameUtilities.IsPlayerPositionAtLabyrintBorder(labyrinthMap, playerStartPosition);
			Assert.IsTrue(isPlayerPositionAtRange);
		}

		[TestMethod]
		public void TestIsPlayerPositionAtLabyrintBorder_PlayerPositionAtRightBorder_ReturnsTrue()
		{
			int[,] labyrinthMap = new int[,]
			{
				{ 0, 0, 0 },
				{ 0, 0, 0 },
				{ 0, 0, 0 },
			};
			PlayerPosition playerStartPosition = new PlayerPosition(2, 1);
			bool isPlayerPositionAtRange = GameUtilities.IsPlayerPositionAtLabyrintBorder(labyrinthMap, playerStartPosition);
			Assert.IsTrue(isPlayerPositionAtRange);
		}

		[TestMethod]
		public void TestIsPlayerPositionAtLabyrintBorder_PlayerPositionAtTopBorder_ReturnsTrue()
		{
			int[,] labyrinthMap = new int[,]
			{
				{ 0, 0, 0 },
				{ 0, 0, 0 },
				{ 0, 0, 0 },
			};
			PlayerPosition playerStartPosition = new PlayerPosition(1, 0);
			bool isPlayerPositionAtRange = GameUtilities.IsPlayerPositionAtLabyrintBorder(labyrinthMap, playerStartPosition);
			Assert.IsTrue(isPlayerPositionAtRange);
		}

		[TestMethod]
		public void TestIsPlayerPositionAtLabyrintBorder_PlayerPositionAtBottomBorder_ReturnsTrue()
		{
			int[,] labyrinthMap = new int[,]
			{
				{ 0, 0, 0 },
				{ 0, 0, 0 },
				{ 0, 0, 0 },
			};
			PlayerPosition playerStartPosition = new PlayerPosition(1, 2);
			bool isPlayerPositionAtRange = GameUtilities.IsPlayerPositionAtLabyrintBorder(labyrinthMap, playerStartPosition);
			Assert.IsTrue(isPlayerPositionAtRange);
		}

		[TestMethod]
		public void TestIsLabyrinthSolvable_LabitynthIsNotSolvable_ReturnsFalse()
		{
			int[,] labyrinthMap = new int[,]
			{
				{ 1, 1, 1 },
				{ 1, 0, 1 },
				{ 1, 1, 1 },
			};
			PlayerPosition playerStartPosition = new PlayerPosition(1, 1);
			bool isLabyrinthSolvable = GameUtilities.IsLabyrinthSolvable(labyrinthMap, playerStartPosition);
			Assert.IsFalse(isLabyrinthSolvable);
		}

		[TestMethod]
		public void TestIsLabyrinthSolvable_LabitynthWithOneExit_ReturnsTrue()
		{
			int[,] labyrinthMap = new int[,]
			{
				{ 1, 1, 1 },
				{ 1, 0, 0 },
				{ 1, 1, 1 },
			};
			PlayerPosition playerStartPosition = new PlayerPosition(1, 1);
			bool isLabyrinthSolvable = GameUtilities.IsLabyrinthSolvable(labyrinthMap, playerStartPosition);
			Assert.IsTrue(isLabyrinthSolvable);
		}

		[TestMethod]
		public void TestIsLabyrinthSolvable_LabitynthWithManyExits_ReturnsTrue()
		{
			int[,] labyrinthMap = new int[,]
			{
				{ 0, 0, 0 },
				{ 0, 0, 0 },
				{ 0, 0, 0 },
			};
			PlayerPosition playerStartPosition = new PlayerPosition(1, 1);
			bool isLabyrinthSolvable = GameUtilities.IsLabyrinthSolvable(labyrinthMap, playerStartPosition);
			Assert.IsTrue(isLabyrinthSolvable);
		}
	}
}
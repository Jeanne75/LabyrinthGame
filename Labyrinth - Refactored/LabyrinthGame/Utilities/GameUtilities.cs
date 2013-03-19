using System;
using System.Linq;

namespace LabyrinthGame.Utilities
{
	public class GameUtilities
	{
		/// <summary>
		/// Verifies that given labyrinth map has at leas one possible solution
		/// </summary>
		/// <param name="labyrinthMap">Labyrinth map to check</param>
		/// <param name="playerPositionRow">Player's starting row position</param>
		/// <param name="playerPositionColumn">Player's starting column position</param>
		/// <returns>Returns true if there is at least one solution and false otherwise</returns>
		public static bool IsLabyrinthSolvable(
			int[,] labyrinthMap, PlayerPosition playerStartPosition)
		{
			CheckLabyrinthMapValidity(labyrinthMap);
			CheckPlayerPositionValidityInLabyrinthMap(labyrinthMap, playerStartPosition);

			int[,] labyrinthMapTestable = (int[,])labyrinthMap.Clone();
			int playerPositionRow = playerStartPosition.Row;
			int playerPositionColumn = playerStartPosition.Column;
			bool isSolvable =
				LabyrintExitExists(labyrinthMapTestable, playerPositionRow, playerPositionColumn);
			return isSolvable;
		}

		/// <summary>
		/// Checks if the given player position is inside the labyrinth map
		/// </summary>
		/// <param name="labyrinthMap">Labyrinth map</param>
		/// <param name="playerPosition">Player's position</param>
		/// <returns>Returns true if the position is inside the labyrinth. 
		/// False - otherwise</returns>
		public static bool IsPlayerPositionInLabyrinthRange(int[,] labyrinthMap,
			PlayerPosition playerPosition)
		{
			CheckLabyrinthMapValidity(labyrinthMap);

			int labyrinthLastRowPosition = labyrinthMap.GetLength(0) - 1;
			int labyrinthLastColumnPosition = labyrinthMap.GetLength(1) - 1;
			bool isPlayerRowPositionInRange = playerPosition.Row >= 0 &&
											  playerPosition.Row <=
											  labyrinthLastRowPosition;
			bool isPlayerColumnPositionInRange = playerPosition.Column >= 0 &&
												 playerPosition.Column <=
												 labyrinthLastColumnPosition;
			bool isPlayerPositionInLabyrinthRange = isPlayerRowPositionInRange &&
													isPlayerColumnPositionInRange;
			return isPlayerPositionInLabyrinthRange;
		}
  
		/// <summary>
		/// Checks if the given player position is at the labyrinth's border
		/// </summary>
		/// <param name="labyrinthMap">Labyrinth map</param>
		/// <param name="playerPosition">Player's position</param>
		/// <returns>Returns true if the position is at one of the the labyrinth's borders. 
		///False - otherwise</returns>
		public static bool IsPlayerPositionAtLabyrintBorder(int[,] labyrinthMap,
			PlayerPosition playerPosition)
		{
			CheckLabyrinthMapValidity(labyrinthMap);

			int labyrinthLastRowPosition = labyrinthMap.GetLength(0) - 1;
			int labyrinthLastColumnPosition = labyrinthMap.GetLength(1) - 1;
			bool isPlayerRowPositionAtBorder = playerPosition.Row == 0 ||
											   playerPosition.Row ==
											   labyrinthLastRowPosition;
			bool isPlayerColumnPositionAtBorder = playerPosition.Column == 0 ||
												  playerPosition.Column ==
												  labyrinthLastColumnPosition;
			bool isPlayerPositionAtLabyrintBorder = isPlayerRowPositionAtBorder ||
													isPlayerColumnPositionAtBorder;
			return isPlayerPositionAtLabyrintBorder;
		}
  
		private static void CheckLabyrinthMapValidity(int[,] labyrinthMap)
		{
			if (labyrinthMap == null)
			{
				throw new ArgumentNullException("Labyrint map should a valid object.");
			}
			int labyrinthRowCount = labyrinthMap.GetLength(0);
			int labyrinthColumnCount = labyrinthMap.GetLength(1);
			if (labyrinthRowCount <= 0 || labyrinthColumnCount <= 0)
			{
				throw new ArgumentOutOfRangeException(
					"The labyrinth should have positive number of rows and columns.");
			}
		}
  
		private static void CheckPlayerPositionValidityInLabyrinthMap(int[,] labyrinthMap,
			PlayerPosition playerStartPosition)
		{
			if (playerStartPosition == null)
			{
				throw new ArgumentNullException("Player position should a valid object.");
			}
			if (!IsPlayerPositionInLabyrinthRange(labyrinthMap, playerStartPosition))
			{
				throw new ArgumentOutOfRangeException(
					"Player position should be within the labyrinth range.");
			}
		}

		//The method works with integer positions instead of PlayerPosition object in order 
		//to prevent stack overflow in case of big labyrinth maps
		private static bool LabyrintExitExists(int[,] labyrinthMap,
			int playerPositionRow, int playerPositionColumn)
		{
			PlayerPosition playerPosition =
				new PlayerPosition(playerPositionRow, playerPositionColumn);
			bool isPlayerPositionInLabyrinthRange =
				IsPlayerPositionInLabyrinthRange(labyrinthMap, playerPosition);
			bool isLabyrinthCellFree = labyrinthMap[playerPositionColumn, playerPositionRow] == 0;

			if (!isPlayerPositionInLabyrinthRange || !isLabyrinthCellFree)
			{
				bool labyrintExitExists = false;
				return labyrintExitExists;
			}
			else
			{
				bool isPlayerPositionAtLabyrinthBorder =
					IsPlayerPositionAtLabyrintBorder(labyrinthMap, playerPosition);
				if (isPlayerPositionAtLabyrinthBorder)
				{
					BlockVisitedLabyrinthCell(labyrinthMap, playerPositionColumn, playerPositionRow);
					bool labyrintExitExists = true;
					return labyrintExitExists;
				}
				else
				{
					BlockVisitedLabyrinthCell(labyrinthMap, playerPositionColumn, playerPositionRow);
					bool labyrintExitExistsInLeft = LabyrintExitExists(
						labyrinthMap, playerPositionRow - 1, playerPositionColumn); 
					bool labyrintExitExistsInRight = LabyrintExitExists(
						labyrinthMap, playerPositionRow + 1, playerPositionColumn);
					bool labyrintExitExistsInUp = LabyrintExitExists(
						labyrinthMap, playerPositionRow, playerPositionColumn - 1);
					bool labyrintExitExistsInDown = LabyrintExitExists(
						labyrinthMap, playerPositionRow, playerPositionColumn + 1);
					bool labyrintExitExists = labyrintExitExistsInLeft ||
											  labyrintExitExistsInRight ||
											  labyrintExitExistsInUp ||
											  labyrintExitExistsInDown;
					return labyrintExitExists;
				}
			}
		}
  
		private static void BlockVisitedLabyrinthCell(int[,] labyrinthMap,
			int playerPositionColumn, int playerPositionRow)
		{
			labyrinthMap[playerPositionColumn, playerPositionRow] = 1;
		}
	}
}
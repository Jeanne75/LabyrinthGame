using System;

namespace LabyrinthGame.Game
{
	class Game
	{
		private const int LABYRINTH_ROWS_SIZE = 7;
		private const int LABYRINTH_COLUMNS_SIZE = 7;
		private const int PLAYER_INITIAL_POSITION_ROW = 3;
		private const int PLAYER_INITIAL_POSITION_COLUMN = 3;

		public static void Main()
		{
			Labyrinth game = new Labyrinth(LABYRINTH_ROWS_SIZE, LABYRINTH_COLUMNS_SIZE,
				PLAYER_INITIAL_POSITION_ROW, PLAYER_INITIAL_POSITION_COLUMN);
			game.Play();
		}
	}
}
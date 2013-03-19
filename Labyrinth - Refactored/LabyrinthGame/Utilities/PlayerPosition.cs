using System;

namespace LabyrinthGame.Utilities
{
	/// <summary>
	/// Stores player's position giving row and column
	/// </summary>
	public class PlayerPosition
	{
		public int Row { get; set; }
		public int Column { get; set; }

		/// <summary>
		/// Creates new player position holder
		/// </summary>
		/// <param name="row">Player's row position</param>
		/// <param name="column">Player's column position</param>
		public PlayerPosition(int row, int column)
		{
			this.Row = row;
			this.Column = column;
		}

		public void CopyTo(PlayerPosition destinationPlayerPosition)
		{
			if (destinationPlayerPosition == null)
			{
				throw new ArgumentNullException("The destination item must be a valid object");
			}

			destinationPlayerPosition.Row = this.Row;
			destinationPlayerPosition.Column = this.Column;
		}

		/// <summary>
		/// Moves she player postition with one column righ
		/// </summary>
		public void MoveRight()
		{
			this.Column++;
		}

		/// <summary>
		/// Moves she player postition with one column left
		/// </summary>
		public void MoveLeft()
		{
			this.Column--;
		}

		/// <summary>
		/// Moves she player postition with one row up
		/// </summary>
		public void MoveUp()
		{
			this.Row--;
		}

		/// <summary>
		/// Moves she player postition with one row dows
		/// </summary>
		public void MoveDown()
		{
			this.Row++;
		}
	}
}
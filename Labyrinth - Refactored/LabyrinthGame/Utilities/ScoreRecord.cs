using System;

namespace LabyrinthGame.Utilities
{
	/// <summary>
	/// Score record class keeping player name and acheived result
	/// </summary>
	public class ScoreRecord : IComparable
	{
		public string PlayerName { get; set; }
		public int Score { get; set; }

		/// <summary>
		/// Creates new record storing player name and reusult
		/// </summary>
		/// <param name="playerName">The name of the player achieved the score</param>
		/// <param name="score">Achieved score</param>
		public ScoreRecord(string playerName, int score)
		{
			if (playerName == string.Empty)
			{
				throw new ArgumentNullException("Player name could not be empty");
			}

			this.PlayerName = playerName;
			this.Score = score;
		}

		/// <summary>
		/// Compares two records. 
		/// </summary>
		/// <param name="comparableObject">Object to compare to</param>
		/// <returns>Result of the comparison</returns>
		public int CompareTo(object comparableObject)
		{
			if (comparableObject == null)
			{
				throw new ArgumentNullException("Comparable item must be a valid object");
			}
			ScoreRecord comparableScoreRecord;
			try
			{
				comparableScoreRecord = (ScoreRecord)comparableObject;
			}
			catch (InvalidCastException innerException)
			{
				throw new ArgumentException("Comparable object is not a ScoreRecord instance", innerException);
			}

			int comparableScore = comparableScoreRecord.Score;
			int compareResult = this.Score.CompareTo(comparableScore);
			return compareResult;
		}
	}
}
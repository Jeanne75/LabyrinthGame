using System;
using System.Collections.Generic;

namespace LabyrinthGame.Utilities
{
	/// <summary>
	/// Holds set of scores sorted in ascending order
	/// </summary>
	public class Scoreboard
	{
		private readonly int maximumScoreboeardSize;
		private readonly GameResourcesCollection resourcesList = new GameResourcesCollection();
		private List<ScoreRecord> scoreRecordsList = new List<ScoreRecord>();
  
		/// <summary>
		/// Creates new scoreboard giving information of its size
		/// </summary>
		/// <param name="storedRecordsNumber">Maximum number of stored records.</param>
		public Scoreboard(int maximumScoreRecordsCount)
		{
			if (maximumScoreRecordsCount <= 0)
			{
				throw new ArgumentOutOfRangeException(
					"The number of sored records should be a positive number.");
			}

			this.maximumScoreboeardSize = maximumScoreRecordsCount;
		}
  
		/// <summary>
		/// Adds new score in the list only in case it is better that the already scored ones
		/// or there is free slot
		/// </summary>
		/// <param name="name">Player's name</param>
		/// <param name="score">Achieved result</param>
		public void AddScore(string name, int score)
		{
			ScoreRecord scoreRecord = new ScoreRecord(name, score);
			this.scoreRecordsList.Add(scoreRecord);
			this.scoreRecordsList.Sort();
			if (this.scoreRecordsList.Count > this.maximumScoreboeardSize)
			{
				this.scoreRecordsList = this.scoreRecordsList.GetRange(0,
					this.maximumScoreboeardSize);
			}
		}
  
		/// <summary>
		/// Checks if a score result could be included in the scoreboard
		/// </summary>
		/// <param name="score">Score result to test</param>
		/// <returns>Returns true if the score could be stored and false otherwise</returns>
		public bool IsHightScore(int score)
		{
			int lowestScore = this.GetLowestScore(this.scoreRecordsList);
			bool isHighScore = (score < lowestScore ||
								this.scoreRecordsList.Count < this.maximumScoreboeardSize);
			return isHighScore;
		}
  
		/// <summary>
		/// Prints the scoreboard on the screen
		/// </summary>
		public void PrintScoreboard()
		{
			if (this.scoreRecordsList.Count != 0)
			{
				string scoreboardPrintMessage =
					this.resourcesList.GetResourceValue("ScoreboardMessage");
				Console.WriteLine(scoreboardPrintMessage);
				foreach (ScoreRecord scoreRecordItem in this.scoreRecordsList)
				{
					int scoreRecordPosition = this.scoreRecordsList.IndexOf(scoreRecordItem) + 1;
					string scoreboardPrintDivider = this.resourcesList.GetResourceValue("ScoreboardPrintDivider");
					string movesPrintMessage = this.resourcesList.GetResourceValue("MovesMessage"); 
					Console.WriteLine("{0}. {1} {2} {3} {4}.",
						scoreRecordPosition, scoreRecordItem.PlayerName,
						scoreboardPrintDivider, scoreRecordItem.Score, movesPrintMessage);
				}
			}
			else
			{
				string emptyScoreboardPrintMessage =
					this.resourcesList.GetResourceValue("EmptyScoreboardMessage");
				Console.WriteLine(emptyScoreboardPrintMessage);
			}
		}

		private int GetLowestScore(List<ScoreRecord> scoreRecordsList)
		{
			int storedRecordsCount = scoreRecordsList.Count;
			if (storedRecordsCount > 0)
			{
				int lowestScoreIndex = scoreRecordsList.Count - 1;			
				ScoreRecord lowestScoreRecord = scoreRecordsList[lowestScoreIndex];				
				return lowestScoreRecord.Score;
			}
			else
			{
				return 0;
			}
		}
	}
}
using System;
using System.Collections.Generic;
using LabyrinthGame.GameExceptions;
using LabyrinthGame.Utilities;

namespace LabyrinthGame.Game
{
	public class Labyrinth
	{
		private const int SCOREBOARD_STORED_RECORDS_SIZE = 5; 
		
		private delegate void UserCommandExecutorDelegate();

		private readonly PlayerPosition playerInitialPosition;
		private readonly GameResourcesCollection resourcesList = new GameResourcesCollection();
		private readonly Dictionary<string, UserCommand> userActionsMap =
			new Dictionary<string, UserCommand>();
		private readonly Dictionary<UserCommand, UserCommandExecutorDelegate> userActionsList =
			new Dictionary<UserCommand, UserCommandExecutorDelegate>();
		
		private int[,] labyrinthMap;
		private bool isGameActive = true;
		private int currentMovementsCount = 0;
		private PlayerPosition playerCurrentPosition;		
		private Scoreboard scoreboard = new Scoreboard(SCOREBOARD_STORED_RECORDS_SIZE);

		private enum UserCommand
		{
			MoveRight,
			MoveLeft,
			MoveUp,
			MoveDown,
			StartNewGame,
			ExitGame,
			ShowTopScores
		};

		/// <summary>
		/// Initialises the labyrinth game 
		/// setting information about the board size and the initial player position
		/// </summary>
		/// <param name="rows">Number of labyrinth rows</param>
		/// <param name="columns">Number of labyrinth columns</param>
		/// <param name="playerInitialPositionRow">Initial row position of the player</param>
		/// <param name="playerInitialPositionColumn">Initial column position of the player</param>
		public Labyrinth(int rows, int columns,
			int playerInitialPositionRow, int playerInitialPositionColumn)
		{
			this.VerifyLabyrinthSizeParameter(rows, columns);
			this.VerifyPlayerInitialPositionParameters(rows, columns,
				playerInitialPositionRow, playerInitialPositionColumn);

			this.labyrinthMap = new int[rows, columns];
			this.playerCurrentPosition =
				new PlayerPosition(playerInitialPositionRow, playerInitialPositionColumn);
			this.playerInitialPosition =
				new PlayerPosition(playerInitialPositionRow, playerInitialPositionColumn);
			this.InitializeUserCommadsParser();
			this.InitializeUserActionList();
			this.StartNewGame();
		}
  
		/// <summary>
		///Plays the game using the user's commands 
		/// </summary>
		public void Play()
		{
			do
			{
				PlayerPosition playerPreviousPosition = new PlayerPosition(
					this.playerCurrentPosition.Row, this.playerCurrentPosition.Column);
				string enterMoveMessage = this.resourcesList.GetResourceValue("EnterMoveMessage");
				Console.Write(enterMoveMessage);
				string userInput = Console.ReadLine();
				try
				{
					UserCommand userCommand = this.ParseUserCommand(userInput);
					this.ExecuteUserCommand(userCommand);
				}
				catch (InvalidCommandException)
				{
					this.PrintInvalidCommandMessage();
					continue;
				}
				if (this.IsPlayerPositionChanged(playerPreviousPosition))
				{
					this.UpdateLabyrinthAfterMovement();
				}
			}
			while (this.isGameActive);
		}

		private void VerifyPlayerInitialPositionParameters(int rows, int columns,
			int playerInitialPositionRow, int playerInitialPositionColumn)
		{
			if (playerInitialPositionRow < 0 || rows <= playerInitialPositionRow)
			{
				string exceptionMessage = string.Format("Player initial row position" +
														" should be within the labyrinth borders" +
														" {0} - {1}", 0, rows - 1);
				throw new ArgumentOutOfRangeException(exceptionMessage);
			}
			if (playerInitialPositionColumn < 0 || columns <= playerInitialPositionColumn)
			{
				string exceptionMessage = string.Format("Player initial column position" +
														" should be within the labyrinth borders" +
														" {0} - {1}", 0, rows - 1);
				throw new ArgumentOutOfRangeException(exceptionMessage);
			}
		}

		private void VerifyLabyrinthSizeParameter(int rows, int columns)
		{
			if (rows < 0)
			{
				throw new ArgumentOutOfRangeException(
					"Number of labyririth rows should be positive");
			}
			if (columns < 0)
			{
				throw new ArgumentOutOfRangeException(
					"Number of labyririth rows should be positive");
			}
		}
  
		private void StartNewGame()
		{
			this.isGameActive = true;
			this.currentMovementsCount = 0;
			this.playerInitialPosition.CopyTo(this.playerCurrentPosition);
			string welcomeMessage = this.resourcesList.GetResourceValue("WelcomeMessage");
			Console.WriteLine(welcomeMessage);
			this.InitializeLabyrinthMap();
			this.PrintLabyrinth();
		}
  
		private void InitializeLabyrinthMap()
		{
			bool isLabyrinthSolvable = false;
			do
			{
				int rowsCount = this.labyrinthMap.GetLength(0);
				int columnsCount = this.labyrinthMap.GetLength(0);
				Random labyrinthCellStatus = new Random();
				for (int row = 0; row < rowsCount; row++)
				{
					for (int column = 0; column < columnsCount; column++)
					{
						this.labyrinthMap[row, column] = labyrinthCellStatus.Next(2);
					}
				}
				this.labyrinthMap[this.playerInitialPosition.Row,
					this.playerInitialPosition.Column] = 0;
				isLabyrinthSolvable = GameUtilities.IsLabyrinthSolvable(
					this.labyrinthMap, this.playerInitialPosition);
			}
			while (!isLabyrinthSolvable);
		}
  
		private void PrintLabyrinth()
		{
			int rowsCount = this.labyrinthMap.GetLength(0);			
			for (int row = 0; row < rowsCount; row++)
			{
				int columnsCount = this.labyrinthMap.GetLength(0);
				for (int column = 0; column < columnsCount; column++)
				{
					string printCharacter;
					if (row == this.playerCurrentPosition.Row &&
						column == this.playerCurrentPosition.Column)
					{
						printCharacter = this.resourcesList.GetResourceValue("PlayerPositionSign");
					}
					else
					{
						int labyrinthCell = this.labyrinthMap[row, column];
						printCharacter = this.GetPrintCharacter(labyrinthCell);
					}
					Console.Write(printCharacter);
				}
				Console.WriteLine();
			}
		}
  
		private string GetPrintCharacter(int labyrinthCellValue)
		{
			if (labyrinthCellValue == 0)
			{
				string labyrinthFreeCellSign =
					this.resourcesList.GetResourceValue("LabyrinthFreeCellSign");
				return labyrinthFreeCellSign; 
			}
			else
			{
				string labyrinthBlockedCellSign =
					this.resourcesList.GetResourceValue("LabyrinthBlockedCellSign");
				return labyrinthBlockedCellSign;
			}
		}
  
		private UserCommand ParseUserCommand(string userInput)
		{ 
			bool isUserCommandValid = this.userActionsMap.ContainsKey(userInput);
			if (isUserCommandValid)
			{
				UserCommand userCommand = this.userActionsMap[userInput];
				return userCommand;
			}
			else
			{
				string invalidCommandMessage =
					string.Format("The commant '{0}' is invalid.", userInput);
				throw new InvalidCommandException(invalidCommandMessage);
			}
		}
  
		private void InitializeUserCommadsParser()
		{
			this.userActionsMap.Add("R", UserCommand.MoveRight);
			this.userActionsMap.Add("r", UserCommand.MoveRight);
			this.userActionsMap.Add("L", UserCommand.MoveLeft);
			this.userActionsMap.Add("l", UserCommand.MoveLeft);
			this.userActionsMap.Add("U", UserCommand.MoveUp);
			this.userActionsMap.Add("u", UserCommand.MoveUp);
			this.userActionsMap.Add("D", UserCommand.MoveDown);
			this.userActionsMap.Add("d", UserCommand.MoveDown);
			this.userActionsMap.Add("exit", UserCommand.ExitGame);
			this.userActionsMap.Add("restart", UserCommand.StartNewGame);
			this.userActionsMap.Add("top", UserCommand.ShowTopScores);
		}
  
		private void ExecuteUserCommand(UserCommand command)
		{
			UserCommandExecutorDelegate userCommandExecutor;
			if (this.userActionsList.TryGetValue(command, out userCommandExecutor))
			{
				userCommandExecutor();
			}
			else
			{
				string invalidCommandMessage =
					string.Format("The commant '{0}' is invalid.", command.ToString());
				throw new InvalidCommandException(invalidCommandMessage);
			}
		}
  
		private void InitializeUserActionList()
		{
			this.userActionsList.Add(UserCommand.MoveRight, this.MoveRight);
			this.userActionsList.Add(UserCommand.MoveLeft, this.MoveLeft);
			this.userActionsList.Add(UserCommand.MoveUp, this.MoveUp);
			this.userActionsList.Add(UserCommand.MoveDown, this.MoveDown);
			this.userActionsList.Add(UserCommand.StartNewGame, this.StartNewGame);
			this.userActionsList.Add(UserCommand.ShowTopScores, this.PrintScoreboard);
			this.userActionsList.Add(UserCommand.ExitGame, this.ExitGame);
		}
  
		private bool IsValidMovement(PlayerPosition playerNextPosition)
		{
			bool isPlayerPositionInRange =
				GameUtilities.IsPlayerPositionInLabyrinthRange(
					this.labyrinthMap, playerNextPosition);
			bool isValidMove = (isPlayerPositionInRange &&
								this.labyrinthMap[playerNextPosition.Row,
									playerNextPosition.Column] == 0);
			return isValidMove;
		}
  
		private bool IsPlayerPositionChanged(PlayerPosition playerPreviousPosition)
		{
			bool isPlayerPositionChanged = this.playerCurrentPosition.Row !=
										   playerPreviousPosition.Row ||
										   this.playerCurrentPosition.Column !=
										   playerPreviousPosition.Column;
			return isPlayerPositionChanged;
		}
  
		private bool IsGameWon()
		{
			bool isGameWon =
				GameUtilities.IsPlayerPositionAtLabyrintBorder(this.labyrinthMap,
					this.playerCurrentPosition);
			return isGameWon;
		}
  
		private void MoveRight()
		{
			PlayerPosition playerNextPosition = new PlayerPosition(0, 0);
			this.playerCurrentPosition.CopyTo(playerNextPosition);
			playerNextPosition.MoveRight();
			if (this.IsValidMovement(playerNextPosition))
			{
				this.playerCurrentPosition.MoveRight();
			}
			else
			{
				throw new InvalidCommandException("Invalid movement");
			}
		}
  
		private void MoveLeft()
		{
			PlayerPosition playerNextPosition = new PlayerPosition(0, 0);
			this.playerCurrentPosition.CopyTo(playerNextPosition);
			playerNextPosition.MoveLeft();
			if (this.IsValidMovement(playerNextPosition))
			{
				this.playerCurrentPosition.MoveLeft();
			}
			else
			{
				throw new InvalidCommandException("Invalid movement");
			}
		}
  
		private void MoveUp()
		{
			PlayerPosition playerNextPosition = new PlayerPosition(0, 0);
			this.playerCurrentPosition.CopyTo(playerNextPosition);
			playerNextPosition.MoveUp();
			if (this.IsValidMovement(playerNextPosition))
			{
				this.playerCurrentPosition.MoveUp();
			}
			else
			{
				throw new InvalidCommandException("Invalid movement");
			}
		}
  
		private void MoveDown()
		{
			PlayerPosition playerNextPosition = new PlayerPosition(0, 0);
			this.playerCurrentPosition.CopyTo(playerNextPosition);
			playerNextPosition.MoveDown();
			if (this.IsValidMovement(playerNextPosition))
			{
				this.playerCurrentPosition.MoveDown();
			}
			else
			{
				throw new InvalidCommandException("Invalid movement");
			}
		}
  
		private void UpdateLabyrinthAfterMovement()
		{
			this.currentMovementsCount++;
			if (!this.IsGameWon())
			{
				this.PrintLabyrinth();
			}
			else
			{
				this.CompleteWonGame();
				this.StartNewGame();
			}
		}

		private void PrintScoreboard()
		{
			this.scoreboard.PrintScoreboard();
		}
  
		private void PrintInvalidCommandMessage()
		{
			string invalidCommandMessage =
				this.resourcesList.GetResourceValue("InvalidCommandMessage");
			Console.WriteLine(invalidCommandMessage);
		}

		private void ExitGame()
		{
			string goodByeMessage = this.resourcesList.GetResourceValue("GoodByeMessage");
			Console.WriteLine(goodByeMessage);
			this.isGameActive = false;
		}

		private void CompleteWonGame()
		{
			string escapedInXMovesMessage =
				this.resourcesList.GetResourceValue("EscapedInXMovesMessage");
			string movesMessage = this.resourcesList.GetResourceValue("MovesMessage");			
			Console.WriteLine("{0} {1} {2}.",
				escapedInXMovesMessage, this.currentMovementsCount, movesMessage);
			if (this.scoreboard.IsHightScore(this.currentMovementsCount))
			{
				string recordScoreMessage =
					this.resourcesList.GetResourceValue("RecordScoreMessage");
				Console.Write("{0} ", recordScoreMessage);
				string playerName = Console.ReadLine();
				this.scoreboard.AddScore(playerName, this.currentMovementsCount);
				this.PrintScoreboard();
			}
		}
	}
}
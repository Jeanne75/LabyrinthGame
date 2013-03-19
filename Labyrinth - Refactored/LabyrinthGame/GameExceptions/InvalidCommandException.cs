using System;
using System.Linq;

namespace LabyrinthGame.GameExceptions
{
	/// <summary>
	/// Exception thrown in case of invalid user command
	/// </summary>
	public class InvalidCommandException : Exception
	{
		public InvalidCommandException()
		{ 
		}

		public InvalidCommandException(string message) : base(message)
		{
		}

		public InvalidCommandException(string message,
			Exception innerException) : base(message, innerException)
		{
		}
	}
}
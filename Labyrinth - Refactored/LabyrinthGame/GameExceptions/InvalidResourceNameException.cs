using System;
using System.Linq;

namespace LabyrinthGame.GameExceptions
{
	/// <summary>
	/// Exception thrown in case of invalid resource name
	/// </summary>
	public class InvalidResourceNameException : System.Exception
	{
		public InvalidResourceNameException()
		{
		}

		public InvalidResourceNameException(string message) : base(message)
		{
		}

		public InvalidResourceNameException(string message,
			Exception innerException) : base(message, innerException)
		{
		}
	}
}
using System;
using System.Linq;
using System.IO;

namespace LabyrinthGameTests.Utilities
{
	class TestUtilities
	{
		public static void RedirectConsoleStram(string consoleInput, out StringWriter consoleWriter)
		{
			StringReader consoleReader = new StringReader(consoleInput);
			consoleWriter = new StringWriter();
			Console.SetIn(consoleReader);
			Console.SetOut(consoleWriter);
		}

		public static string GetConsoleOutput(StringWriter consoleWriter)
		{
			string consoleOuput = consoleWriter.ToString();
			return consoleOuput;
		}
	}
}
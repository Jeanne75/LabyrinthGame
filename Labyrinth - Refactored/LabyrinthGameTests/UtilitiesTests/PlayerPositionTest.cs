using LabyrinthGame.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LabyrinthGameTests.UtilitiesTests
{
	[TestClass]
	public class PlayerPositionTest
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void TestCopyTo_DestinationObjectNotValid_TrownException()
		{
			int row = 0;
			int column = 0; 
			PlayerPosition target = new PlayerPosition(row, column); 
			PlayerPosition destinationPlayerPosition = null;
			target.CopyTo(destinationPlayerPosition);
		}
	}
}
using LabyrinthGame.GameExceptions;
using LabyrinthGame.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LabyrinthGameTests.UtilitiesTests
{
	[TestClass]
	public class GameResourcesCollectionTest
	{
		[TestMethod]
		[ExpectedException(typeof(InvalidResourceNameException))]
		public void TestGetResourceValue_ReadValidResource_ThrownException()
		{
			GameResourcesCollection target = new GameResourcesCollection();
			string resourceName = "WrongResourceName";
			target.GetResourceValue(resourceName);
		}

		[TestMethod]
		public void TestGetResourceValue_ReadValidResource_ResourceValueReturned()
		{
			GameResourcesCollection target = new GameResourcesCollection(); 
			string resourceName = "EmptyScoreboardMessage";
			string expected = "The scoreboard is empty.";
			string actual = target.GetResourceValue(resourceName);
			Assert.AreEqual(expected, actual);
		}
	}
}
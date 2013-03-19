using System;
using System.Linq;
using System.Resources;
using LabyrinthGame.GameExceptions;

namespace LabyrinthGame.Utilities
{
	public class GameResourcesCollection
	{
		private readonly ResourceManager resourcesList;

		/// <summary>
		/// Creates new set or resources
		/// </summary>
		/// <param name="resourceFileName">File name storing the resouces</param>
		public GameResourcesCollection()
		{
			this.resourcesList = LabyrinthGame.Resources.GameResources.ResourceManager;
		}

		/// <summary>
		/// Reads the resource value from the file
		/// </summary>
		/// <param name="resourceName">Resource to read</param>
		/// <returns>Resource value if found</returns>
		public string GetResourceValue(string resourceName)
		{
			string resourceValue = this.resourcesList.GetString(resourceName);
			if (resourceValue == null)
			{
				string message = string.Format("{0} is not a valid resource name", resourceName);
				throw new InvalidResourceNameException(message);
			}
			return resourceValue;
		}
	}
}
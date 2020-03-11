using System.IO;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ProgressManager manages the save and load systems to keep track of the player's progression
/// </summary>
public static class ProgressManager
{
	#region PUBLIC_CLASSES 

	/// <summary>
	/// Location holds the required information of a location
	/// </summary>
	[System.Serializable]
	public class Location
	{
		public string locationName = "";
		[Tooltip("Keeps track if information is unlocked")]
		public bool info = false;
		[Tooltip("Keeps track if badge is unlocked (can only be true if info is true)")]
		public bool badge = false;
	}

	/// <summary>
	/// SaveData holds all the data on locations (needs to be set manually)
	/// </summary>
	[System.Serializable]
	public class SaveData
	{
		public List<Location> locations = new List<Location>();
	}

	#endregion



	#region PRIVATE_MEMBERS

	private static string fileName = "Save";
	private static string fileExtension = ".json";
	private static string filePath = "";
	private static string folderName = "SaveData";
	private static string directory = "";

	#endregion



	#region PUBLIC_METHODS

	/// <summary>
	/// Call method to save the current progress
	/// </summary>
	/// <param name="data"></param>
	/// <param name="index"></param>
	public static void SaveGame(SaveData data, int index)
	{
		string json = JsonUtility.ToJson(data);

		SaveJSON(json, index);
	}

	/// <summary>
	/// Call method to load the saved progress
	/// </summary>
	/// <param name="index"></param>
	/// <returns></returns>
	public static string LoadGame(int index)
	{
		return LoadJSON(filePath, index);
	}

	#endregion



	#region PRIVATE_METHODS

	/// <summary>
	/// Saves a JSON file to the device
	/// </summary>
	/// <param name="json"></param>
	/// <param name="index"></param>
	private static void SaveJSON(string json, int index)
	{
		directory = Application.persistentDataPath + "/" + folderName;

		if (!Directory.Exists(directory))
		{
			Directory.CreateDirectory(directory);
		}

		filePath = directory + "/" + fileName + "_" + index + fileExtension;

		File.WriteAllText(filePath, json);

		Debug.Log("JSON SAVED TO " + filePath);
	}


	/// <summary>
	/// Loads and return a JSON file from given path and index
	/// </summary>
	/// <param name="path"></param>
	/// <param name="index"></param>
	/// <returns></returns>
	private static string LoadJSON(string path, int index)
	{
		// Sets path with given index
		path += "_" + index;

		if (!File.Exists(path)) 
		{
			Debug.LogError("PATH IS INACCESSIBLE");
			return "";
		}

		Debug.Log("JSON LOADED FROM " + path);

		return File.ReadAllText(path);
	}

	#endregion
}

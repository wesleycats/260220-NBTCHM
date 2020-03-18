using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
	public ProgressManager.SaveData _SaveData { get { return saveData; } }

	[SerializeField] private ProgressManager.SaveData saveData = new ProgressManager.SaveData();
}

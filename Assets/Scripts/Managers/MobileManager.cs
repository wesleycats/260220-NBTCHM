using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MobileManager is resposible for all navigations and animations of the mobile menu
/// </summary>
public class MobileManager : MonoBehaviour
{
	#region PUBLIC_STRUCTS

	[System.Serializable]
	public struct Anim
	{
		public string AnimationName;
		public List<string> OpenDirections;
		public List<string> CloseDirections;
	}

	#endregion



	#region PUBLIC_MEMBERS

	public string _LastState { get { return usedStates[usedStates.Count - 1]; } }
	public string _PreviousState { get { return usedStates[usedStates.Count - 2]; } }

	public GameObject _LastMenu { get { return usedMenus[usedMenus.Count - 1]; } }
	public GameObject _PreviousMenu { get { return usedMenus[usedMenus.Count - 2]; } }

	public Animator MobileAnimator = null;
	public DataManager DataManager = null;
	public GameObject ControlPad = null;

	#endregion



	#region PRIVATE_MEMBERS

	[SerializeField] private string stateSeperator = "_";
	[SerializeField] private string hideIndicator = "Hide_";
	[SerializeField] private List<Anim> animations = new List<Anim>();

	[SerializeField] private string currentState = "";
	[SerializeField] private List<string> usedStates = new List<string>();

	[SerializeField] private GameObject currentMenu = null;
	[SerializeField] private List<GameObject> usedMenus = new List<GameObject>();


	private bool animIsPlaying = false;

	#endregion



	#region PUBLIC_METHODS

	public void PlayAnim(string animName)
	{
		if (!MobileAnimator.GetBool(GetTargetState(animName)))
		{
			Debug.Log(GetTargetState(animName) + " PATH NOT OPEN");
			return;
		}

		if (animIsPlaying)
		{
			Debug.Log("AN ANIMATION IS ALREADY PLAYING");
			return;
		}

		if (animName.Contains(hideIndicator))
		{
			ControlPad.SetActive(true);
			usedStates.Clear();
		}
		else
		{
			ControlPad.SetActive(false);
			ChangeState(animName);
		}

		foreach (Anim a in animations)
		{
			if (a.AnimationName == animName)
			{
				animIsPlaying = true;
				MobileAnimator.Play(animName);
				SetDirections(a.OpenDirections, true);
				SetDirections(a.CloseDirections, false);
			}
		}
	}

	
	public void ChangeMenu(GameObject newMenu)
	{
		if (!newMenu)
			return;

		if (!usedMenus.Contains(newMenu))
			usedMenus.Add(newMenu);
		else
			usedMenus.Remove(currentMenu);

		newMenu.SetActive(true);
		currentMenu = newMenu;

		if (usedMenus.Count > 0)
		{
			while (currentState != _LastState)
				RemoveLastMenu();
		}
	}


	public void ChangeState(string stateName)
	{
		if (!usedStates.Contains(stateName))
			usedStates.Add(stateName);
		else
			usedStates.Remove(currentState);

		currentState = GetTargetState(stateName);

		if (usedStates.Count > 0)
		{
			while (currentState != _LastState)
				RemoveLastState();
		}
	}


	/// <summary>
	/// Pauses the game while mobile is open
	/// </summary>
	public void Pause()
	{
		// Player controls disabled
	}


	public void Hide()
	{
		HideMenus();

		PlayAnim(hideIndicator + stateSeperator + currentState);
	}


	public void HideMenus()
	{
		foreach (GameObject menu in usedMenus)
			menu.SetActive(false);

		usedMenus.Clear();
	}


	/// <summary>
	/// Returns to the previous state of the mobile
	/// </summary>
	public void Return()
	{
		if (usedMenus.Count == 0)
		{
			PlayAnim(GetPreviousState(_PreviousState) + "_" + currentState);
		}
		if (usedMenus.Count > 1)
		{
			ChangeMenu(_PreviousMenu);
		}
		else
		{
			HideMenus();
		}
	}


	public void QuitGame()
	{
		StartCoroutine(Quit());
	}


	/// <summary>
	/// Removes last state in usedStates list
	/// </summary>
	public void RemoveLastState()
	{
		usedStates.Remove(_LastState);
	}


	/// <summary>
	/// Removes last menu in usedAnimations list
	/// </summary>
	public void RemoveLastMenu()
	{
		usedMenus.Remove(_LastMenu);
	}


	public void Event_AnimFinished()
	{
		animIsPlaying = false;
	}

	#endregion



	#region PRIVATE_METHODS

	private IEnumerator Quit()
	{
		ProgressManager.SaveData data = DataManager._SaveData;

		ProgressManager.Save(data);

		yield return new WaitUntil(() => ProgressManager.isSaveDataSet(data));

		Debug.Log("QUIT GAME");
  
		Application.Quit();
	}


	private void SetDirections(List<string> dirs, bool set)
	{
		foreach(string d in dirs)
		{
			MobileAnimator.SetBool(d, set);
		}
	}

	private string GetTargetState(string animName)
	{
		if (animName.Contains(stateSeperator))
			return animName.Substring(0, animName.IndexOf('_'));
		else
			return animName;
	}
	

	private string GetPreviousState(string animName)
	{
		if (animName.Contains(stateSeperator))
			return animName.Substring(animName.IndexOf('_') + 1, animName.Length);
		else
			return animName;
	}

	#endregion
}

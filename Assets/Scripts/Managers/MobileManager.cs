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

	public Animator MobileAnimator = null;
	public DataManager DataManager = null;

	#endregion



	#region PRIVATE_MEMBERS

	[SerializeField] private string stateSeperator = "_";
	[SerializeField] private string hideIndicator = "Hide_";
	[SerializeField] private List<Anim> animations = new List<Anim>();

	[SerializeField] private string currentState = "";
	[SerializeField] private List<string> usedStates = new List<string>();

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
			usedStates.Clear();
		}
		else
		{
			if (!usedStates.Contains(animName))
				usedStates.Add(animName);
			else
				usedStates.Remove(currentState);

			currentState = GetTargetState(animName);

			while (currentState != _LastState)
				RemoveLastState();
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


	/// <summary>
	/// Pauses the game while mobile is open
	/// </summary>
	public void Pause()
	{
		// Player controls disabled
	}


	public void Hide()
	{
		PlayAnim(hideIndicator + stateSeperator + currentState);
	}


	/// <summary>
	/// Returns to the previous state of the mobile
	/// </summary>
	public void Return()
	{
		PlayAnim(GetPreviousState(_PreviousState) + "_" + currentState);
	}


	public void QuitGame()
	{
		StartCoroutine(Quit());
	}


	/// <summary>
	/// Removes last menu in usedAnimations list
	/// </summary>
	public void RemoveLastState()
	{
		usedStates.Remove(_LastState);
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

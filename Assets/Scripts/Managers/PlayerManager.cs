using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	#region PUBLIC_MEMBERS

	public ButtonManager ButtonManager = null;
    public PlayerMovement PlayerMovement = null;
	public SpriteRenderer PlayerSprite = null;
	public Animator Animator = null;

	#endregion



	#region MONOBEHAVHIOUR_METHODS

	private void Update()
	{
		CheckPlayerInput();
	}

	#endregion



	#region PRIVATE_METHODS

	private void CheckPlayerInput()
	{
		if (ButtonManager.ButtonHoldDown)
		{
			PlayerMovement.Direction = ButtonManager._Direction;
			SetPlayerDirection(PlayerMovement.Direction);
			Animator.SetBool("isWalking", true);
		}
		else
		{
			PlayerMovement.Direction = null;
			Animator.SetBool("isWalking", false);
		}
	}

	private void SetPlayerDirection(string direction)
	{
		if (direction == "Right")
		{
			PlayerSprite.flipX = false;
		}
		else
		{
			PlayerSprite.flipX = true;
		}
	}

	#endregion
}

using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public bool ButtonHoldDown = false;
    
	private string direction;

    public string _Direction
    {
        get { return direction; }
        set { direction = value; }
    }

    public void HoldingButton(string directionParameter)
    {
        ButtonHoldDown = true;
        direction = directionParameter;
    }

    public void ReleasingButton()
    {
        ButtonHoldDown = false;
    }

}

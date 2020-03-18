using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    private string m_direction;

    public bool m_buttonHoldDown = false;

    public string direction
    {
        get { return m_direction; }
        set { m_direction = value; }
    }

    public void HoldingButton(string directionParameter)
    {
        m_buttonHoldDown = true;
        m_direction = directionParameter;
    }

    public void ReleasingButton()
    {
        m_buttonHoldDown = false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public string direction;

    [SerializeField]
    private float m_movementMultiplier = 3;

    private void FixedUpdate()
    {
        switch(direction)
        {
            case "Left":
                this.transform.Translate((Vector3.left * m_movementMultiplier) * Time.deltaTime);
                break;
            case "Right":
                this.transform.Translate((Vector3.right * m_movementMultiplier) * Time.deltaTime);
                break;
            default:
                break;
        }
    }

}

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public string Direction;

    [SerializeField] private float movementMultiplier = 3;

    private void FixedUpdate()
    {
        switch(Direction)
        {
            case "Left":
                this.transform.Translate((Vector3.left * movementMultiplier) * Time.deltaTime);
                break;
            case "Right":
                this.transform.Translate((Vector3.right * movementMultiplier) * Time.deltaTime);
                break;
            default:
                break;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private ButtonManager buttonManager;
    private PlayerMovement playerMovement;

    private void Start()
    {
        buttonManager = GameObject.Find("Button Manager").GetComponent<ButtonManager>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(buttonManager.m_buttonHoldDown)
        {
            playerMovement.direction = buttonManager.direction;
        }
        else
        {
            playerMovement.direction = null;
        }
    }
}

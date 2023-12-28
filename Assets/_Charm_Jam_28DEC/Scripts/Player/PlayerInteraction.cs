using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteraction : MonoBehaviour
{
    private void OnEnable()
    {
        InputManager.OnInteract += InputManager_OnInteract;
    }

    private void InputManager_OnInteract(object sender, EventArgs e)
    {
        Debug.Log(gameObject.name);
    }

    private void OnDisable()
    {
        InputManager.OnInteract -= InputManager_OnInteract;
    }
}

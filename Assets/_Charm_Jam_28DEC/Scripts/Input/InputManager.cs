using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static event EventHandler<Vector2> OnMove;

    public static event EventHandler OnInteract;

    private const string YAXIS = "Vertical";
    private const string XAXIS = "Horizontal";

    private Vector2 movementVector;

    private void Update()
    {
        MovementInput();
        InteractionInput();
    }

    private void InteractionInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteract?.Invoke(this, EventArgs.Empty);
        }
    }

    private void MovementInput()
    {
        movementVector = new Vector2(Input.GetAxisRaw(XAXIS), Input.GetAxisRaw(YAXIS)).normalized;
        if (movementVector != Vector2.zero)
        {
            OnMove?.Invoke(this, movementVector);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10;

    private void OnEnable()
    {
        InputManager.OnMove += InputManager_OnMove;
    }

    private void InputManager_OnMove(object sender, Vector2 inputData)
    {
        transform.position += movementSpeed * Time.deltaTime * new Vector3(inputData.x, inputData.y);
    }

    private void OnDisable()
    {
        InputManager.OnMove -= InputManager_OnMove;
    }
}

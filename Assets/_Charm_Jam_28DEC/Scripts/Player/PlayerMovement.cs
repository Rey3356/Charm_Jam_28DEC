using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10;
    private Transform PlayerTR;
    
    private void OnEnable()
    {
        InputManager.OnMove += InputManager_OnMove;
        PlayerTR = gameObject.transform;
    }

    private void InputManager_OnMove(object sender, Vector2 inputData)
    {
        
        if (inputData.x < 0)
        {
            PlayerTR.localScale = new Vector3(-1 * Mathf.Abs(PlayerTR.localScale.x), PlayerTR.localScale.y, PlayerTR.localScale.z);
        }
        else
        {
            PlayerTR.localScale = new Vector3(Mathf.Abs(PlayerTR.localScale.x), PlayerTR.localScale.y, PlayerTR.localScale.z);
        }
        transform.position += movementSpeed * Time.deltaTime * new Vector3(inputData.x, inputData.y);

    }

    private void OnDisable()
    {
        InputManager.OnMove -= InputManager_OnMove;
    }
}

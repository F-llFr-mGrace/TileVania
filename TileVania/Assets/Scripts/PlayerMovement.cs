using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInupt;
    private void Update()
    {
        
    }
    void OnMove (InputValue value)
    {
        moveInupt = value.Get<Vector2>();
        Debug.Log(moveInupt);
    }
}

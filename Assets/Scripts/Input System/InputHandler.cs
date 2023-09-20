using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    InputActions inputActions;

    public Vector2 Interact()
    {
        return Camera.main.ScreenToWorldPoint(inputActions.Player.Interact.ReadValue<Vector2>());
    }
}

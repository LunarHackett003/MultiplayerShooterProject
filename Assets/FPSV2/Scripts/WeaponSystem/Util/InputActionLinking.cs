using System;
using UnityEngine;
using UnityEngine.InputSystem;

public static class InputActionLinking 
{
    public static void Link(this InputAction action, Action<InputAction.CallbackContext> callback)
    {
        action.performed += callback;
        action.canceled += callback;
    }
    public static void Unlink(this InputAction action, Action<InputAction.CallbackContext> callback)
    {
        action.performed -= callback;
        action.canceled -= callback;
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMotor : LunarScript
{
    public Transform head;
    public float lookSpeed;
    public Vector2 lookAngle;
    Vector2 oldLook, lookDelta;

    public float moveSpeed;

    #region Input
    public void GetMoveInput(InputAction.CallbackContext context)
    {
        MoveCharacter(context.ReadValue<Vector2>());
    }
    public void GetLookInput(InputAction.CallbackContext context)
    {
        RotateCharacter(context.ReadValue<Vector2>());
    }

    #endregion






    internal override void AfterFrame()
    {

    }

    internal override void OnFrame()
    {
    }

    internal override void OnTick()
    {

    }

    public void MoveCharacter(Vector2 input)
    {
        transform.position += moveSpeed * Time.deltaTime * ((transform.forward * input.y) + (transform.right * input.x));
    }

    public void RotateCharacter(Vector2 input)
    {
        oldLook = lookAngle;
        lookAngle += lookSpeed * input;
        lookDelta = lookAngle - oldLook;
        lookAngle.y = Mathf.Clamp(lookAngle.y, -89, 89);

        head.localRotation = Quaternion.Euler(-lookAngle.y, 0, 0);
        transform.localRotation = Quaternion.Euler(0, lookAngle.x, 0);
    }
    

}

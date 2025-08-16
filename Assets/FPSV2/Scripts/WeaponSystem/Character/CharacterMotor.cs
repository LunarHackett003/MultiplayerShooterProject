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


    #endregion






    internal override void AfterFrame()
    {
        if(InputHandler.LookInput != Vector2.zero)
        {
            RotateCharacter(InputHandler.LookInput * Time.deltaTime);
        }
    }

    internal override void OnFrame()
    {

    }

    internal override void OnTick()
    {

    }

    public void MoveCharacter()
    {
        
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

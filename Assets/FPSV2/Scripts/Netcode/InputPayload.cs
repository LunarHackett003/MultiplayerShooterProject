using Unity.Netcode;
using UnityEngine;

public struct InputPayload : INetworkSerializable
{
    public Vector2 moveInput, lookInput;
    public bool jumpInput, crouchInput, sprintInput;
    


    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref moveInput);
        serializer.SerializeValue(ref lookInput);
        serializer.SerializeValue(ref crouchInput);
        serializer.SerializeValue(ref sprintInput);
        serializer.SerializeValue(ref jumpInput);
    }


    
    
    public static InputPayload BuildPayload()
    {
        return new()
        {
            moveInput = InputHandler.MoveInput, lookInput = InputHandler.LookInput,
            jumpInput = InputHandler.JumpInput,
            crouchInput = InputHandler.CrouchInput,
            sprintInput = InputHandler.SprintInput
        };
    }
}

public static class InputPayloadExtensions
{
    public static bool EqualTo(this InputPayload left, InputPayload right)
    {
        return left.moveInput == right.moveInput && left.lookInput == right.lookInput && left.jumpInput == right.jumpInput && left.crouchInput == right.crouchInput;
    }
}

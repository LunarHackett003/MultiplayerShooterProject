using System;
using System.IO.IsolatedStorage;
using Unity.Cinemachine;
using Unity.Netcode;
using UnityEngine;


public class CharacterMotor : LunarNetScript
{
    public Transform head;
    public float lookSpeed;
    public Vector2 lookAngle;
    Vector2 oldLook, lookDelta;

    internal Rigidbody rb;
    internal Vector3 localVelocity;

    public GroundChecker groundChecker;
    public bool IsGrounded => groundChecker != null && groundChecker.onGround;
    public Vector3 GroundNormal => groundChecker == null ? Vector3.zero : groundChecker.normal;

    internal bool movementBlocked, attackBlocked, weaponActionBlocked, actionsBlocked;

    public bool sprinting, crouching, sliding;
    [SerializeField] private bool debugOnGui;
    [SerializeReference, SubclassSelector(UseToStringAsLabel = true), Tooltip("Make sure you assign the Movers here in the order of priority from highest to lowest that you wish to execute. It will only check up to the first one that is usable.")]
    public Mover[] movers;
    public int currentMover;

    public Transform crouchTransform;
    public CapsuleCollider capsule;
    public float standHeadHeight, crouchHeadHeight;

    public float standCapsuleHeight, crouchCapsuleHeight, standCapsuleY, crouchCapsuleY;
    public float crouchLerpSpeed;
    float currentCrouch;

    public CinemachineCamera cineCam;
    public InputPayload currentInput;

    public void Awake()
    {
        Initialise();
    }

    [Rpc(SendTo.Server)]
    public void InputReceivedByServer_RPC(InputPayload payload)
    {
        currentInput = payload;
    }

    void Initialise()
    {
        rb = rb != null ? rb : GetComponent<Rigidbody>();
        //Init the movers so they have what they need
        for (int i = 0; i < movers.Length; i++)
        {
            movers[i].Initialise(this);
        }
        //Make sure we do NOT have a mover by default.
        currentMover = -1;
    }
    private void OnGUI()
    {
        if (!debugOnGui)
            return;
        GUILayout.BeginVertical();
        GUILayout.Label($"Velocity:{rb.linearVelocity}");
        GUILayout.Label($"Velocity On Ground:{rb.linearVelocity}");
        GUILayout.Label($"Current Speed:{rb.linearVelocity.magnitude}");
        GUILayout.Label($"Yaw:{transform.eulerAngles.y}");
        GUILayout.Label($"Crouching: {crouching}");
        GUILayout.Label($"Sprinting: {sprinting}");
        GUILayout.Label($"Sliding: {sliding}");
        GUILayout.EndHorizontal();
    }

    internal override void AfterFrame()
    {
        if (!IsOwner)
        {
            return;
        }

        if(currentInput.lookInput != Vector2.zero)
        {
            RotateCharacter(currentInput.lookInput * Time.deltaTime);
        }
    }
    internal override void OnTick()
    {
        //If we're not the owner or server, back out.
        if (!(IsOwner || IsServer))
        {
            return;
        }
        if (IsOwner)
        {
            InputPayload lastInput = currentInput;
            currentInput = InputPayload.BuildPayload();
            if (!lastInput.EqualTo(currentInput))
            {
                InputReceivedByServer_RPC(currentInput);
            }
        }

        groundChecker.CheckGround();
        localVelocity = transform.TransformDirection(rb.linearVelocity);
        CheckMovers();
        Crouch();
    }

    void Crouch()
    {
        localVelocity = transform.TransformDirection(rb.linearVelocity);
        bool lowerHead = IsGrounded && (crouching || sliding);

        currentCrouch = Mathf.MoveTowards(currentCrouch, lowerHead ? 1 : 0, crouchLerpSpeed * Time.fixedDeltaTime);
        crouchTransform.localPosition = Vector3.up * Mathf.Lerp(standHeadHeight, crouchHeadHeight, currentCrouch);
        capsule.height = Mathf.Lerp(standCapsuleHeight, crouchCapsuleHeight, currentCrouch);
        capsule.center = Vector3.up * Mathf.Lerp(standCapsuleY, crouchCapsuleY, currentCrouch);
    }
    void CheckMovers()
    {
        for (int i = 0; i < movers.Length; i++)
        {
            Mover m = movers[i];
            if (!m.initialised)
                m.Initialise(this);
            if (m.CanMove())
            {
                //If we have a valid mover already and its NOT our mover, stop it
                if(currentMover != -1 && currentMover != i)
                {
                    movers[currentMover].StoppedMoving();       
                    m.StartedMoving();
                }
                //Assign the new one's index
                currentMover = i;
                //And start it. Using i to make sure we definitely do the correct one.
                break;
            }
            else
            {
                //If we reach the last iteration and haven't found a valid mover, we'll mark it as so by setting CurrentMover to -1.
                if (i == movers.Length - 1)
                {
                    if (currentMover != -1)
                    {
                        movers[currentMover].StoppedMoving();
                    }
                    currentMover = -1;
                }
            }
        }
        if (currentMover != -1)
            movers[currentMover].Process();
    }
    public void RotateCharacter(Vector2 input)
    {
        oldLook = lookAngle;
        lookAngle += lookSpeed * input;
        lookDelta = lookAngle - oldLook;
        lookAngle.y = Mathf.Clamp(lookAngle.y, -89, 89);
        lookAngle.x %= 360;
        lookDelta.x %= 360;
        head.localRotation = Quaternion.Euler(-lookAngle.y, 0, 0);
        transform.localRotation = Quaternion.Euler(0, lookAngle.x, 0);
    }
    

}

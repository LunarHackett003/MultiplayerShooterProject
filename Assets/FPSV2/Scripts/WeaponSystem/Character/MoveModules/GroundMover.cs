using System;
using UnityEngine;
using Lunar;
/// <summary>
/// Physics-based character motor. Uses forces and shit to make sure the character moves correctly. Delicately balanced.
/// <br></br> Handles all of the grounded movement stuff - walking, sprinting, crouching, sliding and jumping.
/// </summary>
[Serializable]
public class GroundMover : Mover
{
    public float forwardForce, strafeForce, backForce;
    public float sprintForceMultiplier, crouchForceMultiplier;
    public float movingDamp, idleDamp;
    Vector2 velocityOnGround;
    

    public float verticalJumpSpeed;
    public float landingResetTime;
    public float dampingResetTime = 0.06f;
    float landingTime;
    public float landingForceScale = 1;

    public float uncrouchHeight;
    public float uncrouchRadius;
    bool canUncrouch;


    public float slideStartSpeed, slideSteerForce, slideDamping, slideCutoffSpeed;

    public bool alignDampToSurface;

    Vector3 rightDir, rightDirFull, forwardDir, forwardDirFull, direction;

    internal override bool CanMove()
    {
        return cm.IsGrounded && !cm.movementBlocked;
    }
    internal override void Process()
    {
        if(landingTime < landingResetTime)
        {
            landingTime += Time.fixedDeltaTime;
        }

        if (cm.currentInput.jumpInput)
        {
            if (TryJump())
            {
                return;
            }
        }
        GetMoveState();
        if (!cm.sliding)
        {
            Walk();
        }
        else
        {
            Slide();
        }
    }
    void GetMoveState()
    {
        //We'll assign inputs as we go.
        //We'll check if we're sprinting;
        cm.sprinting = cm.currentInput.sprintInput;
        //If we're NOT sliding AND we're already crouching, we'll uncrouch, since we've just started sprinting
        if (cm.sprinting && !cm.sliding)
        {
            if (!cm.crouching && cm.currentInput.crouchInput)
            {
                StartSlide();
            }
        }
        //Okay so we're NOT sprinting, right?
        //Because we've already removed the crouch input from the sprint, we SHOULD be good to just do this.
        //If this ray does NOT hit then our head isn't obstructed. we can stand back up
        canUncrouch = !Physics.SphereCast(cm.transform.position, uncrouchRadius, cm.transform.up, out _, uncrouchHeight, cm.groundChecker.rayMask, QueryTriggerInteraction.Ignore);
        cm.crouching = cm.currentInput.crouchInput || (cm.crouching && !canUncrouch);
    }
    /// <summary>
    /// Extracted direction maths so I can use them elsewhere
    /// </summary>
    void CalculateDirections()
    {
        //Creates a forward-direction aligned with the ground
        forwardDirFull = Vector3.Cross(cm.transform.right, cm.GroundNormal);
        //Creates a right-direction aligned with the ground
        rightDirFull = Vector3.Cross(cm.GroundNormal, forwardDirFull);
        direction = (rightDirFull + forwardDirFull).normalized;
        //And then calculate the normalised sizes of these vectors and applies them to their own vectors, preserving the full size vector for other calculations.
        forwardDir = forwardDirFull * Vector3.Dot(forwardDirFull, direction);
        rightDir = rightDirFull * Vector3.Dot(rightDirFull, direction);
    }

    void Walk()
    {
        CalculateDirections();


        //Display the current vectors relevant to movement
        Debug.DrawRay(cm.transform.position, forwardDir, Color.blue, Time.fixedDeltaTime);
        Debug.DrawRay(cm.transform.position, cm.GroundNormal, Color.green, Time.fixedDeltaTime);
        Debug.DrawRay(cm.transform.position, rightDir, Color.red, Time.fixedDeltaTime);

        //We no longer need this since now FLOATY
        ////Only apply gravity counter if we're on a slope
        //if (doGravityCounter)
        //{
        //    //Create the vector that will push against gravity, since we use a frictionless player.
        //    Vector3 gravCounter = Vector3.ProjectOnPlane(Physics.gravity, cm.GroundNormal);
        //    Debug.DrawRay(cm.transform.position - (cm.transform.up * 0.1f), gravCounter, Color.yellow);
        //    //Apply the Grav Counter force. This is just gravity projected against the plane we're standing on and then inverted.
        //    cm.rb.AddForce(-gravCounter * cm.rb.mass);
        //}
        //Then check our velocity relative to the direction our character can move in, since we'll need this later.
        //Since rightDir and forwardDir are unit vectors, this will give the magnitude of each axis.

        //This determines whether or not we've started moving again after jumping/leaving the ground. this SHOULD fix the small bursts of speed you get when moving in certain places, or when moving and jumping.
        if (landingTime >= dampingResetTime) 
        {
            if (cm.rb.linearVelocity != Vector3.zero)
            {
                velocityOnGround = new Vector2(Vector3.Dot(cm.rb.linearVelocity, rightDirFull), Vector3.Dot(cm.rb.linearVelocity, forwardDirFull));
                //If we're not moving on an axis, we want to apply a force opposite to our local velocity on that axis to damp us, helping to counter any unwanted velocity.
                Vector3 dampRight = (cm.currentInput.moveInput.x == 0 ? idleDamp : movingDamp) * velocityOnGround.x * -rightDir;
                Vector3 dampForward = (cm.currentInput.moveInput.y == 0 ? idleDamp : movingDamp) * velocityOnGround.y * -forwardDir;

                //Apply the damping force
                cm.rb.AddForce(dampRight + dampForward);
            }
            if (cm.currentInput.moveInput != Vector2.zero)
            {
                //And then we add our movement force!
                //We check our current move state, since if we're sprinting or crouching, we want to move slightly differently
                float forceMultiplier = cm.crouching ? crouchForceMultiplier : cm.sprinting ? sprintForceMultiplier : 1;

                Vector3 moveRight = cm.currentInput.moveInput.x * strafeForce * rightDir;
                Vector3 moveForward = cm.currentInput.moveInput.y * (cm.currentInput.moveInput.y < 0 ? backForce : forwardForce) * forwardDir;
                cm.rb.AddForce((moveRight + moveForward) * forceMultiplier);
            }
        }
    }
    void StartSlide()
    {
        cm.sliding = true;
        cm.rb.AddForce(cm.rb.mass * slideStartSpeed * cm.rb.linearVelocity.normalized, ForceMode.VelocityChange);
    }
    void CancelSlide()
    {
        Debug.Log("Cancelled Slide");
        cm.sliding = false;
    }
    void Slide()
    {
        //Cancels out sideways velocity based on steer force
        cm.rb.AddForce(-cm.rb.linearVelocity * slideDamping);
        if(cm.rb.linearVelocity.magnitude < slideCutoffSpeed)
        {
            CancelSlide();
        }
    }
    bool TryJump()
    {
        //We want to prevent the player from jumping again
        cm.currentInput.jumpInput = false;
        if (cm.IsOwner)
        {
            InputHandler.JumpInput = false;
        }
        if(landingTime >= landingResetTime)
        {
            Vector3 jumpForce = (cm.rb.mass * verticalJumpSpeed * Vector3.up);
            if(cm.rb.linearVelocity.y < 0)
            {
                jumpForce += Vector3.up * cm.rb.linearVelocity.y;
            }
            landingTime = 0;
            cm.rb.AddForce(jumpForce, ForceMode.VelocityChange);
            return true;
        }
        return false;
    }
    internal override void StartedMoving()
    {
        base.StartedMoving();
    }
    internal override void StoppedMoving()
    {
        base.StoppedMoving();
        landingTime = 0;
    }
}

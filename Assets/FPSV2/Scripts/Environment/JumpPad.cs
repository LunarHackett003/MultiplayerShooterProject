using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.VFX;
public class JumpPad : LunarScript
{
    public VisualEffect jumpPadVFX;

    public AK.Wwise.Event audioEvent;
    public QueryTriggerInteraction qti;
    // When something enters the trigger, I need to check if its close enough to the jump pad to activate it.
    public float maxRayDistance;
    public float jumpPadEnableTime = 0.2f;
    float colliderIgnoreTime = 0.2f;

    public LayerMask layermask;
    public Vector3 rayOffset;

    public Vector3 jumpDirection = Vector3.up;
    public float jumpForce = 50;

    internal override void OnTick()
    {
        base.OnTick();
        if(colliderIgnoreTime > 0)
            colliderIgnoreTime -= Time.fixedDeltaTime;
    }

    private void OnTriggerStay(Collider other)
    {


        if (colliderIgnoreTime <= 0 && other.attachedRigidbody)
        {
            Vector3 origin = new Vector3(other.attachedRigidbody.position.x, transform.position.y, other.attachedRigidbody.position.z) + rayOffset;
            if (Physics.Raycast(origin, transform.up, maxRayDistance, layermask, qti))
            {
                Bounce(other.attachedRigidbody);
                colliderIgnoreTime = jumpPadEnableTime;
            }
        }
    }

    void Bounce(Rigidbody other)
    {
        other.AddForce(jumpDirection * jumpForce, ForceMode.VelocityChange);
        jumpPadVFX.Play();
        Debug.Log("Launched Object!");
        AudioManager.PostEvent(gameObject, audioEvent);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position + rayOffset, jumpDirection * maxRayDistance);
    }
}

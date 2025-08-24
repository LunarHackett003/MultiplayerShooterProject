using System;
using UnityEngine;
[Serializable]
public class AirMover : Mover
{
    public float airMoveForce, airDamping;

    internal override bool CanMove()
    {
        return !cm.IsGrounded && !cm.movementBlocked;
    }

    internal override void Process()
    {
        Vector3 airMoveForce = ((cm.transform.right * cm.currentInput.moveInput.x) + (cm.transform.forward * cm.currentInput.moveInput.y)).normalized * this.airMoveForce;
        Vector3 damping = -cm.rb.linearVelocity * airDamping;
        damping.y = Math.Max(0, damping.y);
        cm.rb.AddForce(airMoveForce + damping);
    }
}

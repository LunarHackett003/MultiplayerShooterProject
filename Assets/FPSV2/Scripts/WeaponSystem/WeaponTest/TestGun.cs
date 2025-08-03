using UnityEngine;

public class TestGun : LunarScript
{
    public Vector3 aimPosition;
    public Quaternion aimRotation;

    public bool equipped;
    public float aimSpeed;
    internal float aimAmount;
    internal bool fireInput, aimInput;


    internal override void AfterFrame()
    {

    }

    internal override void OnFrame()
    {
        aimAmount = Mathf.Clamp01(aimAmount + (aimSpeed * (aimInput ? 1 : -1)));
    }

    internal override void OnTick()
    {

    }
}

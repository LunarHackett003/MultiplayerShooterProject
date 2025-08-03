using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponTester : LunarScript
{
    /*
     * Test script!
     * I want to achieve something similar to Call of Duty's quick switching thing when you equip certain grips on pistols.
     * Not necessarily with the same grip thing, but I still want to see how this might work on its own before i try to
     * integrate it into existing systems. I also need to figure out how this might work with my rig, too.
     */
    public enum EquipmentState
    {
        primary,
        side,
        secondary
    }

    public EquipmentState CurrentState => stateFlow[stateIndex];
    public KeyCode switchKey;
    public int stateIndex;
    public EquipmentState[] stateFlow;
    public Vector3 mainPosition;
    public Quaternion mainRotation = Quaternion.identity;
    public Vector3 sidePosition;
    public Quaternion sideRotation = Quaternion.identity;
    public Transform t1, t2;
    public Transform weaponTransform;
    public Transform primarySidePoint;
    public Transform secondarySidePoint;
    public Transform aimPosePoint;
    public TestGun gun1, gun2;

    public TestGun currentGun;
    public float currentAim;
    public float posLerpSpeed = 5, rotLerpSpeed = 15;

    Vector3 primaryLerpToPos, secondaryLerpToPos;
    Quaternion primaryLerpToRot = Quaternion.identity, secondaryLerpToRot = Quaternion.identity;
    private void Start()
    {
        stateIndex = stateFlow.Length - 1;
        SwitchState();
    }
    public void SwitchState()
    {
        stateIndex = (stateIndex+1)%stateFlow.Length;
        if(currentGun != null && currentGun == gun1)
        {
            currentGun.aimAmount = 0;
            currentGun.aimInput = false;
            currentGun.fireInput = false;
        }
        switch (CurrentState)
        {
            case EquipmentState.primary:
                t1.gameObject.SetActive(true);
                t2.gameObject.SetActive(false);
                currentGun = gun1;
                break;
            case EquipmentState.side:
                t1.gameObject.SetActive(true);
                t2.gameObject.SetActive(true);
                currentGun = gun2;
                break;
            case EquipmentState.secondary:
                t1.gameObject.SetActive(false);
                t2.gameObject.SetActive(true);
                currentGun = gun2;
                break;
            default:
                break;
        }
        aimPosePoint.SetLocalPositionAndRotation(currentGun.aimPosition, currentGun.aimRotation);
    }

    internal override void AfterFrame()
    {

    }

    internal override void OnFrame()
    {
        if (Input.GetKeyDown(switchKey))
        {
            SwitchState();
        }

        UpdateWeaponPose();
    }

    internal override void OnTick()
    {

    }

    void UpdateWeaponPose()
    {
        currentAim = Mathf.Clamp01(currentAim + (Time.deltaTime * currentGun.aimSpeed * (Input.GetMouseButton(1) ? 1 : -1)));


        switch (CurrentState)
        {
            case EquipmentState.primary:
                primaryLerpToPos = Vector3.Lerp(weaponTransform.TransformPoint(mainPosition), aimPosePoint.position, currentAim);
                primaryLerpToRot = Quaternion.Lerp(mainRotation * weaponTransform.rotation, aimPosePoint.rotation, currentAim);
                break;
            case EquipmentState.side:
                //t1.SetPositionAndRotation(primarySidePoint.position, primarySidePoint.rotation);
                //t2.SetPositionAndRotation(Vector3.Lerp(secondarySidePoint.position, aimPosePoint.position, currentAim), Quaternion.Lerp(mainRotation * secondarySidePoint.rotation, aimPosePoint.rotation, currentAim));
                primaryLerpToPos = primarySidePoint.position;
                primaryLerpToRot = primarySidePoint.rotation;
                secondaryLerpToPos = Vector3.Lerp(secondarySidePoint.TransformPoint(mainPosition), aimPosePoint.position, currentAim);
                secondaryLerpToRot = Quaternion.Lerp(mainRotation * secondarySidePoint.rotation, aimPosePoint.rotation, currentAim);
                break;
            case EquipmentState.secondary:
                secondaryLerpToPos = Vector3.Lerp(weaponTransform.TransformPoint(mainPosition), aimPosePoint.position, currentAim);
                secondaryLerpToRot = Quaternion.Lerp(mainRotation * weaponTransform.rotation, aimPosePoint.rotation, currentAim);
                break;
            default:
                break;
        }

        t1.SetPositionAndRotation(Vector3.MoveTowards(t1.position, primaryLerpToPos, posLerpSpeed * Time.deltaTime), Quaternion.RotateTowards(t1.rotation, primaryLerpToRot, rotLerpSpeed * Time.deltaTime));
        t2.SetPositionAndRotation(Vector3.MoveTowards(t2.position, secondaryLerpToPos, posLerpSpeed * Time.deltaTime), Quaternion.RotateTowards(t2.rotation, secondaryLerpToRot, rotLerpSpeed * Time.deltaTime));
    }

    private void OnValidate()
    {
        if (Application.isPlaying)
            return;

        if (t1 != null && t2 != null)
            UpdateWeaponPose();
    }
}

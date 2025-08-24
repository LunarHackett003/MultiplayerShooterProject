using Unity.Netcode;
using UnityEngine;


public class LunarNetScript : NetworkBehaviour
{
    /// <summary>
    /// Similar to update: called when the frame happens.
    /// </summary>
    internal virtual void OnFrame()
    {

    }
    /// <summary>
    /// Similar to FixedUpdate: called 1/timestep times a second.
    /// </summary>
    internal virtual void OnTick()
    {

    }
    /// <summary>
    /// Similar to LateUpdate: called just after OnFrame.
    /// </summary>
    internal virtual void AfterFrame()
    {

    }


    protected virtual void OnEnable()
    {
        LunarManager.Subscribe(OnFrame, OnTick, AfterFrame);
    }
    protected virtual void OnDisable()
    {
        LunarManager.Unsubscribe(OnFrame, OnTick, AfterFrame);
    }
}

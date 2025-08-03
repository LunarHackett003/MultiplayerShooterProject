using UnityEngine;

/// <summary>
/// Monobehaviour-derived version of LunarScript. Use this for scripts that do not need network functions.
/// </summary>
public abstract class LunarScript : MonoBehaviour
{
    /// <summary>
    /// Similar to update: called when the frame happens.
    /// </summary>
    internal abstract void OnFrame();
    /// <summary>
    /// Similar to FixedUpdate: called 1/timestep times a second.
    /// </summary>
    internal abstract void OnTick();
    /// <summary>
    /// Similar to LateUpdate: called just after OnFrame.
    /// </summary>
    internal abstract void AfterFrame();


    private void OnEnable()
    {
        LunarManager.Subscribe(this);
    }
    private void OnDisable()
    {
        LunarManager.Unsubscribe(this);
    }
}

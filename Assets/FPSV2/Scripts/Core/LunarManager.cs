using System;
using UnityEngine;

/// <summary>
/// This script handles running all of our LunarScripts for us. Its like the whole update manager thing.
/// Google Unity 10000 Update Calls to find out more :)
/// </summary>
/// 
[DefaultExecutionOrder(0)]
public class LunarManager : MonoBehaviour
{
    /// <summary>
    /// We need to initialise the LunarManager.
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    public static void InitialiseOnLoad()
    {
        Debug.Log("Creating lunar manager...");
        GameObject go = new("Lunar Manager");
        DontDestroyOnLoad(go);
        _instance = go.AddComponent<LunarManager>();
    }

    public static LunarManager Singleton => _instance;
    static LunarManager _instance;

    //Now that's out of the way...
    //Btw, this script should NEVER be added to an object manually. It should ALWAYS be created by the program itself.
    private void Start()
    {
        if (_instance != this)
        {
            //If this script is NOT the singleton, then we'll nuke it. Get it GONE
            Debug.Log("Destroying extra manager...");
            Destroy(gameObject);
        }
    }

    public delegate void UpdateDelegate();
    public delegate void FixedUpdateDelegate();
    public delegate void LateUpdateDelegate();

    public UpdateDelegate OnFrame;
    public FixedUpdateDelegate OnTick;
    public LateUpdateDelegate AfterFrame;

    public static void Subscribe(UpdateDelegate onFrame, FixedUpdateDelegate onTick, LateUpdateDelegate afterFrame)
    {
        _instance.OnFrame += onFrame;
        _instance.OnTick += onTick;
        _instance.AfterFrame += afterFrame;
    }
    public static void Unsubscribe(UpdateDelegate onFrame, FixedUpdateDelegate onTick, LateUpdateDelegate afterFrame)
    {
        _instance.OnFrame -= onFrame;
        _instance.OnTick -= onTick;
        _instance.AfterFrame -= afterFrame;
    }
    private void Update()
    {
        OnFrame?.Invoke();
    }
    private void FixedUpdate()
    {
        OnTick?.Invoke();
    }
    private void LateUpdate()
    {
        AfterFrame?.Invoke();
    }
}

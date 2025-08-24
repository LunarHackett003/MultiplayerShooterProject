using UnityEngine;

public class AudioManager : LunarScript
{


    public AK.Wwise.Bank[] banksToLoad;

    private void Awake()
    {
        for (int i = 0; i < banksToLoad.Length; i++)
        {
            banksToLoad[i].Load();
        }
    }
    private void OnDestroy()
    {
        for (int i = 0; i < banksToLoad.Length; i++)
        {
            banksToLoad[i].Unload();
        }
    }







    public static void PostEvent(GameObject sender, AK.Wwise.Event akEvent)
    {
        akEvent.Post(sender);
    }
}

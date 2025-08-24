using Unity.Netcode;
using UnityEngine;

public class NetworkAssistant : MonoBehaviour
{
    public static NetworkAssistant Instance {  get; private set; }
    public bool isServer, isClient;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
            return;
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();
        if(!isServer && !isClient)
        {
            if (GUILayout.Button("Start Server"))
            {
                StartServer();
            }
            if(GUILayout.Button("Start Host"))
            {
                StartHost();
            }
            if (GUILayout.Button("Start Client"))
            {
                StartClient();
            }
        }
        else
        {
            if (GUILayout.Button($"Shutdown {(isServer ? (isClient ? "Host" : "Server") : "Client")}"))
            {
                Shutdown();
            }
        }
        GUILayout.EndVertical();
    }
    void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        isServer = isClient = true;
    }
    void StartClient()
    {
        NetworkManager.Singleton.StartClient();
        isClient = true;
    }
    void StartServer()
    {
        NetworkManager.Singleton.StartServer();
        isServer = true;
    }
    void Shutdown()
    {
        NetworkManager.Singleton.Shutdown();
        isServer = isClient = false;
    }

}

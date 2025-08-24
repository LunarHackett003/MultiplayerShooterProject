using UnityEngine;
using UnityEngine.UI;

public class RenderUITexture : LunarScript
{
    private RenderTexture tempRT;
    private Camera cam;

    private RenderTexture target;

    public RawImage output;
    int height, width;
    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void Init()
    {
        tempRT = RenderTexture.GetTemporary(Screen.width, Screen.height);
        cam.targetTexture = tempRT;
        height = Screen.height;
        width = Screen.width;
    }
    internal override void OnFrame()
    {
        base.OnFrame();
        if (height != Screen.height || width != Screen.width)
        {
            Init();
        }
        cam.Render();
        Graphics.Blit(tempRT, target);

        RenderTexture.ReleaseTemporary(tempRT);
    }

}

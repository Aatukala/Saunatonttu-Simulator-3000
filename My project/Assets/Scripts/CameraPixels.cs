using UnityEngine;

public class LowResBlit : MonoBehaviour
{
    public RenderTexture lowResRT;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (lowResRT != null)
        {
            Graphics.Blit(lowResRT, dest);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}

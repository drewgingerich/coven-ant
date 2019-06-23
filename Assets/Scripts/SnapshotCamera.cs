using UnityEngine;

public class SnapshotCamera : MonoBehaviour
{
    public int resWidth = 1920;
    public int resHeight = 1080;

    [System.NonSerialized]
    public new Camera camera;

    // https://answers.unity.com/questions/22954/how-to-save-a-picture-take-screenshot-from-a-camer.html
    public Texture2D TakeSnapshot()
    {
        RenderTexture rt = new RenderTexture(camera.pixelWidth, camera.pixelHeight, 24);
        RenderTexture.active = rt;
        camera.targetTexture = rt;
        camera.Render();

        Rect snapshotRect = new Rect(
            resWidth * 0.25f,
            resHeight * 0.25f,
            resWidth * 0.5f, 
            resHeight * 0.5f
        );

        Texture2D snapshot = new Texture2D(camera.pixelWidth, camera.pixelHeight, TextureFormat.RGB24, false);
        snapshot.ReadPixels(snapshotRect, 0, 0);

        camera.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);

        return snapshot;
    }
}

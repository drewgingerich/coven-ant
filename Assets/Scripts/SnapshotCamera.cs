using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SnapshotCamera : MonoBehaviour
{
    [System.NonSerialized]
    public new Camera camera;

    private void Start()
    {
        this.camera = GetComponent<Camera>();
    }

    // https://answers.unity.com/questions/22954/how-to-save-a-picture-take-screenshot-from-a-camer.html
    public Texture2D TakeSnapshot()
    {
        RenderTexture rt = new RenderTexture(camera.pixelWidth, camera.pixelHeight, 32);
        RenderTexture.active = rt;
        camera.targetTexture = rt;
        camera.Render();

        Rect snapshotRect = new Rect(
            0,
            0,
            camera.pixelWidth,
            camera.pixelHeight
        );

        Texture2D snapshot = new Texture2D(camera.pixelWidth, camera.pixelHeight, TextureFormat.RGBA32, false);
        snapshot.ReadPixels(snapshotRect, 0, 0);

        camera.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);

        return snapshot;
    }
}

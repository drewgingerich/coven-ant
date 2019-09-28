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
        var width = camera.pixelWidth;
        var height = camera.pixelHeight;

        RenderTexture rt = new RenderTexture(width, width, 32);

        camera.targetTexture = rt;
        camera.Render();

        Rect snapshotRect = new Rect(0, 0, width, width);

        Texture2D snapshot = new Texture2D(width, width, TextureFormat.RGBA32, false);
        RenderTexture.active = rt;
        snapshot.ReadPixels(snapshotRect, 0, 0);

        RenderTexture.active = null; // JC: added to avoid errors
        camera.targetTexture = null;
        Destroy(rt);

        return snapshot;
    }
}

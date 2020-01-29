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
        camera.aspect = 1f;
        var length = camera.pixelWidth;

        RenderTexture rt = new RenderTexture(length, length, 32);

        RenderTexture.active = rt;
        camera.targetTexture = rt;
        camera.Render();

        Rect snapshotRect = new Rect(0, 0, length, length);

        Texture2D snapshot = new Texture2D(length, length, TextureFormat.RGBA32, false);
        snapshot.ReadPixels(snapshotRect, 0, 0);

        RenderTexture.active = null; // JC: added to avoid errors
        camera.targetTexture = null;
        Destroy(rt);

        return snapshot;
    }
}

using UnityEngine;

public class GameEndHandler : MonoBehaviour
{
    public SelectableNavigator navigator;
    public SnapshotCamera snapshotCamera;
    public ImageUploader uploader;
    public QrCodeRenderer qrCodeRenderer;

    public void OnGameEnd()
    {
        navigator.gameObject.SetActive(false);
        // Texture2D snapshot = snapshotCamera.TakeSnapshot();
        // string url = uploader.UploadImage(snapshot);
        // qrCodeRenderer.DisplayQrCode(url);
        // Transition to picture browser scene
    }

}

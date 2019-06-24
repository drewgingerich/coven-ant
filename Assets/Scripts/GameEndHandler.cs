using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class GameEndHandler : MonoBehaviour
{
    public UnityEvent OnRoutineEnd;

    public SelectableNavigator navigator;
    public SnapshotCamera snapshotCamera;
    public ImageUploader uploader;
    public QrCodeRenderer qrCodeRenderer;

    private bool waitForUpload;
    private string url;

    public void OnGameEnd()
    {
        StartCoroutine(GameEndRoutine());
    }

    private IEnumerator GameEndRoutine()
    {
        navigator.gameObject.SetActive(false);
        Texture2D snapshot = snapshotCamera.TakeSnapshot();
        waitForUpload = true;
        uploader.UploadImage(snapshot);
        while (waitForUpload) {
            yield return null;
        }
        qrCodeRenderer.DisplayQrCode(url);
    }

    private void HandleUpload(string url)
    {
        waitForUpload = false;
        this.url = url;
    }
}

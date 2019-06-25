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
    public CharacterStore characterStore;

    private bool waitForUpload;
    private string url;
    // TODO: let the player set this
    private string characterName = "Ant Man";

    public void OnGameEnd()
    {
        StartCoroutine(GameEndRoutine());
    }

    private IEnumerator GameEndRoutine()
    {
        navigator.gameObject.SetActive(false);
        Texture2D snapshot = snapshotCamera.TakeSnapshot();
        waitForUpload = true;

        uploader.onUploadComplete.AddListener(HandleUpload);

        // Upload the finished character image to imgur
        uploader.UploadImage(snapshot);

        while (waitForUpload) {
            yield return null;
        }

        // Display a QR code for the imgur link
        qrCodeRenderer.DisplayQrCode(url);

        // Persist character data
        characterStore.SetCharacter(url, characterName);
    }

    private void HandleUpload(string imageUrl)
    {
        waitForUpload = false;
        this.url = imageUrl;
    }
}

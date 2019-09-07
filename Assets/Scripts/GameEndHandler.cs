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
    public NameGenerator nameGenerator;
    public SceneLoader sceneLoader;


    public void OnGameEnd()
    {
        navigator.gameObject.SetActive(false);
        Texture2D snapshot = snapshotCamera.TakeSnapshot();

        uploader.onUploadComplete.AddListener(HandleUpload);
        uploader.onUploadFail.AddListener(HandleUploadFail);

        // Upload the finished character image to imgur
        uploader.UploadImage(snapshot);
    }

    private void HandleUpload(string imageUrl)
    {
        qrCodeRenderer.gameObject.SetActive(true);
        qrCodeRenderer.onQrCodeDisplayed.AddListener(() =>
        {
            CharacterStore.offline = false;
            characterStore.SetCharacter(imageUrl, nameGenerator.GenerateName());
        });
        qrCodeRenderer.DisplayQrCode(imageUrl);
    }

    private void HandleUploadFail()
    {
        CharacterStore.offline = true;
        sceneLoader.FadeScene("HallOfFame");
    }
}

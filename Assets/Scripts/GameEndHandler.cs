﻿using UnityEngine;
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

    public void OnGameEnd()
    {
        navigator.gameObject.SetActive(false);
        Texture2D snapshot = snapshotCamera.TakeSnapshot();

        uploader.onUploadComplete.AddListener(HandleUpload);

        // Upload the finished character image to imgur
        uploader.UploadImage(snapshot);
    }

    private void HandleUpload(string imageUrl)
    {
        // Display a QR code for the imgur link
        qrCodeRenderer.DisplayQrCode(imageUrl);

        // Persist character data
        characterStore.SetCharacter(imageUrl, nameGenerator.GenerateName());
    }
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class ImageUploader : MonoBehaviour
{
	public string clientId = "b448c8366a1c37a";
	public string baseUploadUrl = "https://api.imgur.com/3/upload";

	[System.Serializable]
	public class UploadCompleteEvent : UnityEvent<string> { }

	public UploadCompleteEvent onUploadComplete;

	public void UploadImage(Texture2D texture)
	{
		StartCoroutine(UploadImageRoutine(texture));
	}

	private IEnumerator UploadImageRoutine(Texture2D texture)
	{
		var imageData = texture.EncodeToPNG();
		var base64Image = Convert.ToBase64String(imageData);

		WWWForm data = new WWWForm();
		data.AddField("image", base64Image);

		UnityWebRequest uploadRequest = UnityWebRequest.Post(baseUploadUrl, data);
		uploadRequest.SetRequestHeader("Authorization", "Client-ID" + clientId);

		yield return uploadRequest.SendWebRequest();

		if (uploadRequest.isNetworkError)
		{
			Debug.Log("Error While Sending: " + uploadRequest.error);
		}
		else
		{
			Debug.Log("Received: " + uploadRequest.downloadHandler.text);
		}
	}
}
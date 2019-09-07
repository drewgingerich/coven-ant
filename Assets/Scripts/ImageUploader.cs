using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class ImageUploader : MonoBehaviour
{
	public string clientId = "b448c8366a1c37a";
	public string baseUploadUrl = "https://api.imgur.com/3/upload";

	[System.Serializable]
	public class UploadCompleteEvent : UnityEvent<string> { }

	// Consumed by GameEndHandler via script
	public UploadCompleteEvent onUploadComplete;
	public UnityEvent onUploadFail;

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
		uploadRequest.SetRequestHeader("Authorization", "Client-ID " + clientId);

		StartCoroutine(UploadTimeout());
		yield return uploadRequest.SendWebRequest();

		if (uploadRequest.isNetworkError)
		{
			Debug.Log("Error While Sending: " + uploadRequest.error);
			onUploadFail.Invoke();
		}
		else
		{
			var jsonResponseString = uploadRequest.downloadHandler.text;
			var json = TinyJson.JSONParser.FromJson<object>(jsonResponseString);
			var uploadData = ((Dictionary<string, object>)json)["data"];
			var link = ((Dictionary<string, object>)uploadData)["link"].ToString();
			Debug.Log("Image accessible at: " + link);
			onUploadComplete.Invoke(link);
		}
	}

	private IEnumerator UploadTimeout()
	{
		yield return new WaitForSeconds(3f);
		onUploadFail.Invoke();
		StopAllCoroutines();
	}
}
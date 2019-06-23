using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class ImageUploader : MonoBehaviour
{
    public string clientId;
    public string baseUploadUrl;

    [System.Serializable]
    public class QrCodeEvent : UnityEvent<string>
    {
    }

    public QrCodeEvent generateQrCode;

    public void UploadImage(Texture2D texture)
    {
        var imageData = texture.EncodeToPNG();
        var base64Image = Convert.ToBase64String(imageData);

        StartCoroutine(
        Upload(base64Image, (response) =>
        {
            if (generateQrCode != null)
            {
                Debug.Log("invoking generateQrCode!");
                generateQrCode.Invoke(response);
            } else
            {
                Debug.Log("generateQrCode = null");
            }
        }));
    }

    private IEnumerator Upload(string base64Image, Action<string> onUploadCompleted)
    {
        using (var wClient = new WebClient())
        {
            wClient.Headers.Add("Authorization", "Client-ID " + clientId);
            var parameters = new NameValueCollection(){
                { "image", base64Image }
            };

            byte[] response = wClient.UploadValues(baseUploadUrl, parameters);
            string uploadJsonString = Encoding.UTF8.GetString(response);

            Debug.Log("uploaded image: " + uploadJsonString);

            var json = TinyJson.JSONParser.FromJson<object>(uploadJsonString);
            var uploadData = ((Dictionary<string, object>)json)["data"];
            var link = ((Dictionary<string, object>)uploadData)["link"].ToString();

            onUploadCompleted(link);
        }
        yield return null;
    }
}
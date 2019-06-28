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
    public class UploadCompleteEvent : UnityEvent<string>
    {
    }

    public UploadCompleteEvent onUploadComplete;

    public void UploadImage(Texture2D texture)
    {
        var imageData = texture.EncodeToPNG();
        var base64Image = Convert.ToBase64String(imageData);

        using (var wClient = new WebClient())
        {
            wClient.Headers.Add("Authorization", "Client-ID " + clientId);
            var parameters = new NameValueCollection(){
                { "image", base64Image }
            };

            wClient.UploadValuesCompleted += WClient_UploadValuesCompleted;
            wClient.UploadValuesAsync(new System.Uri(baseUploadUrl), parameters);
        }
    }

    private void WClient_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
    {
        if (e.Error != null)
        {
            Debug.Log("error uploading image: " + e.Error);
            return;
        }

        var uploadJsonString = Encoding.UTF8.GetString(e.Result);

        Debug.Log("uploaded image: " + uploadJsonString);

        var json = TinyJson.JSONParser.FromJson<object>(uploadJsonString);
        var uploadData = ((Dictionary<string, object>)json)["data"];
        var link = ((Dictionary<string, object>)uploadData)["link"].ToString();

        onUploadComplete.Invoke(link);
    }
}
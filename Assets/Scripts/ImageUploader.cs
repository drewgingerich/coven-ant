using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

#if UNITY_STANDALONE
using System.Net;
#else
using UnityEngine.Networking;
#endif

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

#if UNITY_STANDALONE
        using (var wClient = new WebClient())
        {
            wClient.Headers.Add("Authorization", "Client-ID " + clientId);
            var parameters = new NameValueCollection(){
                { "image", base64Image }
            };

            wClient.UploadValuesCompleted += WClient_UploadValuesCompleted;
            wClient.UploadValuesAsync(new System.Uri(baseUploadUrl), parameters);
        }
#else
        StartCoroutine(StartUpload(baseUploadUrl, base64Image));
#endif
    }

#if UNITY_STANDALONE
    private void WClient_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
    {
        if (e.Error != null)
        {
            Debug.Log("error uploading image: " + e.Error);
            return;
        }

        var uploadJsonString = Encoding.UTF8.GetString(e.Result);

        HandleUpload(uploadJsonString);
    }
#else
    IEnumerator StartUpload(string url, string base64Image)
    {
        var form = new WWWForm();

        // The name of the player submitting the scores
        form.AddField("image", base64Image);

        var download = UnityWebRequest.Post(url, form);
        download.SetRequestHeader("Authorization", "Client-ID " + clientId);

        // Wait until the download is done
        yield return download.SendWebRequest();

        if (download.isNetworkError || download.isHttpError)
        {
            Debug.Log(download.error);
        }
        else
        {
            Debug.Log("Finished Uploading Screenshot");
            HandleUpload(download.downloadHandler.text);
        }
    }
#endif

    void HandleUpload(string result)
    {
        Debug.Log("uploaded image: " + result);

        var json = TinyJson.JSONParser.FromJson<object>(result);
        var uploadData = ((Dictionary<string, object>)json)["data"];
        var link = ((Dictionary<string, object>)uploadData)["link"].ToString();

        onUploadComplete.Invoke(link);
    }
}
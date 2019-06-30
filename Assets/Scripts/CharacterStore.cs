using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_STANDALONE
using System.Net;
#else
using UnityEngine.Networking;
#endif

using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;


public class CharacterStore : MonoBehaviour
{
    [System.Serializable]
    public class GetCharactersCompleteEvent : UnityEvent<List<string>>
    {
    }

    public string apiUrl;
    public string bucketId;
    public string secretKey;

    public GetCharactersCompleteEvent onGetCharactersComplete;
    public UnityEvent onSetCharacterComplete;

    const string DATE_FORMAT = "yyyyMMddHHmmss";

    public void GetCharacters()
    {
        var fullApiUrl = $"{apiUrl}/{bucketId}/?values=true&format=json&key={secretKey}";

#if UNITY_STANDALONE
        using (var wClient = new WebClient())
        {
            wClient.DownloadStringCompleted += WClient_DownloadStringCompleted;
            wClient.DownloadStringAsync(new System.Uri(fullApiUrl));
        }
#else
        StartCoroutine(StartDownload(fullApiUrl));
#endif
    }

    public void SetCharacter(string url, string givenName)
    {
        var key = DateTime.Now.ToString(DATE_FORMAT);
        var fullApiUrl = $"{apiUrl}/{bucketId}/{key}";
        var value = $"{url},{givenName},{DateTime.Today.ToShortDateString()}";

#if UNITY_STANDALONE
        using (var wClient = new WebClient())
        {
            wClient.UploadStringCompleted += WClient_UploadStringCompleted;
            wClient.UploadStringAsync(new System.Uri(fullApiUrl), value);
        }
#else
        StartCoroutine(StartUpload(fullApiUrl, value));
#endif
    }

#if UNITY_STANDALONE
    private void WClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
    {
        if (e.Error != null)
        {
            Debug.Log("error downloading characters: " + e.Error);
            return;
        }

        Debug.Log("key / value list: " + e.Result);

        HandleDownload(e.Result);
    }

    private void WClient_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
    {
        if(e.Error != null)
        {
            Debug.Log("error uploading character: " + e.Error);
            return;
        }

        HandleUpload();
    }
#else
    IEnumerator StartDownload(string url)
    {
        using (var wClient = new WWW(url))
        {
            yield return wClient;
            HandleDownload(wClient.text);
        }
    }

    IEnumerator StartUpload(string url, string value)
    {
        var download = UnityWebRequest.Put(url, value);

        // Wait until the download is done
        yield return download.SendWebRequest();

        if (download.isNetworkError || download.isHttpError)
        {
            Debug.Log(download.error);
        }
        else
        {
            Debug.Log("Finished Uploading Screenshot");
            HandleUpload();
        }
    }
#endif

    void HandleDownload(string result)
    {
        var jankyDictionary = JsonConvert.DeserializeObject<List<List<string>>>(result);
        var characters = new List<string>();

        foreach (var keyPair in jankyDictionary)
        {
            characters.Add(keyPair[1]);
        }

        onGetCharactersComplete.Invoke(characters);
    }

    void HandleUpload()
    {
        onSetCharacterComplete.Invoke();
    }
}

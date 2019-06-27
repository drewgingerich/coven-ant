using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
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
        using (var wClient = new WebClient())
        {
            var fullApiUrl = $"{apiUrl}/{bucketId}/?values=true&format=json&key={secretKey}";

            wClient.DownloadStringCompleted += WClient_DownloadStringCompleted;
            wClient.DownloadStringAsync(new System.Uri(fullApiUrl));
        }
    }

    public void SetCharacter(string url, string givenName)
    {
        using (var wClient = new WebClient())
        {
            var key = DateTime.Now.ToString(DATE_FORMAT);
            var fullApiUrl = $"{apiUrl}/{bucketId}/{key}";
            var value = $"{url},{givenName},{DateTime.Today.ToShortDateString()}";

            wClient.UploadStringCompleted += WClient_UploadStringCompleted;
            wClient.UploadStringAsync(new System.Uri(fullApiUrl), value);
        }
    }

    private void WClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
    {
        if (e.Error != null)
        {
            Debug.Log("error downloading characters: " + e.Error);
            return;
        }

        Debug.Log("key / value list: " + e.Result);

        var jankyDictionary = JsonConvert.DeserializeObject<List<List<string>>>(e.Result);
        var characters = new List<string>();

        foreach (var keyPair in jankyDictionary)
        {
            characters.Add(keyPair[1]);
        }

        onGetCharactersComplete.Invoke(characters);
    }

    private void WClient_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
    {
        if(e.Error != null)
        {
            Debug.Log("error uploading character: " + e.Error);
            return;
        }

        onSetCharacterComplete.Invoke();
    }
}

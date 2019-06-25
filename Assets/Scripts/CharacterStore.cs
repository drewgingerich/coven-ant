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
        StartCoroutine(StartGettingCharacters((response) =>
        {
            if (onGetCharactersComplete != null)
            {
                Debug.Log("invoking onGetKeysComplete!");
                onGetCharactersComplete.Invoke(response);
            }
            else
            {
                Debug.Log("onGetKeysComplete = null");
            }
        }));
    }

    public void SetCharacter(string url, string givenName)
    {
        StartCoroutine(StartSettingCharacter(url, givenName, () =>
        {
            if (onSetCharacterComplete != null)
            {
                Debug.Log("invoking onSetKeyComplete!");
                onSetCharacterComplete.Invoke();
            }
            else
            {
                Debug.Log("onSetKeyComplete = null");
            }
        }));
    }

    IEnumerator StartGettingCharacters(Action<List<string>> onGetKeysCompleted)
    {
        using (var wClient = new WebClient())
        {
            var fullApiUrl = $"{apiUrl}/{bucketId}/?values=true&format=json&key={secretKey}";
            // curl 'https://kvdb.io/BiqhxhTjZkvNFBus9WC7T2/hello' - d 'world'

            var jsonString = wClient.DownloadString(fullApiUrl);

            Debug.Log("key / value list: " + jsonString);

            var jankyDictionary = JsonConvert.DeserializeObject<List<List<string>>>(jsonString);
            var characters = new List<string>();

            foreach(var keyPair in jankyDictionary)
            {
                characters.Add(keyPair[1]);
            }

            onGetKeysCompleted(characters);
        }
        yield return null;
    }

    IEnumerator StartSettingCharacter(string url, string givenName, Action onSetKeyCompleted)
    {
        using (var wClient = new WebClient())
        {
            var key = DateTime.Now.ToString(DATE_FORMAT);
            var fullApiUrl = $"{apiUrl}/{bucketId}/{key}";
            var value = $"{url},{givenName},{DateTime.Today.ToShortDateString()}";
            var jsonString = wClient.UploadString(fullApiUrl, value);

            onSetKeyCompleted();
        }
        yield return null;
    }
}

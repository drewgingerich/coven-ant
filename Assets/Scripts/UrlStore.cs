using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;

public class UrlStore : MonoBehaviour
{
    [System.Serializable]
    public class GetKeysCompleteEvent : UnityEvent<List<string>>
    {
    }

    public string apiUrl;
    public string bucketId;
    public string secretKey;

    public GetKeysCompleteEvent onGetKeysComplete;
    public UnityEvent onSetKeyComplete;

    const string DATE_FORMAT = "yyyyMMddHHmmss";

    public void GetUrls()
    {
        StartCoroutine(GetKeys((response) =>
        {
            if (onGetKeysComplete != null)
            {
                Debug.Log("invoking onGetKeysComplete!");
                onGetKeysComplete.Invoke(response);
            }
            else
            {
                Debug.Log("onGetKeysComplete = null");
            }
        }));
    }

    public void SetUrl(string value)
    {
        StartCoroutine(SetKey(value, () =>
        {
            if (onSetKeyComplete != null)
            {
                Debug.Log("invoking onSetKeyComplete!");
                onSetKeyComplete.Invoke();
            }
            else
            {
                Debug.Log("onSetKeyComplete = null");
            }
        }));
    }

    IEnumerator GetKeys(Action<List<string>> onGetKeysCompleted)
    {
        using (var wClient = new WebClient())
        {
            var fullApiUrl = $"{apiUrl}/{bucketId}/?values=true&format=json&key={secretKey}";
            // curl 'https://kvdb.io/BiqhxhTjZkvNFBus9WC7T2/hello' - d 'world'

            //var provider = CultureInfo.InvariantCulture;
            //var key = DateTime.Now.ToString(DATE_FORMAT);
            //var date = DateTime.ParseExact(key, DATE_FORMAT, provider);
            //Debug.Log(date);

            var jsonString = wClient.DownloadString(fullApiUrl);

            Debug.Log("key / value list: " + jsonString);

            var jankyDictionary = JsonConvert.DeserializeObject<List<List<string>>>(jsonString);
            var urls = new List<string>();

            foreach(var keyPair in jankyDictionary)
            {
                urls.Add(keyPair[1]);
            }

            onGetKeysCompleted(urls);
        }
        yield return null;
    }

    IEnumerator SetKey(string value, Action onSetKeyCompleted)
    {
        using (var wClient = new WebClient())
        {
            var key = DateTime.Now.ToString(DATE_FORMAT);
            var fullApiUrl = $"{apiUrl}/{bucketId}/{key}";
            var jsonString = wClient.UploadString(fullApiUrl, value);

            onSetKeyCompleted();
        }
        yield return null;
    }
}

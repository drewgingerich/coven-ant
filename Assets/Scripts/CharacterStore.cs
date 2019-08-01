using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class CharacterStore : MonoBehaviour
{
	[System.Serializable]
	public class GetCharactersCompleteEvent : UnityEvent<List<string>> { }

	const string apiUrl = "https://kvdb.io";
	const string bucketId = "XHswQYMv1ndXXgwvhzh1sG";
	const string secretKey = "covenAnt";

	public GetCharactersCompleteEvent onGetCharactersComplete;
	public UnityEvent onSetCharacterComplete;

	const string DATE_FORMAT = "yyyyMMddHHmmss";

	public void GetCharacters()
	{
		StartCoroutine(GetCharactersRoutine());
	}

	public IEnumerator GetCharactersRoutine()
	{
		var fullApiUrl = $"{apiUrl}/{bucketId}/?values=true&format=json&key={secretKey}";

		UnityWebRequest request = UnityWebRequest.Get(fullApiUrl);
		yield return request.SendWebRequest();

		if (request.isNetworkError)
		{
			Debug.Log("Error uploading character: " + request.error);
		}
		else
		{
			var characterData = UnityWebRequest.UnEscapeURL(request.downloadHandler.text);
			Debug.Log("key / value list: " + characterData);

			var jankyDictionary = JsonConvert.DeserializeObject<List<List<string>>>(characterData);
			var characters = new List<string>();

			foreach (var keyPair in jankyDictionary)
			{
				characters.Add(keyPair[1]);
			}

			onGetCharactersComplete.Invoke(characters);
		}
	}

	public void SetCharacter(string url, string givenName)
	{
		StartCoroutine(SetCharacterRoutine(url, givenName));
	}

	public IEnumerator SetCharacterRoutine(string url, string givenName)
	{
		var key = DateTime.Now.ToString(DATE_FORMAT);
		var fullApiUrl = $"{apiUrl}/{bucketId}/{key}";
		var value = $"{url},{givenName},{DateTime.Today.ToShortDateString()}";

		UnityWebRequest uploadRequest = UnityWebRequest.Post(fullApiUrl, value);
		yield return uploadRequest.SendWebRequest();

		if (uploadRequest.isNetworkError)
		{
			Debug.Log("Error uploading character: " + uploadRequest.error);
		}
		else
		{
			onSetCharacterComplete.Invoke();
		}
	}
}

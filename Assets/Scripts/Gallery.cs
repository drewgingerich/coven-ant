using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using DG.Tweening;

public class Gallery : MonoBehaviour
{
    public GameObject galleryImagePrefab;
    public PulsateOnCommand leftArrow;
    public PulsateOnCommand rightArrow;
    public float scrollSeconds;
    public int maxCharactersToLoad = 10;

    int m_CurrentImageIndex = 0;
    List<RectTransform> m_ImageTransforms = new List<RectTransform>();

    float portraitWidth;

    bool m_IsScrolling = false;

    public void Initialize(List<string> characters)
    {
        // characters.Reverse();
        portraitWidth = galleryImagePrefab.GetComponent<RectTransform>().rect.width;
        StartCoroutine(LoadGallery(characters));
    }

    void Update()
    {
        if (m_ImageTransforms.Count > 1 && !m_IsScrolling)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");

            if (moveHorizontal > 0.1f)
            {
                ScrollRight();
            }
            else if (moveHorizontal < -0.1f)
            {
                ScrollLeft();
            }
        }
    }

    public void ScrollLeft()
    {
        UserScroll(-1);
    }

    public void ScrollRight()
    {
        UserScroll(1);
    }

    public void UserScroll (int value)
    {
        StopAllCoroutines();
        if (value == 0)
        {
            return;
        }
        StartCoroutine(UserScrollTo(m_CurrentImageIndex + value));
    }

    public IEnumerator UserScrollTo(int targetIndex)
    {
        m_IsScrolling = true;

        targetIndex = ClampTargetIndex(targetIndex);

        if (targetIndex == m_CurrentImageIndex)
        {
            yield break;
        }

        if (targetIndex < m_CurrentImageIndex)
        {
            leftArrow.PulsateOneshot(0.1f);
        }
        else if (targetIndex > m_CurrentImageIndex)
        {
            rightArrow.PulsateOneshot(0.1f);
        }

        SfxManager.Instance.PlayArrowClick();

        yield return new WaitForSeconds(0.1f);
 
        leftArrow.gameObject.SetActive(false);
        rightArrow.gameObject.SetActive(false);

        yield return StartCoroutine(ScrollTo(targetIndex, scrollSeconds));

        if (targetIndex == 1)
        {
            leftArrow.gameObject.SetActive(false);
            rightArrow.gameObject.SetActive(true);
        }
        else if (targetIndex == m_ImageTransforms.Count - 2)
        {
            leftArrow.gameObject.SetActive(true);
            rightArrow.gameObject.SetActive(false);
        }
        else
        {
            leftArrow.gameObject.SetActive(true);
            rightArrow.gameObject.SetActive(true);
        }

        m_IsScrolling = false;
    }

    public IEnumerator ScrollTo(int targetIndex, float time)
    {
        targetIndex = ClampTargetIndex(targetIndex);

        if (targetIndex == m_CurrentImageIndex)
        {
            yield break;
        }


        var offsets = GetOffsets(targetIndex);

        for (int i = 0; i < m_ImageTransforms.Count; i++)
        {
            m_ImageTransforms[i].DOLocalMoveX(offsets[i], time);
        }

        yield return new WaitForSeconds(time);

        m_CurrentImageIndex = targetIndex;
    }

    private int ClampTargetIndex(int targetIndex)
    {
        // One pad image on the left
        var lowerBound = 1;
        // Two pad images on the right
        var upperBound = m_ImageTransforms.Count - 3;
        return Mathf.Clamp(targetIndex, lowerBound, upperBound);
    }

    private void SnapTo(int targetIndex)
    {
        targetIndex = ClampTargetIndex(targetIndex);

        if (targetIndex == m_CurrentImageIndex)
        {
            return;
        }

        var offsets = GetOffsets(targetIndex);

        for (int i = 0; i < m_ImageTransforms.Count; i++)
        {
            m_ImageTransforms[i].localPosition = new Vector3(offsets[i], 0, 0);
        }

        m_CurrentImageIndex = targetIndex;
    }

    private float[] GetOffsets(int targetIndex)
    {
        var offsets = new float[m_ImageTransforms.Count];
        for (int i = 0; i < m_ImageTransforms.Count; i++)
        {
            var indexDiff = targetIndex - i;
            var targetX = indexDiff * portraitWidth * -1;
            offsets[i] = targetX;
        }
        return offsets;
    }

    private void CreatePadPortraits()
    {
        for (int i = 0; i < 3; i++)
        {
            var pad = Instantiate(galleryImagePrefab, transform).GetComponent<RectTransform>();
            m_ImageTransforms.Add(pad);
        }
        SnapTo(1);
    }

    private IEnumerator LoadGallery(List<string> characters)
    {
        CreatePadPortraits();

        int filledPortraits = 0;
        for (int i = 0; filledPortraits < maxCharactersToLoad && i < characters.Count; i++)
        {
            var characterData = characters[i].Split(',');
            var url = characterData[0];
            var characterName = characterData.ElementAtOrDefault(1);
            var createdOn = characterData.ElementAtOrDefault(2);

            var www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                continue;
            }

            var child = Instantiate(galleryImagePrefab, transform);
            var childRectTransform = child.GetComponent<RectTransform>();
            m_ImageTransforms.Insert(1, childRectTransform);

            child.GetComponentInChildren<Text>().text = characterName;

            var childImage = child.GetComponentsInChildren<Image>()[1];

            var myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            var sprite = Sprite.Create(myTexture, new Rect(0.0f, 0.0f, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f), 100f);
            childImage.sprite = sprite;

            filledPortraits++;

            SnapTo(2);
            yield return StartCoroutine(ScrollTo(1, 0.2f));
        }
    }
}

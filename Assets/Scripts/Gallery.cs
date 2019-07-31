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
    RectTransform firstImage;
    RectTransform lastImage;

    float portraitWidth;
    float m_LeftX;
    float m_RightX;

    float m_FarLeftX;
    float m_FarRightX;

    bool m_IsScrolling = false;

    public void Initialize(List<string> characters)
    {
        characters.Reverse();
        StartCoroutine(LoadGallery(characters));
    }

    // Update is called once per frame
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
        
        // TODO: autoscroll if arrow key not pressed
    }

    public void ScrollLeft()
    {
        Scroll(-1);
    }

    public void ScrollRight()
    {
        Scroll(1);
    }

    public void Scroll (int value)
    {
        if (value == 0)
        {
            return;
        }
        StartCoroutine(ScrollTo(m_CurrentImageIndex + value));
    }

    public IEnumerator ScrollTo(int targetIndex)
    {
        if (targetIndex < 1)
        {
            targetIndex = 1;
        }
        else if (targetIndex > m_ImageTransforms.Count - 2)
        {
            targetIndex = m_ImageTransforms.Count - 2;
        }

        if (targetIndex == m_CurrentImageIndex)
        {
            yield break;
        }

        m_IsScrolling = true;

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

        for (int i = 0; i < m_ImageTransforms.Count; i++)
        {
            var indexDiff = targetIndex - i;
            var transform = m_ImageTransforms[i];
            var targetX = indexDiff * portraitWidth * -1;
            transform.DOLocalMoveX(targetX, scrollSeconds);
        }

        yield return new WaitForSeconds(scrollSeconds);

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

        m_CurrentImageIndex = targetIndex;
        m_IsScrolling = false;
    }

    private IEnumerator LoadGallery(List<string> characters)
    {
        portraitWidth = galleryImagePrefab.GetComponent<RectTransform>().rect.width;
        m_RightX = galleryImagePrefab.GetComponent<RectTransform>().rect.width;
        m_LeftX = m_RightX * -1;
        m_FarRightX = m_RightX * 2;
        m_FarLeftX = m_LeftX * 2;

        firstImage = Instantiate(galleryImagePrefab, transform).GetComponent<RectTransform>();
        m_ImageTransforms.Add(firstImage);

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
            m_ImageTransforms.Add(childRectTransform);

            var childText = child.GetComponentInChildren<Text>();
            // NOTE: not enough room for date
            //childText.text = $"{characterName} - {createdOn}";
            childText.text = characterName;

            var childImage = child.GetComponentsInChildren<Image>()[1];

            var myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            //childImage.texture = myTexture;
            //childImage.SizeToParent();

            //childImage.GetComponent<RectTransform>().sizeDelta = new Vector2(100f, 100f);

            //var sprite = Sprite.Create(myTexture, new Rect(0.0f, 0.0f, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f), 100f);
            var sprite = Sprite.Create(myTexture, new Rect(0.0f, 0.0f, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f), 100f);
            childImage.sprite = sprite;

            filledPortraits++;
        }

        lastImage = Instantiate(galleryImagePrefab, transform).GetComponent<RectTransform>();
        m_ImageTransforms.Add(lastImage);

        StartCoroutine(ScrollTo(1));
    }
}

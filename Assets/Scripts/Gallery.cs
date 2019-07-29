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
                StartCoroutine(ScrollRight());
                SfxManager.Instance.PlayArrowClick();
            }
            else if (moveHorizontal < -0.1f)
            {
                StartCoroutine(ScrollLeft());
                SfxManager.Instance.PlayArrowClick();
            }
        }
        
        // TODO: autoscroll if arrow key not pressed
    }

    public IEnumerator ScrollLeft()
    {
        // Added early exit if called as a public function
        if( m_IsScrolling) {
            yield break;
        }
        
        leftArrow.PulsateOneshot(0.25f);

        // disable controls
        m_IsScrolling = true;

        var prevImageIndex = m_CurrentImageIndex;

        // update and constrain the current image index
        m_CurrentImageIndex--;
        if (m_CurrentImageIndex < 0)
        {
            m_CurrentImageIndex = 0;
            m_IsScrolling = false;
        }
        else
        {
            // move current image
            m_ImageTransforms[m_CurrentImageIndex].DOLocalMoveX(0f, scrollSeconds).OnComplete(() =>
            {
                m_IsScrolling = false;
            });

            // move previous image
            m_ImageTransforms[prevImageIndex].DOLocalMoveX(m_RightX, scrollSeconds);

            // move previous previous image
            var prevPrevImageIndex = prevImageIndex + 1;
            if (prevPrevImageIndex < m_ImageTransforms.Count)
            {
                m_ImageTransforms[prevPrevImageIndex].DOLocalMoveX(m_FarRightX, scrollSeconds);
            }

            // move next image
            var nextImageIndex = m_CurrentImageIndex - 1;
            if (nextImageIndex >= 0)
            {
                m_ImageTransforms[nextImageIndex].DOLocalMoveX(m_LeftX, scrollSeconds);
            }
        }
        yield return new WaitForSeconds(scrollSeconds);
    }

    public IEnumerator ScrollRight()
    {
        // Added early exit if called as a public function
        if( m_IsScrolling) {
            yield break;
        }

        rightArrow.PulsateOneshot(0.25f);

        // disable controls
        m_IsScrolling = true;

        var prevImageIndex = m_CurrentImageIndex;

        // update and constrain the current image index
        m_CurrentImageIndex++;
        if (m_CurrentImageIndex >= m_ImageTransforms.Count)
        {
            m_CurrentImageIndex = m_ImageTransforms.Count - 1;
            m_IsScrolling = false;
        }
        else
        {
            // move current image
            m_ImageTransforms[m_CurrentImageIndex].DOLocalMoveX(0f, scrollSeconds).OnComplete(() =>
            {
                m_IsScrolling = false;
            });

            // move previous image
            m_ImageTransforms[prevImageIndex].DOLocalMoveX(m_LeftX, scrollSeconds);

            // move previous previous image
            var prevPrevImageIndex = prevImageIndex - 1;
            if (prevPrevImageIndex >= 0)
            {
                m_ImageTransforms[prevPrevImageIndex].DOLocalMoveX(m_FarLeftX, scrollSeconds);
            }

            // move next image
            var nextImageIndex = m_CurrentImageIndex + 1;
            if (nextImageIndex < m_ImageTransforms.Count)
            {
                m_ImageTransforms[nextImageIndex].DOLocalMoveX(m_RightX, scrollSeconds);
            }
        }
        yield return new WaitForSeconds(scrollSeconds);
    }

    private IEnumerator LoadGallery(List<string> characters)
    {
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

        m_ImageTransforms[0].DOLocalMoveX(m_LeftX, 0f);
        m_ImageTransforms[1].DOLocalMoveX(0f, 0f);
        m_ImageTransforms[2].DOLocalMoveX(m_RightX, 0f);
        
        for (int i = 3; i < m_ImageTransforms.Count; i++)
        {
            m_ImageTransforms[i].DOLocalMoveX(m_FarRightX, 0f);
        } 
    }
}

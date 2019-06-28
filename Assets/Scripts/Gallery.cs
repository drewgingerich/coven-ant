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
    public GameObject galleryImagePrefabMid;
    public GameObject galleryImagePrefabLeft;
    public GameObject galleryImagePrefabRight;
    public PulsateOnCommand leftArrow;
    public PulsateOnCommand rightArrow;
    public float scrollSeconds;
    public int maxCharactersToLoad = 10;

    int m_CurrentImageIndex = 0;
    List<RectTransform> m_ImageTransforms = new List<RectTransform>();

    float m_LeftX;
    float m_RightX;

    float m_FarLeftX;
    float m_FarRightX;

    bool m_IsScrolling = false;

    public void Initialize(List<string> characters)
    {
        characters.Reverse();
        StartCoroutine(LoadGallery(characters.Take(maxCharactersToLoad).ToList()));
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
                SfxManager.Instance.PlayArrowClick();
            }
            else if (moveHorizontal < -0.1f)
            {
                ScrollLeft();
                SfxManager.Instance.PlayArrowClick();
            }
        }
        
        // TODO: autoscroll if arrow key not pressed
    }

    public void ScrollLeft()
    {
        // Added early exit if called as a public function
        if( m_IsScrolling) {
            return;
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
    }

    public void ScrollRight()
    {
        // Added early exit if called as a public function
        if( m_IsScrolling) {
            return;
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
    }

    IEnumerator LoadGallery(List<string> characters)
    {
        //var lastTransformX = transform.position.x;

        for (var i = 0; i < characters.Count; i++)
        {
            var character = characters[i].Split(',');
            var url = character[0];
            var characterName = character.ElementAtOrDefault(1);
            var createdOn = character.ElementAtOrDefault(2);

            var www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                GameObject child = null;

                // Use the prefab with the extra left image if this is the first portrait,
                // the default prefab for all of the middle portraits,
                // and the prefab with the extra right image if this is the last portrait
                if (i == 0)
                {
                    child = Instantiate(galleryImagePrefabLeft, transform);
                } else if (i == characters.Count - 1)
                {
                    child = Instantiate(galleryImagePrefabRight, transform);
                } else
                {
                    child = Instantiate(galleryImagePrefabMid, transform);
                }

                var childRectTransform = child.GetComponent<RectTransform>();
                m_ImageTransforms.Add(childRectTransform);

                if (i == 0)
                {
                    m_RightX = m_ImageTransforms[0].rect.width;
                    m_LeftX = m_RightX * -1;

                    m_FarRightX = m_RightX * 2;
                    m_FarLeftX = m_LeftX * 2;
                }

                var childText = child.GetComponentInChildren<Text>();
                // NOTE: not enough room for date
                //childText.text = $"{characterName} - {createdOn}";
                childText.text = characterName;

                var childImage = child.GetComponentsInChildren<Image>()[1];
                //var childImage = child.GetComponentInChildren<RawImage>();

                var myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                //childImage.texture = myTexture;
                //childImage.SizeToParent();

                //childImage.GetComponent<RectTransform>().sizeDelta = new Vector2(100f, 100f);

                //var sprite = Sprite.Create(myTexture, new Rect(0.0f, 0.0f, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f), 100f);
                var sprite = Sprite.Create(myTexture, new Rect(0.0f, 0.0f, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f), 100f);
                childImage.sprite = sprite;

                if (i > 1)
                {
                    childRectTransform.DOLocalMoveX(m_FarRightX, 0f);
                } else if (i == 1)
                {
                    childRectTransform.DOLocalMoveX(m_RightX, 0f);
                } else
                {
                    childRectTransform.DOLocalMoveX(0f, 0f);
                }
            }
        }
    }
}

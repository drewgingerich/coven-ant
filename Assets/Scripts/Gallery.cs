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
    //public int scrollWaitInSeconds = 1;

    [Range(0, 2)]
    public float sidePositionPercentage = 1f;

    int m_CurrentImageIndex = 0;
    List<RectTransform> m_ImageTransforms = new List<RectTransform>();

    float m_LeftX;
    float m_RightX;

    float m_FarLeftX;
    float m_FarRightX;

    bool m_IsScrolling = false;

    void Start()
    {
        m_RightX = Screen.width * sidePositionPercentage;
        m_LeftX = 0f;

        m_FarRightX = m_RightX * 1.5f;
        m_FarLeftX = m_LeftX - (m_RightX / 2);
    }

    public void Initialize(List<string> characters)
    {
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

    private void ScrollLeft()
    {
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
            m_ImageTransforms[m_CurrentImageIndex].DOMoveX(transform.position.x, 1f).OnComplete(() =>
            {
                m_IsScrolling = false;
            });

            // move previous image
            m_ImageTransforms[prevImageIndex].DOMoveX(m_RightX, 1f);

            // move previous previous image
            var prevPrevImageIndex = prevImageIndex + 1;
            if (prevPrevImageIndex < m_ImageTransforms.Count)
            {
                m_ImageTransforms[prevPrevImageIndex].DOMoveX(m_FarRightX, 1f);
            }

            // move next image
            var nextImageIndex = m_CurrentImageIndex - 1;
            if (nextImageIndex >= 0)
            {
                m_ImageTransforms[nextImageIndex].DOMoveX(m_LeftX, 1f);
            }
        }
    }

    private void ScrollRight()
    {
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
            m_ImageTransforms[m_CurrentImageIndex].DOMoveX(transform.position.x, 1f).OnComplete(() =>
            {
                m_IsScrolling = false;
            });

            // move previous image
            m_ImageTransforms[prevImageIndex].DOMoveX(m_LeftX, 1f);

            // move previous previous image
            var prevPrevImageIndex = prevImageIndex - 1;
            if (prevPrevImageIndex >= 0)
            {
                m_ImageTransforms[prevPrevImageIndex].DOMoveX(m_FarLeftX, 1f);
            }

            // move next image
            var nextImageIndex = m_CurrentImageIndex + 1;
            if (nextImageIndex < m_ImageTransforms.Count)
            {
                m_ImageTransforms[nextImageIndex].DOMoveX(m_RightX, 1f);
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
                var myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                var sprite = Sprite.Create(myTexture, new Rect(0.0f, 0.0f, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f));
                var child = Instantiate(galleryImagePrefab, transform);

                var childText = child.GetComponentInChildren<Text>();
                childText.text = $"{characterName} - {createdOn}";
                
                var childImage = child.GetComponent<Image>();
                childImage.sprite = sprite;

                var childRectTransform = child.GetComponent<RectTransform>();
                m_ImageTransforms.Add(childRectTransform);

                if (i > 1)
                {
                    childRectTransform.DOMoveX(m_FarRightX, 0f);
                } else if (i == 1)
                {
                    childRectTransform.DOMoveX(m_RightX, 0f);
                }
            }
        }
    }
}

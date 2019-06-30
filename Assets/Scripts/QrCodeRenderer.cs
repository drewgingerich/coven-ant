using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#if UNITY_STANDALONE
using ZXing;
using ZXing.QrCode;
#endif

public class QrCodeRenderer : MonoBehaviour
{
    public Image qrImage;
    public UnityEvent onQrCodeDisplayed;

    public void DisplayQrCode(string text)
    {
        var myQR = GenerateQR(text);
        var sprite = Sprite.Create(myQR, new Rect(0.0f, 0.0f, myQR.width, myQR.height), new Vector2(0.5f, 0.5f));

        qrImage.gameObject.SetActive(true);
        qrImage.sprite = sprite;
        onQrCodeDisplayed.Invoke();
    }

	private Texture2D GenerateQR(string text)
	{
		var encoded = new Texture2D(256, 256);
		var color32 = Encode(text, encoded.width, encoded.height);
		encoded.SetPixels32(color32);
		encoded.Apply();
		return encoded;
	}

    private static Color32[] Encode(string textForEncoding, int width, int height)
    {
#if UNITY_STANDALONE
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
#else
        return new Color32[0];
#endif

    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class FourImageSlideshowWithCountdown : MonoBehaviour
{
    [Header("教學圖片播放")]
    public Image displayImage;
    public Sprite image1;
    public Sprite image2;
    public Sprite image3;
    public Sprite image4;
    public float interval = 1f; // 每張圖片顯示秒數

    [Header("倒數 UI")]
    public TMP_Text countdownText;  // 顯示倒數 3,2,1,Start!

    private Sprite[] slides;
    private int index = 0;

    void Start()
    {
        slides = new Sprite[] { image1, image2, image3, image4 };
        displayImage.sprite = slides[index];
    }

    void OnEnable()
    {
        
        InvokeRepeating(nameof(NextSlide), interval, interval);
    }

    void NextSlide()
    {
        index++;

        // 👉 播完第 4 張 → 隱藏圖片 → 開始倒數
        if (index >= slides.Length)
        {
            index = 0;
            displayImage.sprite = slides[index];
            countdownText.gameObject.SetActive(true);
            CancelInvoke();
            displayImage.gameObject.SetActive(false);
            return;
        }

        displayImage.sprite = slides[index];
    }
}
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownSlider : MonoBehaviour
{
    public StartControl startControl;
    public Image image;
    public TMP_Text scoreText,endScore;
    public Slider slider;          // 指向你的 Slider
    public float duration = 30f;   // 倒數總時長設定為30秒
    public bool start=false,end=false;

    private float timer;

    void Start()
    {
        timer = duration;
        slider.maxValue = 1;
        slider.value = 1;
    }

    void Update()
    {
        if(start)
        {
            // 計時器遞減
            timer -= Time.deltaTime;

            // 剩餘比例（0~1）
            float t = Mathf.Clamp01(timer / duration);

            // 套用到 Slider
            slider.value = t;

            // 倒數結束
            if (timer <= 0 && !end)
            {
                scoreText.gameObject.SetActive(false);
                image.gameObject.SetActive(true);
                endScore.text = scoreText.text;
                startControl.end = true;
                end = true;
            }
        }
        else
        {
            scoreText.gameObject.SetActive(true);
            image.gameObject.SetActive(false);
            timer = duration;
            slider.maxValue = 1;
            slider.value = 1;
            end = false;
        }
        
    }
}
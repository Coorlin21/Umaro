using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCount : MonoBehaviour
{
    GameObject speedControler;
    SpeedControl speedControl;
    public int score = 0;
    public bool start = false;
    public TMP_Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        speedControler = GameObject.Find("Speed Control");
        speedControl = speedControler.GetComponent<SpeedControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if(start)
        {
            score += Mathf.CeilToInt(speedControl.speed * 100);
            
        }
        else
        {
            score = 0;
        }
        scoreText.text = "Score:\n" + score;
    }
}

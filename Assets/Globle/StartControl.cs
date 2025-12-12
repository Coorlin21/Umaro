using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartControl : MonoBehaviour
{
    public Image image;
    public GameObject oB,sC,scC,iL;
    public Slider slider;
    public bool end = false;
    bool start=false,safe=true;
    

    // Start is called before the first frame update
    void Start()
    {
        image.gameObject.SetActive(false);
        oB.SetActive(false);
        iL.SetActive(false);
        sC.GetComponent<SpeedControl>().start = start;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!start || end)
            {
                start = !start;
            }
            
            if(start)
            {
                if(safe)
                {
                    image.gameObject.SetActive(true);
                    safe = false;
                }
                
            }
            else
            {
                iL.SetActive(start);
                oB.SetActive(start);
                sC.GetComponent<SpeedControl>().start = start;
                scC.GetComponent<ScoreCount>().start = start;
                slider.GetComponent<CountdownSlider>().start = start;
                safe = true;
                end = false;

            }
            
        }
    }
    public void GameStart()
    {
        oB.SetActive(start);
        iL.SetActive(start);
        sC.GetComponent<SpeedControl>().start = start;
        scC.GetComponent<ScoreCount>().start = start;
        slider.GetComponent<CountdownSlider>().start = start;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SiteLoop : MonoBehaviour
{
    GameObject speedControler;
    SpeedControl speedControl;
    float speed = 0.3f;
    
    // Start is called before the first frame updat\e
    
    void Start()
    {
        speedControler = GameObject.Find("Speed Control");
        speedControl = speedControler.GetComponent<SpeedControl>();
    }
    void Update()
    {
        speed = speedControl.speed;
    }

    void FixedUpdate()
    {
        gameObject.transform.Translate(new Vector3(-speed, 0f, 0f));
        if(gameObject.transform.position.x<=-120f)
        {
            gameObject.transform.position = new Vector3(165f, 0f, 0f);
        }
    }
}

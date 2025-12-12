using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLoop : MonoBehaviour
{
    public GameObject[] items = new GameObject[3];
    GameObject speedControler;
    SpeedControl speedControl;
    float speed = 0.3f,distance = 100f;
    int i;
    // Start is called before the first frame update
    void Start()
    {
        speedControler = GameObject.Find("Speed Control");
        speedControl = speedControler.GetComponent<SpeedControl>();
    }
    void OnEnable()
    {
        items[0].transform.position = new Vector3(-100f,0f,0f);
        items[1].transform.position = new Vector3(-100f,0f,0f);
        items[2].transform.position = new Vector3(-100f,0f,0f);
        i = Random.Range(0,3);
        switch(i)
        {
            case 0:
                items[0].transform.position = new Vector3(distance,0f,Random.Range(-1,2)*2);
                break;
            case 1:
                items[1].transform.position = new Vector3(distance,0f,Random.Range(-1,2)*2);
                break;
            case 2:
                items[2].transform.position = new Vector3(distance,0f,Random.Range(-1,2)*2);
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {
        speed = speedControl.speed;
    }

    void FixedUpdate()
    {
        items[i].transform.Translate(new Vector3(-speed, 0f, 0f));
        if(items[i].transform.position.x <= -10f)
        {
            i = Random.Range(0,3);
            switch(i)
        {
            case 0:
                items[0].transform.position = new Vector3(distance,0f,Random.Range(-1,2)*2);
                break;
            case 1:
                items[1].transform.position = new Vector3(distance,0f,Random.Range(-1,2)*2);
                break;
            case 2:
                items[2].transform.position = new Vector3(distance,0f,Random.Range(-1,2)*2);
                break;
        }
        }
    }
}

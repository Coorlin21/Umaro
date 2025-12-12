using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesLoop : MonoBehaviour
{
    public GameObject[] obstacles = new GameObject[8];
    GameObject speedControler;
    SpeedControl speedControl;
    float speed = 0.3f,distance = 80f;
    
    int i;
    // Start is called before the first frame update
    void Start()
    {
        speedControler = GameObject.Find("Speed Control");
        speedControl = speedControler.GetComponent<SpeedControl>();
    }
    void OnEnable()
    {
        obstacles[0].transform.position = new Vector3(-100f,0f,0f);
        obstacles[1].transform.position = new Vector3(-100f,0f,0f);
        obstacles[2].transform.position = new Vector3(-100f,0f,0f);
        obstacles[3].transform.position = new Vector3(-100f,0f,0f);
        obstacles[4].transform.position = new Vector3(-100f,0f,0f);
        obstacles[5].transform.position = new Vector3(-100f,0f,0f);
        obstacles[6].transform.position = new Vector3(-100f,0f,0f);
        obstacles[7].transform.position = new Vector3(-100f,0f,0f);
        i = Random.Range(0,8);
        switch(i)
        {
            case 0:
                obstacles[0].transform.position = new Vector3(distance,0f,Random.Range(-1,2)*2);
                break;
            case 1:
                obstacles[1].GetComponent<Mushroom>().trigger = false;
                obstacles[1].transform.position = new Vector3(distance,0f,Random.Range(-1,2)*2);
                break;
            case 2:
                obstacles[2].transform.position = new Vector3(distance,0f,0f);
                break;
            case 3:
                obstacles[3].GetComponent<Turtle>().trigger = false;
                obstacles[3].transform.position = new Vector3(distance,0f,0f);
                break;
            default:
                obstacles[i].transform.position = new Vector3(distance,0f,0f);
                break;
        }
    }
    void Update()
    {
        speed = speedControl.speed;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        obstacles[i].transform.Translate(new Vector3(-speed, 0f, 0f));
        if(obstacles[i].transform.position.x <= -10f)
        {
            i = Random.Range(0,8);
            switch(i)
        {
            case 0:
                obstacles[0].transform.position = new Vector3(distance,0f,Random.Range(-1,2)*2);
                break;
            case 1:
                obstacles[1].GetComponent<Mushroom>().trigger = false;
                obstacles[1].transform.position = new Vector3(distance,0f,Random.Range(-1,2)*2);
                break;
            case 2:
                obstacles[2].transform.position = new Vector3(distance,0f,0f);
                break;
            case 3:
                obstacles[3].GetComponent<Turtle>().trigger = false;
                obstacles[3].transform.position = new Vector3(distance,0f,0f);
                break;
            default:
                obstacles[i].transform.position = new Vector3(distance,0f,0f);
                break;
        }
        }
    }

}

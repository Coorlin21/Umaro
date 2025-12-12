using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] 
    float speed = 0.1f;
    public bool trigger = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if(trigger)
        {
            
            if(gameObject.transform.position.y <= 1.82f)
            {
                gameObject.transform.Translate(new Vector3(0f, speed, 0f));
            }
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        trigger = true;
    }
    void OnTriggerExit(Collider collider)
    {
        trigger = false;
    }
}

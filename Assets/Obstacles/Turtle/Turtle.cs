using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour
{
    [SerializeField] 
    float speed = 0.25f;
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
            
            if(gameObject.transform.position.z >= -11f)
            {
                gameObject.transform.Translate(new Vector3(0f, 0f, -speed));
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

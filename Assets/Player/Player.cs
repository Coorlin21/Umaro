using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    Rigidbody rb;
    CapsuleCollider capsuleCollider;
    GameObject speedControler,scorecounter;
    SpeedControl speedControl;
    ScoreCount scoreCount;
    Animator animator;
    AudioSource audioSource;
    
    public int pose=0;
    public int position = 0;
    [SerializeField]
    float jumpForce = 1f, moveSpeed = 1f;
    bool isGrounded = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        speedControler = GameObject.Find("Speed Control");
        speedControl = speedControler.GetComponent<SpeedControl>();
        scorecounter = GameObject.Find("Score Count");
        scoreCount = scorecounter.GetComponent<ScoreCount>();
        rb = GetComponent<Rigidbody>();
        capsuleCollider =  GetComponent<CapsuleCollider>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(position)
        {
            case 0:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, 0f), moveSpeed * Time.deltaTime);
                break;
            case 1:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, -2f), moveSpeed * Time.deltaTime);
                break;
            case -1:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, 2f), moveSpeed * Time.deltaTime);
                break;
        }
        animator.SetInteger("Pose", pose);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }



    }
    public void Crouch()
    {
        if (isGrounded)
        {
            animator.SetBool("Crouch", true);
            capsuleCollider.center = new Vector3(0f,-2.5f,0f);
            capsuleCollider.height = 10;
        }
    }
    public void Stand()
    {
        animator.SetBool("Crouch", false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            scoreCount.score -=2500;
        } 
        if(other.gameObject.layer == 7)
        {
            scoreCount.score +=2500;
            audioSource.Play();
        } 
        if(other.gameObject.layer == 8)
        {
            if(pose.ToString() != other.gameObject.name )
            scoreCount.score -=2500;
        } 
    }

    void OnCollisionEnter(Collision collisionInfo) {
        if (collisionInfo.gameObject.name == "Fixed Floor") 
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.name == "Fixed Floor")
        {
            isGrounded = false;
        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {


    [SerializeField] private float velocity;
    [SerializeField] private float deltaVelocity;
    [SerializeField] private GameController gameController;
    [SerializeField] private PlatformController platformController;
    private Rigidbody rb;
    private float vel;
    private Vector3 initialVelocity;
    private bool first = true;
    private float initialDelta;
    private bool goingDown = false;
    private float mult;
    private float time;
    private int hits;
    private float platformSpeed;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        vel = 0;
        initialDelta = deltaVelocity;
        //initialVelocity = velocity;
        time = Time.time - 0.62f;
        hits = 0;
        mult = 1;
    }
	
	
	//void FixedUpdate () {
 //       if(vel <= 0)
 //       {
 //           goingDown = true;
 //           vel = 0;
 //       }
 //       else if(vel >= velocity)
 //       {
 //           vel = velocity;
 //       }
 //       if(goingDown)
 //       {
 //           vel += deltaVelocity;
 //           rb.velocity = new Vector3(0, -vel, 0);
            
 //       } else
 //       {
 //           vel -= deltaVelocity;
 //           rb.velocity = new Vector3(0, vel, 0);
 //       }
        
 //   }

    public float GetTime()
    {
        return Time.time - time;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(first)
        {
            initialVelocity = rb.velocity;
            first = false;
        }
        //Time.timeScale += 0.1f;
        rb.velocity = initialVelocity;
        hits++;
        Debug.Log("Time: " + (Time.time - time) + "; Hits: " + hits + "; Pos: " + transform.position.y);
        //if(hits % 10 == 0)
        //{
        //    mult += 0.1f;
        //    velocity = initialVelocity * mult;
        //    deltaVelocity = initialDelta * (mult * mult);
        //}
        if(hits % 5 == 0) {
            Time.timeScale += 0.1f;
        }
        if(!first) platformController.ChangeSpeed(2 / (Time.time - time));
        time = Time.time;
        goingDown = false;
    }
}

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
    private float initialVelocity;
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
        initialVelocity = velocity;
        time = Time.time - 0.62f;
        hits = 0;
        mult = 1;
    }
	
	
	void FixedUpdate () {
        if(vel <= 0)
        {
            goingDown = true;
            vel = 0;
        }
        else if(vel >= velocity)
        {
            vel = velocity;
        }
        if(goingDown)
        {
            vel += deltaVelocity;
            rb.velocity = new Vector3(0, -vel, 0);
            
        } else
        {
            vel -= deltaVelocity;
            rb.velocity = new Vector3(0, vel, 0);
        }
        
    }

    public float GetTime()
    {
        return Time.time - time;
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameController.SpawnPlatform();
        platformController.ChangeSpeed(Time.time - time);
        hits++;
        Debug.Log("Time: " + (Time.time - time) + "; Vel : " + velocity + "; Acc: " + deltaVelocity + "; Hits: " + hits);
        if(hits % 10 == 0)
        {
            mult += 0.1f;
            velocity = initialVelocity * mult;
            deltaVelocity = initialDelta * (mult * mult);
        }
        time = Time.time;
        goingDown = false;
        vel = velocity;
        rb.velocity = new Vector3(0, vel, 0);
    }
}

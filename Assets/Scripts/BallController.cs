using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {


    [SerializeField] private PlatformController platformController;
    private Rigidbody rb;
    private Vector3 initialVelocity;
    private bool first = true;
    private float time;
    private int hits;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	
	void FixedUpdate () {
        transform.position = new Vector3(2, transform.position.y, 0);
        if(Physics.Raycast(transform.position, Vector3.up, 0.5f)) {
            GetComponent<Collider>().isTrigger = true;
        } else if(Physics.Raycast(transform.position, (Vector3.up + Vector3.forward).normalized, 1)) {
            GetComponent<Collider>().isTrigger = true;
        } else if(Physics.Raycast(transform.position, (Vector3.up + Vector3.back).normalized, 1)) {
            GetComponent<Collider>().isTrigger = true;        
        } else if(Physics.Raycast(transform.position, new Vector3(0, 2, 1).normalized, 1)) {
            GetComponent<Collider>().isTrigger = true;        
        } else if(Physics.Raycast(transform.position, new Vector3(0, 2, -1).normalized, 1)) {
            GetComponent<Collider>().isTrigger = true;        
        } else if(Physics.Raycast(transform.position, Vector3.back, 1)) {
            GetComponent<Collider>().isTrigger = true;        
        } else if(Physics.Raycast(transform.position, Vector3.back, 1)) {
            GetComponent<Collider>().isTrigger = true;        
        } else if(Physics.Raycast(transform.position, Vector3.down, 0.5f)) {
            GetComponent<Collider>().isTrigger = false;
        }
    }    

    private void OnCollisionEnter(Collision collision)
    {   
        Debug.Log("Points: " + collision.contacts.Length + "; First Point: " + collision.contacts[0].point);  
        if(collision.contacts[0].point.y < 0.5) {        
            if(first)
            {
                initialVelocity = rb.velocity;
                first = false;
            }
            rb.velocity = initialVelocity;
            hits++;
            Debug.Log("Time: " + (Time.time - time) + "; Hits: " + hits + "; Pos: " + transform.position.y);
            if(hits % 5 == 0) {
                Time.timeScale += 0.1f;
            }
            if(!first) platformController.ChangeSpeed(2 / (Time.time - time));
            time = Time.time;
        }
    }
}

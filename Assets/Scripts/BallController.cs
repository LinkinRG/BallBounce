using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour {


    [SerializeField] private PlatformController platformController;
    [SerializeField] private GameController gameController;
    [SerializeField] private float minY;
    [SerializeField] private AudioClip gameOverAudio;
    [SerializeField] private Slider audioSlider;

    private Rigidbody rb;
    private Vector3 initialVelocity;
    private bool first = true;
    private int hits;
    private float time;
    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        time = 0;
        audioSlider.value = audioSource.volume;
    }
	
	
	void FixedUpdate () {
        transform.position = new Vector3(2, transform.position.y, 0);
        if(Physics.Raycast(transform.position, Vector3.up, 0.5f)) {
            GetComponent<Collider>().isTrigger = true;
        } else if(Physics.Raycast(transform.position, (Vector3.up + Vector3.forward).normalized, 1)) {
            GetComponent<Collider>().isTrigger = true;
        } else if(Physics.Raycast(transform.position, (Vector3.up + Vector3.back).normalized, 1)) {
            GetComponent<Collider>().isTrigger = true;
        } else if(Physics.Raycast(transform.position, new Vector3(0, 2, 0.5f).normalized, 1)) {
            GetComponent<Collider>().isTrigger = true;
        } else if(Physics.Raycast(transform.position, new Vector3(0, 2, -0.5f).normalized, 1)) {
            GetComponent<Collider>().isTrigger = true;
        } else if(Physics.Raycast(transform.position, new Vector3(0, 2, 5).normalized, 1)) {
            GetComponent<Collider>().isTrigger = true;
        } else if(Physics.Raycast(transform.position, new Vector3(0, 2, -5).normalized, 1)) {
            GetComponent<Collider>().isTrigger = true;
        } else if(Physics.Raycast(transform.position, Vector3.back, 1)) {
            GetComponent<Collider>().isTrigger = true;
        } else if(Physics.Raycast(transform.position, Vector3.forward, 1)) {
            GetComponent<Collider>().isTrigger = true;
        } else if(Physics.Raycast(transform.position, Vector3.down, 0.5f)) {
            GetComponent<Collider>().isTrigger = false;
        }
        if(transform.position.y <= minY) {
            audioSource.clip = gameOverAudio;
            audioSource.Play();
            gameController.GameOver();
        }
    }


    public void AdjustVolume() {
        audioSource.volume = audioSlider.value;
    }

    private void OnCollisionEnter(Collision collision)
    {   
        Debug.Log("Points: " + collision.contacts.Length + "; First Point: " + collision.contacts[0].point);  
        if(collision.contacts[0].point.y < 0.5) {            
            if(first)
            {
                initialVelocity = rb.velocity;
                first = false;
                platformController.ChangeSpeed(1 / (Time.timeSinceLevelLoad - time));
            } else {
                platformController.ChangeSpeed(2 / (Time.timeSinceLevelLoad - time));
                gameController.SpawnPlatform(2 / (Time.timeSinceLevelLoad - time));
            }
            rb.velocity = initialVelocity;
            hits++;
            Debug.Log("Time: " + (Time.timeSinceLevelLoad - time) + "; Hits: " + hits + "; Pos: " + transform.position.y);
            if(hits % 5 == 0) {
                Time.timeScale += 0.1f;
            }
            time = Time.timeSinceLevelLoad;
            audioSource.Play();
        }
    }

    public int GetScore() {
        return hits;
    }
}

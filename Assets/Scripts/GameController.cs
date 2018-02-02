using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject platform;
    public GameObject ball;
    public Transform[] spawnPositionsZ;
    public Transform[] spawnPositionsX;
    private bool onX = true;

    private BallController ballController;

    // Use this for initialization
    void Start () {
        ballController = ball.GetComponent<BallController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

    public void SpawnPlatform()
    {
        if (onX)
        {
            GameObject instance = Instantiate(platform, spawnPositionsX[Random.Range(0, spawnPositionsX.Length)].position, Quaternion.identity) as GameObject;
            instance.transform.parent = GameObject.Find("Platforms").transform;
            onX = false;
        }
        else
        {
            GameObject instance = Instantiate(platform, spawnPositionsZ[Random.Range(0, spawnPositionsZ.Length)].position, Quaternion.identity) as GameObject;
            instance.transform.parent = GameObject.Find("Platforms").transform;
            onX = true;
        }
    }
}

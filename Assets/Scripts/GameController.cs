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
        SpawnPlatforms();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

    public void SpawnPlatforms()
    {
        int[] x = {2, -2};
        float pos = 0;
        GameObject instance = Instantiate(platform, new Vector3(2, pos, 0), Quaternion.identity) as GameObject;
        instance.transform.parent = GameObject.Find("Platforms").transform;
        pos += 2;
        onX = false;
        for(int i = 0; i < 100; i++)
        {            
            //GameObject instance = Instantiate(platform, new Vector3(2 - x, pos, 0), Quaternion.identity) as GameObject;
            //instance.transform.parent = GameObject.Find("Platforms").transform;
            //pos += 1.5f;
            //instance = Instantiate(platform, new Vector3(0 - x, pos, 2), Quaternion.identity) as GameObject;
            //instance.transform.parent = GameObject.Find("Platforms").transform;
            //pos += 1.5f;
            //instance = Instantiate(platform, new Vector3(-2 - x, pos, 0), Quaternion.identity) as GameObject;
            //instance.transform.parent = GameObject.Find("Platforms").transform;
            //pos += 1.5f;
            //instance = Instantiate(platform, new Vector3(0 - x, pos, -2), Quaternion.identity) as GameObject;
            //instance.transform.parent = GameObject.Find("Platforms").transform;
            //pos += 1.5f;
            if(onX) 
            {
                instance = Instantiate(platform, new Vector3(x[Random.Range(0, x.Length)], pos, 0), Quaternion.identity) as GameObject;
                instance.transform.parent = GameObject.Find("Platforms").transform;
                pos += 2;
                onX = false;
            }
            else 
            {
                instance = Instantiate(platform, new Vector3(0, pos, x[Random.Range(0, x.Length)]), Quaternion.identity) as GameObject;
                instance.transform.parent = GameObject.Find("Platforms").transform;
                pos += 2;
                onX = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

    public float speed;
    public float rotateSpeed;
    public GameObject ball;

    private bool rotating;

    // Use this for initialization
    void Start ()
    {

    }
	
	
	void FixedUpdate () {
        float move = Input.GetAxisRaw("Horizontal");
        if(move != 0 && !rotating)
        {
            Quaternion pos = Quaternion.identity;
            pos.eulerAngles = new Vector3(0, ((transform.rotation.eulerAngles.y + move * 90) % 360), 0);
            rotating = true;
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, pos, speed * Time.deltaTime);
            StartCoroutine(rotateToNext(pos));
        }       
	}

    public void ChangeSpeed(float time)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<MoveDown>().ChangeSpeed(time);
        }
    }

    IEnumerator rotateToNext(Quaternion desiredPosition)
    {   
        while(true)
        {
            yield return new WaitForSeconds(rotateSpeed);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredPosition, speed * Time.deltaTime);
            if(transform.rotation == desiredPosition)
            {
                rotating = false;                
                yield return null;
                break;
            }
            
        }
        
    }
}
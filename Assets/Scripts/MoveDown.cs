using UnityEngine;
using System.Collections;

public class MoveDown : MonoBehaviour
{

    public float speed;
    public float minY;
    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, -speed, 0);
    }

    public void ChangeSpeed(float newSpeed) 
    {        
        rb.velocity = new Vector3(0, -newSpeed, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.y <= minY)
        {
            Destroy(gameObject);
        }
    }
}

using UnityEngine;
using System.Collections;

public class MoveDown : MonoBehaviour
{

    public float speed;
    public float minY;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 nextPos = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
        transform.position = nextPos;
        if(transform.position.y <= minY)
        {
            Destroy(gameObject);
        }
    }
}

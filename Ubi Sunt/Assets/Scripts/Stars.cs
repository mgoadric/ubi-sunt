using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{

    public float speed = -0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(speed, 0, 0);
        if (transform.position.x < -51.2f) {
            transform.position += new Vector3(51.2f, 0, 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnvironmentChanger : MonoBehaviour
{

    public int strength;
    public int direction;

    // Start is called before the first frame update
    void Start()
    {
        //transform.GetChild(0).GetComponent<Light2D>().pointLightOuterRadius = 2 * strength;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

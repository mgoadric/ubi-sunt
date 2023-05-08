using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance {get; private set;}

    private Pod pod;

    public GameObject podRotation;

    public GameObject soil;

    public GameObject growLight;

    public GameObject ubi;


    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
 
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        pod = new Pod(9, 8);

        GameObject t = Instantiate(soil, new Vector3(0, 4, 0), Quaternion.identity);
        pod.Set(0, 4, t);

        GameObject li = Instantiate(growLight, new Vector3(0, 0, 0), Quaternion.identity);
        pod.Set(0, 0, li);

        ubi.GetComponent<UbiWorking>().SetPod(pod);

        podRotation.GetComponent<PodRotation>().Setup(pod);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

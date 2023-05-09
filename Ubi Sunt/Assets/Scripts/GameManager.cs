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

    public GameObject seed;

    public GameObject growLight;

    public GameObject heater;

    public GameObject acunit;

    public GameObject ubi;


    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(ubi);
 
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        pod = new Pod(9, 8);

        MakeThing(soil, 0, 4);
        MakeThing(soil, 0, 5);
        MakeThing(soil, 0, 6);
        MakeThing(seed, 4, 2);
        MakeThing(seed, 4, 1);

        MakeThing(growLight, 1, 3);
        MakeThing(growLight, 5, 2);
        MakeThing(growLight, 3, 3);        
        MakeThing(acunit, 3, 4);
        MakeThing(heater, 5, 5);

        ubi.GetComponent<UbiWorking>().SetPod(pod);

        podRotation.GetComponent<PodRotation>().Setup(pod);
    }

    void MakeThing(GameObject thing, int x, int y) {
        GameObject t = Instantiate(thing, new Vector3(x, y, 0), Quaternion.identity);
        pod.Set(x, y, t);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance {get; private set;}

    private Pod pod;

    public GameObject podRotationPrefab;

    public GameObject thing;


    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
 
        } else {
            Destroy(gameObject);
        }



    }

    void Start() {
        pod = new Pod(9, 12);
        GameObject t = Instantiate(thing);
        pod.Set(0, 0, t);

        GameObject prf = Instantiate(podRotationPrefab);
        prf.GetComponent<PodRotation>().pod = pod;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

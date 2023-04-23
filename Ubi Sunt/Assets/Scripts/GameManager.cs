using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance {get; private set;}




    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
 
        } else {
            Destroy(gameObject);
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

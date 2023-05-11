using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{

    public string scene;

    public bool left;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider2D) {
        print("New scene?");
        if (collider2D.gameObject.tag == "Player") {
            GameManager.Instance.exitLeft = left;
            GameManager.Instance.ChangeScene(scene);
        }
    }
}

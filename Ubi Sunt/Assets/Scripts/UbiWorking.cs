using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UbiWorking : MonoBehaviour
{

    private UbiMovement movement;

    private Pod pod;

    private Stack<GameObject> storage;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<UbiMovement>();
        storage = new Stack<GameObject>();
    }

    public void SetPod(Pod pod) {
        this.pod = pod;
    }

    // Update is called once per frame
    void Update()
    {
        if (pod != null && (movement.state == UbiMovement.State.REST ||
                            movement.state == UbiMovement.State.WORKING)) {
            if (Input.GetKeyDown(KeyCode.Q)) {
                // Drop
                if (storage.Count > 0) {
                    GameObject go = storage.Pop();
                    bool placed = pod.Set((int)transform.position.x, (int)transform.position.y, go);
                    if (placed) {
                        go.transform.parent = null;
                        go.transform.localScale = Vector3.one;
                    } else {
                        storage.Push(go);
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.E)) {
                // Pick up
                print("Trying to pick up");
                GameObject go = pod.Remove((int)transform.position.x, (int)transform.position.y);
                if (go != null) {
                    storage.Push(go);
                    go.transform.parent = gameObject.transform;
                    go.transform.localScale *= 1.1f;
                }
            }
            else if (Input.GetKeyDown(KeyCode.R)) {
                // Interact (toggle?)
            }
        }        
    }
}

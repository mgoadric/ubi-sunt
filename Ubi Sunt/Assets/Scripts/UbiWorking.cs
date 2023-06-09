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

    }

    public void Drop() {
        print("Trying to drop");
        if (pod != null && (movement.state == UbiMovement.State.REST)) {
            if (storage.Count > 0) {
                GameObject go = storage.Pop();
                bool placed = pod.Set((int)transform.position.x, (int)transform.position.y, go);
                if (!placed) {
                    storage.Push(go);
                }
            }
        }
    }

    public void PickUp() {
        print("Trying to pick up");
        if (pod != null && (movement.state == UbiMovement.State.REST)) {
            GameObject go = pod.Remove((int)transform.position.x, (int)transform.position.y);
            if (go != null) {
                storage.Push(go);
                go.transform.parent = gameObject.transform;
                go.transform.localScale = Vector3.one * 1.1f;
                go.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            }
        }
    }

    public bool Replace(GameObject oldItem, GameObject newItem) {
        if (storage.Contains(oldItem)) {
            Stack<GameObject> flipped = new Stack<GameObject>();
            while (storage.Peek() != oldItem) {
                flipped.Push(storage.Pop());
            }
            storage.Pop();
            storage.Push(newItem);
            while (flipped.Count > 0) {
                storage.Push(flipped.Pop());
            }
            return true;
        }
        return false;
    }
}

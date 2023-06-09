using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollen : MonoBehaviour
{

    public GameObject target;

    public GameObject origin;

    public float easing = 0.001f;
    // Start is called before the first frame update

    public Genetics genes;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null) {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, easing);
        }   
    }

    void OnTriggerEnter2D(Collider2D collider2D) {
        if (collider2D.gameObject.tag == "Player" || collider2D.gameObject.tag == "Robot") {
            target = collider2D.gameObject;
            transform.parent = null;
        } else if (collider2D.gameObject.tag == "Plant") {
            Plant p = collider2D.gameObject.GetComponent<Plant>();
            if (origin != collider2D.gameObject && p.IsFullGrown() && !p.IsPollinated()) {
                print("POLLINATION!!!!");
                p.Pollinate(this);
                Destroy(gameObject);
            }
        }
    }

    public void SetGenes(Genetics genes) {
        this.genes = genes;
    }
}

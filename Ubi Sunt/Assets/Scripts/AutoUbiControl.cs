using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoUbiControl : MonoBehaviour
{

    public GameObject target = null;

    private static int lastPriority = 0;

    public int priority = 0;

    private Animator animator;

    private UbiWorking worker;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        worker = GetComponent<UbiWorking>();
        priority = lastPriority++;
    }

    public void SetTarget(GameObject target) {
        this.target = target;
    }

    void NewRandomTarget() {
        target.transform.position = new Vector3(UnityEngine.Random.Range(-4, 4),
            UnityEngine.Random.Range(-7, 7),
            0);
    }

    // Update is called once per frame
    void Update()
    {   
        if (target != null) {
            Vector3 desired = target.transform.position - transform.position;

            if (desired.x > 0.99) {
                animator.SetFloat("horizontal", 1);
            } else if (desired.x < -0.99) {
                animator.SetFloat("horizontal", -1);
            } else {
                animator.SetFloat("horizontal", 0);
            }

            if (desired.y > 0.99) {
                animator.SetFloat("vertical", 1);
            } else if (desired.y < -0.99) {
                animator.SetFloat("vertical", -1);
            } else {
                animator.SetFloat("vertical", 0);
            }

            if (desired.magnitude < 0.1) {
                target = null;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Robot") {
            AutoUbiControl other = col.gameObject.GetComponent<AutoUbiControl>();
            if (priority > other.priority) {
                GameObject temp = target;
                target = other.target;
                other.target = temp;
            }
        }
    }

     void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        if (target != null) {
            Gizmos.DrawWireSphere(target.transform.position, 0.5f);
            Gizmos.DrawLine(transform.position, target.transform.position);
        }
    }
}

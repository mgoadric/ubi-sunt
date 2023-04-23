using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UbiMovement : MonoBehaviour
{

    public enum State {REST, MOVING, WORKING, STOPPING};

    public Tuple<int, int>[] directions = {
        new Tuple<int, int>(0, 1),
        new Tuple<int, int>(0, -1),
        new Tuple<int, int>(1, 0),
        new Tuple<int, int>(-1, 0),
    };

    private Rigidbody2D rigidbody;
    private Animator animator;
    private Vector3 target;
    public float horizontal;
    private float lasth;
    public float vertical;
    private float lastv;

    private Vector3 velocityBeforePhysicsUpdate;

    private bool hfirst = true;

    public State state = State.REST;
    private bool firstStopping = true;

    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = animator.GetFloat("horizontal");
        vertical = animator.GetFloat("vertical");
    }

    void FixedUpdate() {
        if (horizontal != 0 && vertical != 0) {
            if (hfirst) {
                vertical = 0;
            } else {
                horizontal = 0;
            }
        } 

        if (state == State.REST) {
            if (horizontal != 0 || vertical != 0) {
                rigidbody.AddForce(new Vector2(horizontal * speed, vertical * speed));
                state = State.MOVING;
            }
        } else if (state == State.MOVING) {
            if (lasth != horizontal || lastv != vertical) {
                state = State.STOPPING;
            } else {
                rigidbody.AddForce(new Vector2(horizontal * speed, vertical * speed));
            }
        } else if (state == State.STOPPING) {
            if (firstStopping) {
                NewTarget();
                firstStopping = false;
            }

            Vector2 desired = target - transform.position;
            if (desired.magnitude < 0.01) {
                StopMoving();
                state = State.WORKING;
                StartCoroutine("Work");
            } else {
                rigidbody.AddForce(desired.normalized * (speed / 4) - rigidbody.velocity);
            }
        }
        lasth = horizontal;
        lastv = vertical;

        velocityBeforePhysicsUpdate = rigidbody.velocity;

    }

    void StopMoving() {
        transform.position = target;
        rigidbody.velocity = Vector2.zero;
        firstStopping = true;
    }

    void NewTarget() {
        target = new Vector3(Mathf.Floor(transform.position.x) + Math.Max(0, Math.Sign(velocityBeforePhysicsUpdate.x)), 
            Mathf.Floor(transform.position.y) + Math.Max(0, Math.Sign(velocityBeforePhysicsUpdate.y)), 
            transform.position.z);
    }
    void GoBack() {
        print("hv = " + lasth + "," + lastv);
        print("tr = " + transform.position.x + "," + transform.position.y);
        float newx = Mathf.Round(transform.position.x);
        if (lasth > 0) {
            newx = Mathf.Floor(transform.position.x);
        } else if (lasth < 0) {
            newx = Mathf.Ceil(transform.position.x);
        }        
        float newy = Mathf.Round(transform.position.y);
        if (lastv > 0) {
            newy = Mathf.Floor(transform.position.y);
        } else if (lastv < 0) {
            newy = Mathf.Ceil(transform.position.y);
        }
        target = new Vector3(
            newx, 
            newy, 
            transform.position.z);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        rigidbody.velocity = Vector2.zero;
        GoBack();
        firstStopping = false;
        state = State.STOPPING;
        hfirst = !hfirst;
    }


     void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.blue;
        if (state == State.STOPPING && !firstStopping) {
            Gizmos.DrawSphere(target, 0.3f);
        }
    }

    IEnumerator Work() {
        yield return new WaitForSeconds(UnityEngine.Random.value);
        if (state == State.WORKING) {
            state = State.REST;
        }
    }
}
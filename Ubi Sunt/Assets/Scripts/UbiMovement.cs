using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UbiMovement : MonoBehaviour
{

    public enum State {REST, MOVING, STOPPING};

    private Rigidbody2D body;
    private Animator animator;
    private Vector3 target;
    public float horizontal;
    private float lasth;
    public float vertical;
    private float lastv;

    public State state = State.REST;
    private bool firstStopping = true;

    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        animator.SetFloat("horizontal", horizontal);
        animator.SetFloat("vertical", vertical);
    }

    void FixedUpdate() {
        if (horizontal != 0 && vertical != 0) {
            horizontal = 0;
        } 

        if (state == State.REST) {
            if (horizontal != 0 || vertical != 0) {
                body.AddForce(new Vector2(horizontal * speed, vertical * speed));
                state = State.MOVING;
            }
        } else if (state == State.MOVING) {
            if (lasth != horizontal || lastv != vertical) {
                state = State.STOPPING;
            } else {
                body.AddForce(new Vector2(horizontal * speed, vertical * speed));
            }
        } else if (state == State.STOPPING && body.velocity.magnitude < 1) {
            if (firstStopping) {
            target = new Vector3(Mathf.Floor(transform.position.x) + Math.Max(0, Math.Sign(body.velocity.x)), 
                Mathf.Floor(transform.position.y) + Math.Max(0, Math.Sign(body.velocity.y)), 
                Mathf.Floor(transform.position.z));
                firstStopping = false;
            }

            Vector2 desired = target - transform.position;
            if (desired.magnitude < 0.01) {
                transform.position = target;
                body.velocity = Vector2.zero;
                state = State.REST;
                firstStopping = true;
            } else {
                body.AddForce(desired.normalized * (speed / 2) - body.velocity);
            }
        }
        lasth = horizontal;
        lastv = vertical;
    }
}
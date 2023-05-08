using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUbiControl : MonoBehaviour
{

    private Animator animator;
    private UbiMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<UbiMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("horizontal", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("vertical", Input.GetAxisRaw("Vertical"));
    }
}

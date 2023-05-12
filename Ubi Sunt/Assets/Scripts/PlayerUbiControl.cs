using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUbiControl : MonoBehaviour
{

    private Animator animator;

    private UbiWorking worker;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        worker = GetComponent<UbiWorking>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("horizontal", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("vertical", Input.GetAxisRaw("Vertical"));
        if (Input.GetKeyDown("q")) {
            worker.Drop();
        }
        else if (Input.GetKeyDown("e")) {
            worker.PickUp();
        }
        else if (Input.GetKeyDown("r")) {
            GameManager.Instance.ToggleInfoBox();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreDialog : MonoBehaviour
{

    public GameObject mechanic;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("IntroScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator IntroScene() {
        while (mechanic.transform.position.x < 1) {
            mechanic.transform.position += 0.01f * Vector3.right;
            yield return new WaitForSeconds(0.01f);
        }
        GameManager.Instance.DialogShow("Ubi? Where are you?");

        yield return new WaitForSeconds(2);
        GameManager.Instance.DialogHide();
        mechanic.GetComponent<SpriteRenderer>().flipX = true;

        while (mechanic.transform.position.x > -1) {
            mechanic.transform.position += 0.01f * Vector3.left;
            yield return new WaitForSeconds(0.01f);
        }
        GameManager.Instance.DialogShow("We need to get ready for docking.");
        yield return new WaitForSeconds(2);
        GameManager.Instance.DialogHide();
        mechanic.GetComponent<SpriteRenderer>().flipX = false;

        yield return new WaitForSeconds(1);
        GameManager.Instance.ChangeScene("PodContainer");

    }
}

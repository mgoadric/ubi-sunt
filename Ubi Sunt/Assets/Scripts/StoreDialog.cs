using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreDialog : MonoBehaviour
{

    public GameObject mechanic;

    public GameObject ubi;

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
            mechanic.transform.position += 0.02f * Vector3.right;
            yield return new WaitForSeconds(0.01f);
        }
        GameManager.Instance.DialogShow("Ubi? Where are you?");
        yield return new WaitForSeconds(1);

        while (ubi.transform.position.x < -3.5) {
            ubi.transform.position += 0.03f * Vector3.right;
            yield return new WaitForSeconds(0.01f);
        }
        GameManager.Instance.DialogHide();
        mechanic.GetComponent<SpriteRenderer>().flipX = true;

        while (mechanic.transform.position.x > -1) {
            mechanic.transform.position += 0.02f * Vector3.left;
            yield return new WaitForSeconds(0.01f);
        }
        GameManager.Instance.DialogShow("There you are!");
        yield return new WaitForSeconds(1);
        GameManager.Instance.DialogShow("Ok, we need to get ready for docking.");
        yield return new WaitForSeconds(1);
        GameManager.Instance.DialogShow("Can you go get a spare flange from the cargo pod?");
        yield return new WaitForSeconds(2);

        GameManager.Instance.DialogHide();
        //mechanic.GetComponent<SpriteRenderer>().flipX = false;
        while (ubi.transform.position.x > -11.5) {
            ubi.transform.position -= 0.04f * Vector3.right;
            yield return new WaitForSeconds(0.01f);
        }
        //yield return new WaitForSeconds(1);
        GameManager.Instance.ChangeScene("PodContainer");

    }
}

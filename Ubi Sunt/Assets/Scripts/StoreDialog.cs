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
        while (mechanic.transform.position.x > 0) {
            mechanic.transform.position -= 0.02f * Vector3.right;
            yield return new WaitForSeconds(0.01f);
        }
        GameManager.Instance.DialogShow("Hello again, Ubi! It is good to see you after all this time.");
        yield return new WaitForSeconds(2);
        GameManager.Instance.DialogShow("Do you have any spare AC Units to trade? We need to replace a few that have gone bad.");
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

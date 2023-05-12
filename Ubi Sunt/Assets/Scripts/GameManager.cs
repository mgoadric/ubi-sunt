using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance {get; private set;}

    public Pod pod {get; set;}

    public GameObject podPrefab;

    public GameObject podRotation;

    public bool exitLeft;

    public GameObject ubi;


    public GameObject mainMenu;

    public GameObject curtain;
    private bool raiseLower = false;
    public GameObject canvas;

    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;

    private IEnumerator textCo;
    public float textSpeed = 0.02f;

    public void DialogShow(string text) {
        dialogBox.SetActive(true);
        if (textCo != null) {
            StopCoroutine(textCo);
        }
        textCo = TypeText(text);
        StartCoroutine(textCo);
    }

    public void DialogHide() {
        dialogBox.SetActive(false);
    }

    IEnumerator TypeText(string text) {
        dialogText.text = "";
        foreach (char c in text.ToCharArray()) {
            dialogText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        textCo = null;
    }

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(canvas);
 
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        //LoadPod();
    }

    void LoadPod() {
        GameObject pp = Instantiate(podPrefab);
        pod = pp.GetComponent<Pod>();
        pod.Setup(6, 8);

        ubi = GameObject.FindWithTag("Player");
        ubi.GetComponent<UbiWorking>().SetPod(pod);
        int shift = 0;
        if (!exitLeft) {
            shift = -(pod.GetWidth() - 1);
        }
        ubi.transform.position = new Vector3(shift + pod.GetWidth() - 1, pod.GetCircumference() / 2 - 1, 0);
        Camera.main.transform.position = new Vector3(ubi.transform.position.x,
                                                     ubi.transform.position.y,
                                                     Camera.main.transform.position.z);



        podRotation = GameObject.FindWithTag("PodTiles");
        podRotation.GetComponent<PodRotation>().Setup(pod);
    }

    IEnumerator ColorLerpFunction(bool fadeout, float duration)
    {
        float time = 0;
        raiseLower = true;
        Image curtainImg = curtain.GetComponent<Image>();
        Color startValue;
        Color endValue;
        if (fadeout) {
            startValue = new Color(0, 0, 0, 0);
            endValue = new Color(0, 0, 0, 1);
        } else {
            startValue = new Color(0, 0, 0, 1);
            endValue = new Color(0, 0, 0, 0);
        }

        while (time < duration)
        {
            curtainImg.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        curtainImg.color = endValue;
        raiseLower = false;
        if (!fadeout) {
            curtain.SetActive(false);
        }
    }


     IEnumerator LoadYourAsyncScene(string scene)
     {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        curtain.SetActive(true);
        StartCoroutine(ColorLerpFunction(true, 1));

        while (raiseLower)
        {
            yield return null;
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        // TODO 
        mainMenu.SetActive(false);
        LoadPod();
        StartCoroutine(ColorLerpFunction(false, 1));
    }

    public void ChangeScene(string scene) {
        StartCoroutine(LoadYourAsyncScene(scene));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

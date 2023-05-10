using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance {get; private set;}

    private Pod pod;

    public GameObject podRotation;

    public GameObject soil;

    public GameObject seed;

    public GameObject growLight;

    public GameObject heater;

    public GameObject acunit;

    public GameObject ubi;
    public GameObject curtain;
    private bool raiseLower = false;


    public GameObject canvas;

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
        LoadPod();
    }

    void LoadPod() {
        pod = new Pod(6, 20);

        MakeThing(soil, 0, 4);
        MakeThing(soil, 0, 5);
        MakeThing(soil, 0, 6);
        MakeThing(seed, 4, 2);
        MakeThing(seed, 4, 1);

        MakeThing(growLight, 1, 3);
        MakeThing(growLight, 5, 2);
        MakeThing(growLight, 3, 3);        
        MakeThing(acunit, 3, 4);
        MakeThing(heater, 5, 5);

        ubi = GameObject.FindWithTag("Player");
        ubi.GetComponent<UbiWorking>().SetPod(pod);
        ubi.transform.position = new Vector3(pod.GetWidth() - 1, pod.GetCircumference() / 2 - 1, 0);
        Camera.main.transform.position = new Vector3(ubi.transform.position.x,
                                                     ubi.transform.position.y,
                                                     Camera.main.transform.position.z);

        podRotation = GameObject.FindWithTag("PodTiles");
        podRotation.GetComponent<PodRotation>().Setup(pod);
    }

    void MakeThing(GameObject thing, int x, int y) {
        GameObject t = Instantiate(thing, new Vector3(x, y, 0), Quaternion.identity);
        pod.Set(x, y, t);
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
    }


     IEnumerator LoadYourAsyncScene(string scene)
     {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

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

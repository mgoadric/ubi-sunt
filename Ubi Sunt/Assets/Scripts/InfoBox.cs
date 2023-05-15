using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoBox : MonoBehaviour
{

    public GameObject infoBox;

    public TextMeshProUGUI item;

    public GameObject plantStatus;

    public TextMeshProUGUI waterStatus;

    public GameObject waterStatusColor;
    public TextMeshProUGUI lightStatus;

    public GameObject lightStatusColor;
    public TextMeshProUGUI tempStatus;

    public GameObject tempStatusColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.ubi != null) {
            GameObject here = GameManager.Instance.pod.Get(
            (int)Mathf.Round(GameManager.Instance.ubi.transform.position.x),
            (int)Mathf.Round(GameManager.Instance.ubi.transform.position.y));
            if (here != null) {
                item.text = here.tag;
                if (here.tag == "Plant" || here.tag == "Seed") {
                    plantStatus.SetActive(true);
                    infoBox.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 250);
                    Plant p = here.GetComponent<Plant>();
                    waterStatus.text = p.WaterText();
                    waterStatusColor.GetComponent<Image>().color = p.WaterColor();
                    lightStatus.text = p.LightText();
                    lightStatusColor.GetComponent<Image>().color = p.LightColor();
                    tempStatus.text = p.TempText();
                    tempStatusColor.GetComponent<Image>().color = p.TempColor();
                } else {
                    plantStatus.SetActive(false);
                    infoBox.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 100);
                }
            } else {
                item.text = "Scanning ...";
                plantStatus.SetActive(false);
                infoBox.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 100);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{

    public GameObject plant;

    public float waterSaturation;

    public float decayRate;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Evaporate");
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator Evaporate() {
        while (true) {
            waterSaturation *= (1 - decayRate);
            yield return new WaitForSeconds(1);
        }
    }

    public void Water() {
        waterSaturation++;
    }

    public bool Plant(GameObject plant) {
        if (this.plant == null) {
            this.plant = plant;
            plant.GetComponent<Plant>().SetPlot(this);
            plant.transform.parent = this.transform;
            return true;
        }
        return false;
    }

    public GameObject Harvest() {
        if (plant != null) {
            GameObject p = plant;
            if (p.GetComponent<Plant>().Harvestable()) {
                plant = null;
                return p;
            } else {
                print("Plant cannot be harvested.");
            }
        }
        return null;
    }

    public float WaterLevel() {
        return Mathf.Round(waterSaturation);
    }
}

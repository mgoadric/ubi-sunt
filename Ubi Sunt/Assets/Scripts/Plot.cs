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

    public GameObject Crop() {
        if (plant != null) {
            GameObject p = plant;
            if (p.GetComponent<Plant>().Harvestable()) {
                return p.GetComponent<Plant>().Fruit();
            } else {
                print("Plant is not ready yet.");
                return p;
            }
        }
        return null;       
    }

    public GameObject Harvest() {
        if (plant != null) {
            GameObject p = plant;
            if (p.GetComponent<Plant>().Harvestable()) {
                return p.GetComponent<Plant>().TakeFruit();
            } 
        }
        return null;
    }

    public float WaterLevel() {
        return Mathf.Round(waterSaturation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{

    public GameObject plant;

    public float waterSaturation;

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
            waterSaturation *= .98f;
            yield return new WaitForSeconds(0.1f);
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
            }
        }
        return null;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

    public Sprite[] growth;

    public Plot plot;

    public float waterRequirements;

    public float lightRequirements;

    public float tempRequirements;

    public int stage;

    public float timeToGrow;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        stage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sprite = growth[stage];
    }

    public bool Harvestable() {
        return stage == growth.Length - 1;
    }

    IEnumerator Grow() {
        print("Growing??");
        while (stage < growth.Length - 1) {
            print("stage " + stage);
            yield return new WaitForSeconds(timeToGrow);
            if ( true ) { //Comfortable(plot.waterSaturation, )) {
                stage++;
            }
        }
    }

    public bool Comfortable(float water, float light, float temp) {
        if (Mathf.Abs(water - waterRequirements) < 0.1 &&
            Mathf.Abs(light - lightRequirements) < 0.1 &&
            Mathf.Abs(temp - tempRequirements) < 0.1) {
            return true;
        } else {
            return false;
        }
    }

    public void SetPlot(Plot plot) {
        this.plot = plot;
        StartCoroutine("Grow");
        print("started grow??");
    }
}

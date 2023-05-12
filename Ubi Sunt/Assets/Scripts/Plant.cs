using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

    public Sprite[] growth;

    public Plot plot;

    public GameObject pollenPrefab;

    public float waterRequirements;

    public float waterThreshold;

    public float lightRequirements;

    public float lightThreshold;

    public float tempRequirements;

    public float tempThreshold;

    public int stage;

    public float timeToGrow;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = growth[stage];
        stage = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool Harvestable() {
        print("harvest? " + stage + ", " + (growth.Length - 1));
        return stage == growth.Length - 1;
    }

    IEnumerator Grow() {
        print("Growing??");
        while (stage < growth.Length - 1) {
            print("stage " + stage);
            yield return new WaitForSeconds(timeToGrow);
            if (Comfortable(plot.WaterLevel(), 
            GameManager.Instance.pod.AmbientLight((int)transform.position.x, (int)transform.position.y),
            GameManager.Instance.pod.AmbientTemp((int)transform.position.x, (int)transform.position.y)
            )) {
                stage++;
                spriteRenderer.sprite = growth[stage];
            }
        }
        GameObject pollen = Instantiate(pollenPrefab, transform.position, Quaternion.identity);
        pollen.GetComponent<Pollen>().origin = gameObject;
        pollen.transform.parent = transform;
    }

    public bool Comfortable(float water, float light, float temp) {
        print("w = " + water + ", l = " + light + ", t = " + temp);
        if (Mathf.Abs(water - waterRequirements) < waterThreshold &&
            Mathf.Abs(light - lightRequirements) < lightThreshold &&
            Mathf.Abs(temp - tempRequirements) < tempThreshold) {
            return true;
        } else {
            return false;
        }
    }

    public void SetPlot(Plot plot) {
        this.plot = plot;
        GetComponent<Rigidbody2D>().simulated = true;
        StartCoroutine("Grow");
        print("started grow??");
    }
}

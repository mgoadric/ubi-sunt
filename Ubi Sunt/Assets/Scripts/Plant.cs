using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

    public Sprite[] growth;
    
    public GameObject fruit;

    public Plot plot;

    private bool pollinated = false;

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

    public void Pollinate() {
        pollinated = true;
    }

    public bool Harvestable() {
        print("harvest? " + stage + ", " + (growth.Length - 1));
        return stage == growth.Length - 1;
    }

    // FIXME
    public GameObject Fruit() {
        return gameObject;
    }

    public GameObject TakeFruit() {
        return gameObject;
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

    public string WaterText() {
        if (waterRequirements < 2) {
            return "ARID";
        } else if (waterRequirements > 8) {
            return "HUMID";
        } else {
            return "MED";
        }
    }

    public Color WaterColor() {
        if (plot != null) {
            if (Mathf.Abs(plot.WaterLevel() - waterRequirements) < waterThreshold) {
                return new Color(0, 1, 0);
            } else {
                return new Color(1, 0, 0);
            }
        } else {
            return new Color(1, 1, 0);
        }
    }

    public Color LightColor() {
        if (plot != null) {
            if (Mathf.Abs(GameManager.Instance.pod.AmbientLight((int)transform.position.x, (int)transform.position.y) - lightRequirements) < lightThreshold) {
                return new Color(0, 1, 0);
            } else {
                return new Color(1, 0, 0);
            }
        } else {
            return new Color(1, 1, 0);
        }
    }

    public Color TempColor() {
        if (plot != null) {
            if (Mathf.Abs(GameManager.Instance.pod.AmbientTemp((int)transform.position.x, (int)transform.position.y) - tempRequirements) < tempThreshold) {
                return new Color(0, 1, 0);
            } else {
                return new Color(1, 0, 0);
            }
        } else {
            return new Color(1, 1, 0);
        }
    }

    public string LightText() {
        if (lightRequirements <= 5) {
            return "LOW";
        } else if (lightRequirements > 10) {
            return "PART";
        } else {
            return "FULL";
        }
    }
    public string TempText() {
        if (tempRequirements < 10) {
            return "COLD";
        } else if (tempRequirements > 30) {
            return "HOT";
        } else {
            return "MOD";
        }
    }
}

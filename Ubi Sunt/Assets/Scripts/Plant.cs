using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

    public Sprite[] growth;
    public int stage;

    public GameObject fruitPrefab;
    public List<GameObject> fruits;
    public int fruitMade;

    public Plot plot;

    public Genetics pollenGenes;
    public GameObject pollenPrefab;

    public Genetics genes;

    private SpriteRenderer spriteRenderer;

    public GameObject monitor;
    public GameObject waterMonitor;
    public GameObject lightMonitor;
    public GameObject tempMonitor;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fruits = new List<GameObject>();
        spriteRenderer.sprite = growth[stage];
        stage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (plot != null && GameManager.Instance.infoBox.activeInHierarchy) {
            monitor.SetActive(true);
            waterMonitor.GetComponent<SpriteRenderer>().color = genes.WaterColor(plot.WaterLevel());
            lightMonitor.GetComponent<SpriteRenderer>().color = genes.LightColor(GameManager.Instance.pod.AmbientLight((int)transform.position.x, (int)transform.position.y));
            tempMonitor.GetComponent<SpriteRenderer>().color = genes.TempColor(GameManager.Instance.pod.AmbientTemp((int)transform.position.x, (int)transform.position.y));
        } else {
            monitor.SetActive(false);
        }
    }

    public void SetGenes(Genetics genes) {
        this.genes = genes;
    }

    public bool IsPollinated() {
        return pollenGenes != null;
    }

    public bool IsFullGrown() {
        return stage == growth.Length - 1;
    }

    public void Pollinate(Pollen pollen) {
        pollenGenes = pollen.genes;
        GetComponent<Rigidbody2D>().simulated = false;
        StartCoroutine("MakeFruit");
    }

    public bool Harvestable() {
        return fruits.Count != 0;
    }

    // FIXME
    public GameObject Fruit() {
        if (Harvestable()) {
            return fruits[0];
        }
        return null;
    }

    public GameObject TakeFruit() {
        if (Harvestable()) {
            GameObject f = fruits[0];
            fruits.RemoveAt(0);
            f.GetComponent<Fruit>().Pick();
            if (fruitMade == genes.numFruit && fruits.Count == 0) {
                StartCoroutine("Decay");
            }
            return f;
        }
        return null;
    }

    IEnumerator Decay() {
        yield return new WaitForSeconds(genes.timeToGrow);
        plot.plant = null;
        Destroy(gameObject);
    }

    IEnumerator Grow() {
        print("Growing??");
        while (stage < growth.Length - 1) {
            print("stage " + stage);
            yield return new WaitForSeconds(genes.timeToGrow);
            if (genes.Comfortable(plot.WaterLevel(), 
            GameManager.Instance.pod.AmbientLight((int)transform.position.x, (int)transform.position.y),
            GameManager.Instance.pod.AmbientTemp((int)transform.position.x, (int)transform.position.y)
            )) {
                stage++;
                spriteRenderer.sprite = growth[stage];
            }
        }
        GameObject pollen = Instantiate(pollenPrefab, transform.position, Quaternion.identity);
        pollen.GetComponent<Pollen>().origin = gameObject;
        pollen.GetComponent<Pollen>().SetGenes(genes);
        pollen.transform.parent = transform;
        GetComponent<Rigidbody2D>().simulated = true;
    }

    IEnumerator MakeFruit() {
        print("Making fruit??");
        while (fruitMade < genes.numFruit) {
            yield return new WaitForSeconds(genes.timeToGrow * 2);
            if (genes.Comfortable(plot.WaterLevel(), 
            GameManager.Instance.pod.AmbientLight((int)transform.position.x, (int)transform.position.y),
            GameManager.Instance.pod.AmbientTemp((int)transform.position.x, (int)transform.position.y)
            )) {
                GameObject f = Instantiate(fruitPrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)), transform);
                fruits.Add(f);
                // using genes from pollen and me


                f.GetComponent<Fruit>().SetGenes(genes.Recombine(pollenGenes));

                fruitMade++;
            }
        }
    }


    public void SetPlot(Plot plot) {
        this.plot = plot;
        StartCoroutine("Grow");
        print("started grow??");
    }

    public Color WaterColor() {
        if (plot != null) {
            return genes.WaterColor(plot.WaterLevel());
        } else {
            return new Color(1, 1, 0);
        }
    }

    public Color LightColor() {
        if (plot != null) {
            return genes.LightColor(GameManager.Instance.pod.AmbientLight((int)transform.position.x, (int)transform.position.y));
        } else {
            return new Color(1, 1, 0);
        }
    }

    public Color TempColor() {
        if (plot != null) {
            return genes.TempColor(GameManager.Instance.pod.AmbientTemp((int)transform.position.x, (int)transform.position.y));
        } else {
            return new Color(1, 1, 0);
        }
    }
}

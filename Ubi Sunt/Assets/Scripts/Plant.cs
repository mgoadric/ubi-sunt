using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

    public Sprite[] growth;
    
    public GameObject fruit;

    public List<GameObject> fruits;

    public int fruitMade;

    public Plot plot;

    private bool pollinated = false;

    public GameObject pollenPrefab;

    public GeneInfo genes;

    public int stage;

    private SpriteRenderer spriteRenderer;

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
    }

    public void SetGenes(GeneInfo genes) {
        this.genes = genes;
    }

    public void Pollinate(Pollen pollen) {
        StartCoroutine("MakeFruit");
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
        if (fruits.Count != 0) {
            GameObject f = fruits[0];
            fruits.RemoveAt(0);
            return f;
        }
        return null;
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
    }

    IEnumerator MakeFruit() {
        print("Making fruit??");
        while (fruitMade < genes.numFruit) {
            yield return new WaitForSeconds(genes.timeToGrow * 2);
            if (genes.Comfortable(plot.WaterLevel(), 
            GameManager.Instance.pod.AmbientLight((int)transform.position.x, (int)transform.position.y),
            GameManager.Instance.pod.AmbientTemp((int)transform.position.x, (int)transform.position.y)
            )) {
                GameObject f = Instantiate(fruit, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)), transform);
                fruits.Add(f);
                fruitMade++;
            }
        }
        // using genes from pollen and me
    }


    public void SetPlot(Plot plot) {
        this.plot = plot;
        GetComponent<Rigidbody2D>().simulated = true;
        StartCoroutine("Grow");
        transform.GetChild(0).gameObject.SetActive(true);
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

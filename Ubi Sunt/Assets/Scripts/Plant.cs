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

    public GeneInfo genes;

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

    public void SetGenes(GeneInfo genes) {
        this.genes = genes;
    }

    public void Pollinate(Pollen pollen) {
        // make fruit
        // using genes from pollen and me
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


    public void SetPlot(Plot plot) {
        this.plot = plot;
        GetComponent<Rigidbody2D>().simulated = true;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{

    public Genetics genes;

    public GameObject seedPrefab;

    public SpriteRenderer spriteRenderer;

    public float rot;
    public float rotRate;
    public float rotThreshold;

    public Color fresh = new Color(1, 1, 1);
    public Color bad = new Color(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.color = Color.Lerp(fresh, bad, (rot - 1) / (rotThreshold - 1));
    }

    public void Pick() {
        StartCoroutine("Spoil");
    }

    public void SetGenes(Genetics genes) {
        this.genes = genes;
    }

    IEnumerator Spoil() {
        while (rot < rotThreshold) {
            rot *= (1 + rotRate);
            yield return new WaitForSeconds(1);
        }

        // Leave seeds behind
        GameObject s;
        if (transform.parent == null) {
            GameManager.Instance.pod.Remove((int)transform.position.x, (int)transform.position.y);
            s = GameManager.Instance.pod.Make(seedPrefab, (int)transform.position.x, (int)transform.position.y);
        } else {
            s = Instantiate(seedPrefab, transform.position, Quaternion.identity, transform.parent);
            transform.parent.gameObject.GetComponent<UbiWorking>().Replace(gameObject, s);
            s.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
        }
        s.GetComponent<Plant>().SetGenes(genes);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class Pod
{

    public GameObject[,] storage;

    public float[,] temperature;
    public float[,] lighting;

    public List<GameObject> bots;

    public Pod(int width, int circumference) 
    {
        storage = new GameObject[width, circumference];
        bots = new List<GameObject>();
    }

    public void AddBot(GameObject bot) {
        bots.Add(bot);
    }

    public bool Set(int x, int y, GameObject go) {
        int cy = RealMod(y, GetCircumference());
        if (storage[x, cy] == null) {
            
            storage[x, cy] = go;
            go.transform.parent = null;
            go.transform.localScale = Vector3.one;
            go.GetComponent<SpriteRenderer>().sortingLayerName = "ShipContainers";
            return true;
        } else {
            Debug.Log("Seed into soil??");
            Debug.Log(storage[x, cy].tag + ", " + go.tag);
            if (storage[x, cy].tag == "Soil" && go.tag == "Seed") {
                Debug.Log("It worked!");
                if (storage[x, cy].GetComponent<Plot>().Plant(go)) {
                    go.transform.localScale = Vector3.one;
                    go.GetComponent<SpriteRenderer>().sortingLayerName = "ShipContainers";
                return true;
                }
            }
        }
        return false;
    }

    public GameObject Get(int x, int y) {
        int cy = RealMod(y, GetCircumference());
        if (storage[x, cy] != null) {
            return storage[x, cy];
        }
        return null;
    }

    public GameObject Remove(int x, int y) {
        int cy = RealMod(y, GetCircumference());
        if (storage[x, cy] != null) {
            if (storage[x, cy].tag == "Soil") {
                GameObject p = storage[x, cy].GetComponent<Plot>().Harvest();
                if (p != null) {
                    return p;
                }
            }
            GameObject go = storage[x, cy];
            storage[x, cy] = null;
            return go;
        }
        return null;
    }

    public int RealMod(int x, int m) {
        return (x%m + m)%m;
    }

    public int GetWidth() {
        return storage.GetLength(0);
    }

    public int GetCircumference() {
        return storage.GetLength(1);
    }

    public float AmbientTemp(int x, int y) {
        int cy = RealMod(y, GetCircumference());
        
        return temperature[x, cy];
    }

    public float AmbientLight(int x, int y) {
        int cy = RealMod(y, GetCircumference());

        return lighting[x, cy];
    }
}

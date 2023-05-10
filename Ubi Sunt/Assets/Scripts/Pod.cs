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
        temperature = new float[width, circumference];
        lighting = new float[width, circumference];
        bots = new List<GameObject>();
    }

    public void AddBot(GameObject bot) {
        bots.Add(bot);
    }

    public void BotLocCheck(int miny, int maxy) {
        foreach (GameObject bot in bots) {
            if (bot.transform.position.y < miny) {
                bot.transform.position += new Vector3(0, GetCircumference(), 0);
            } else if (bot.transform.position.y > maxy) {
                bot.transform.position -= new Vector3(0, GetCircumference(), 0);
            }
        }
    }

    public bool Set(int x, int y, GameObject go) {
        int cy = RealMod(y, GetCircumference());
        if (storage[x, cy] == null) {
            if (go.tag == "Heater") {
                ChangeAmbient(x, cy, 1, 1, temperature);
            }
            if (go.tag == "ACUnit") {
                ChangeAmbient(x, cy, 1, -1, temperature);
            }
            if (go.tag == "GrowLight") {
                ChangeAmbient(x, cy, 1, 1, lighting);
            }            
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
            else if (storage[x, cy].tag == "Heater") {
                ChangeAmbient(x, cy, 1, -1, temperature);
            }
            else if (storage[x, cy].tag == "GrowLight") {
                ChangeAmbient(x, cy, 1, -1, lighting);
            }   
            else if (storage[x, cy].tag == "ACUnit") {
                ChangeAmbient(x, cy, 1, 1, temperature);
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

    public void ChangeAmbient(int x, int y, int strength, int dir, float[, ] ambient) {
        for (int hx = -strength; hx < strength + 1; hx++) {
            for (int hy = -strength; hy < strength + 1; hy++) {
                if (x + hx >= 0 && x + hx < GetWidth()) {
                    float change = dir * Mathf.Max(0, (strength + 0.5f) - (new Vector2(x, y) - new Vector2(x + hx, y + hy)).magnitude);
                    ambient[hx + x, RealMod(hy + y, GetCircumference())] += change;
                }
            }
        }
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

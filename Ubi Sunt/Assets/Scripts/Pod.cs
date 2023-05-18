using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class Pod : MonoBehaviour
{

    public GameObject[,] storage;

    public float[,] temperature;
    public float[,] lighting;

    public GameObject soilPrefab;
    public GameObject seedPrefab;
    public GameObject growLightPrefab;
    public GameObject heaterPrefab;
    public GameObject acunitPrefab;
    public GameObject waterPrefab;
    public GameObject spigotPrefab;

    public List<GameObject> bots;

    public GameObject botPrefab;

    public void Setup(int width, int circumference) 
    {
        storage = new GameObject[width, circumference];
        temperature = new float[width, circumference];
        lighting = new float[width, circumference];
        bots = new List<GameObject>();

        Make(soilPrefab, 0, 4);
        Make(soilPrefab, 0, 5);
        Make(soilPrefab, 0, 6);
        GameObject s = Make(seedPrefab, 4, 2);
        s.GetComponent<Plant>().SetGenes(new Genetics("squash", 5, 1, 2, 1, 5, 1, 2, 5));
        s = Make(seedPrefab, 4, 1);
        s.GetComponent<Plant>().SetGenes(new Genetics("squash", 5, 1, 2, 1, 5, 1, 2, 5));
        s = Make(seedPrefab, 4, 0);
        s.GetComponent<Plant>().SetGenes(new Genetics("squash", 5, 1, 2, 1, 5, 1, 2, 5));

        //Make(growLightPrefab, 1, 3);
        //Make(growLightPrefab, 5, 2);
        Make(growLightPrefab, 3, 3);        
        //Make(waterPrefab, 3, 4);
        Make(heaterPrefab, 5, 5);
        Make(spigotPrefab, 0, 1);
        //Make(acunitPrefab, 5, 6);

        GameObject bot = Instantiate(botPrefab);
        AddBot(bot);
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

    GameObject Make(GameObject thing, int x, int y) {
        GameObject t = Instantiate(thing, new Vector3(x, y, 0), Quaternion.identity);
        Set(x, y, t);
        return t;
    }


    public bool Set(int x, int y, GameObject go) {
        if (x >= 0 && x < GetWidth()) {
            int cy = RealMod(y, GetCircumference());
            if (storage[x, cy] == null) {
                if (go.tag == "Heater" || go.tag == "ACUnit") {
                    EnvironmentChanger env = go.GetComponent<EnvironmentChanger>();
                    ChangeAmbient(x, cy, env.strength, env.direction, temperature);
                }  
                else if (go.tag == "GrowLight") {
                    EnvironmentChanger env = go.GetComponent<EnvironmentChanger>();
                    ChangeAmbient(x, cy, env.strength, env.direction, lighting);
                }  
                else if (go.tag == "Soil") {
                    GameObject p = go.GetComponent<Plot>().plant;
                    if (p != null) {
                        Plant plant = p.GetComponent<Plant>();
                        if (plant.IsFullGrown() && !plant.IsPollinated()) {
                            p.GetComponent<Rigidbody2D>().simulated = true;
                        }
                        plant.GetComponent<SpriteRenderer>().sortingLayerName = "ShipContainers";
                    }   
                }     
                storage[x, cy] = go;
                go.transform.parent = null;
                go.transform.localScale = Vector3.one;
                go.GetComponent<SpriteRenderer>().sortingLayerName = "ShipContainers";
                return true;
            } else if (storage[x, cy].tag == "Soil") {
                if (go.tag == "Seed") {
                    Debug.Log("Planted a seed!");
                    if (storage[x, cy].GetComponent<Plot>().Plant(go)) {
                        go.transform.localScale = Vector3.one;
                        go.GetComponent<SpriteRenderer>().sortingLayerName = "ShipContainers";
                        go.tag = "Plant";
                    return true;
                    }
                } else if (go.tag == "Water") {
                    storage[x, cy].GetComponent<Plot>().Water();
                    Destroy(go);
                    return true;
                }
            }
        }
        
        return false;
    }

    public GameObject Get(int x, int y) {
        if (x >= 0 && x < GetWidth()) {
            int cy = RealMod(y, GetCircumference());
            if (storage[x, cy] != null) {
                GameObject go = storage[x, cy];

                if (go.tag == "Soil") {
                    GameObject c = go.GetComponent<Plot>().Crop();
                    if (c != null) {
                        Debug.Log("Returning fruit!");
                        return c;
                    }
                }
                return go;
            }
        }
        return null;
    }

   public GameObject GetBase(int x, int y) {
        if (x >= 0 && x < GetWidth()) {
            int cy = RealMod(y, GetCircumference());
            if (storage[x, cy] != null) {
                GameObject go = storage[x, cy];
                return go;
            }
        }
        return null;
    }

    public GameObject Remove(int x, int y) {
        if (x >= 0 && x < GetWidth()) {

            int cy = RealMod(y, GetCircumference());
            if (storage[x, cy] != null) {
                GameObject go = storage[x, cy];

                if (go.tag == "Soil") {
                    GameObject f = go.GetComponent<Plot>().Harvest();
                    if (f != null) {
                        Debug.Log("Returning fruit!");
                        return f;
                    }
                    GameObject p = go.GetComponent<Plot>().plant;
                    if (p != null) {
                        print("WE HAVE a plant to move!");
                        p.GetComponent<Rigidbody2D>().simulated = false;
                        p.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
                    }
                }
                else if (go.tag == "Heater" || go.tag == "ACUnit") {
                    EnvironmentChanger env = go.GetComponent<EnvironmentChanger>();
                    ChangeAmbient(x, cy, env.strength, -env.direction, temperature);
                }  
                else if (go.tag == "GrowLight") {
                    EnvironmentChanger env = go.GetComponent<EnvironmentChanger>();
                    ChangeAmbient(x, cy, env.strength, -env.direction, lighting);
                }  
                else if (go.tag == "Spigot") {
                    GameObject w = Instantiate(waterPrefab, 
                    go.transform.position, Quaternion.identity);
                    return w;
                }

                storage[x, cy] = null;
                return go;
            }
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
        for (int hx = -strength / 2; hx < strength / 2 + 1; hx++) {
            for (int hy = -strength / 2; hy < strength / 2 + 1; hy++) {
                if (x + hx >= 0 && x + hx < GetWidth()) {
                    //float change = Mathf.Round(10 * (dir * Mathf.Max(0, (strength + 0.5f) - (new Vector2(x, y) - new Vector2(x + hx, y + hy)).magnitude)));
                    float change = dir * Mathf.Max(0, (strength - Mathf.Round(2 * (0.1f + (new Vector2(x, y) - new Vector2(x + hx, y + hy)).magnitude))));
                    ambient[hx + x, RealMod(hy + y, GetCircumference())] += change;
                }
            }
        }
    }

    public float AmbientTemp(int x, int y) {
        int cy = RealMod(y, GetCircumference());
        if (x >= 0 && x < GetWidth()) {
            return temperature[x, cy];
        } else {
            return 0;
        }
    }

    public float AmbientLight(int x, int y) {
        int cy = RealMod(y, GetCircumference());
        if (x >= 0 && x < GetWidth()) {
            return lighting[x, cy];
        } else {
            return 0;
        }
    }
}

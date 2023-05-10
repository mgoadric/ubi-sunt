using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PodRotation : MonoBehaviour
{

    public Pod pod;
    private Tilemap background;
    private Tilemap walls;

    public TileBase tracks;
    public TileBase wall;

    private Transform cameraTransform;
    private float lastCameraY;

    public GameObject leftExit;

    public GameObject rightExit;

    private int c;
    private int w;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Setup(Pod pod) {
        cameraTransform = Camera.main.transform;
        lastCameraY = Mathf.Floor(cameraTransform.position.y);
        background = transform.Find("Background").gameObject.GetComponent<Tilemap>();
        walls = transform.Find("Walls").gameObject.GetComponent<Tilemap>();
        
        this.pod = pod;
        w = pod.GetWidth();
        c = pod.GetCircumference();
        for (int y = 0; y < c; y++) {
            for (int x = 0; x < w; x++) {  
                background.SetTile(new Vector3Int(x, y, 0), tracks);
            }
        }

        // Connections
        for (int x = 0; x < w; x++) {  
            background.SetTile(new Vector3Int(-1 * x - 1, c / 2 - 1, 0), tracks);
            background.SetTile(new Vector3Int(w + x, c / 2 - 1, 0), tracks);
        }

        // Walls
        for (int y = 0; y < c; y++) {
            if (y != c / 2 - 1) {
                for (int x = 0; x < w; x++) {
                    walls.SetTile(new Vector3Int(-1 * x - 1, y, 0), wall);
                    walls.SetTile(new Vector3Int(w + x, y, 0), wall);
                }
            }
        }

        // Exits
        leftExit.transform.position = new Vector3(-2, c / 2 - 1, 0);
        rightExit.transform.position = new Vector3(w + 1, c / 2 - 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (pod != null) {
            float currentCameraY = Mathf.Floor(cameraTransform.position.y);
            int up = (int)lastCameraY + c / 2;
            int down = (int)lastCameraY - c / 2;

            //bool moving = false;
            if (currentCameraY > lastCameraY) {
                up += 1;
                down += 1;
                print("Going up!");
                for (int x = 0; x < pod.GetWidth(); x++) {
                    background.SetTile(new Vector3Int(x, up, 0), tracks);
                    background.SetTile(new Vector3Int(x, down, 0), null);                
                }
                
                if (pod.RealMod(up, c) != c / 2 - 1) {
                    for (int x = 0; x < w; x++) {  
                        walls.SetTile(new Vector3Int(-1 * x - 1, up, 0), wall);
                        walls.SetTile(new Vector3Int(w + x, up, 0), wall);
                        walls.SetTile(new Vector3Int(-1 * x - 1, down, 0), null);
                        walls.SetTile(new Vector3Int(w + x, down, 0), null);
                    }
                } else {
                    for (int x = 0; x < w; x++) {  
                        background.SetTile(new Vector3Int(-1 * x - 1, up, 0), tracks);
                        background.SetTile(new Vector3Int(w + x, up, 0), tracks);                        background.SetTile(new Vector3Int(-1 * x - 1, up, 0), tracks);
                        background.SetTile(new Vector3Int(-1 * x - 1, down, 0), null);
                        background.SetTile(new Vector3Int(w + x, down, 0), null);                        background.SetTile(new Vector3Int(-1 * x - 1, up, 0), tracks);
                    } 
                    leftExit.transform.position += new Vector3(0, c, 0);
                    rightExit.transform.position += new Vector3(0, c, 0);
                }

                for (int x = 0; x < pod.GetWidth(); x++) {
                    GameObject thing = pod.Get(x, down);
                    if (thing != null) {
                        thing.transform.position += new Vector3(0, c, 0);
                    }             
                }
            }
            if (currentCameraY < lastCameraY) {
                print("Going down!");
                for (int x = 0; x < pod.GetWidth(); x++) {
                    background.SetTile(new Vector3Int(x, up, 0), null);  
                    background.SetTile(new Vector3Int(x, down, 0), tracks);
                }

                if (pod.RealMod(down, c) != c / 2 - 1) {
                    for (int x = 0; x < w; x++) {  
                        walls.SetTile(new Vector3Int(-1 * x - 1, down, 0), wall);
                        walls.SetTile(new Vector3Int(w + x, down, 0), wall);
                        walls.SetTile(new Vector3Int(-1 * x - 1, up, 0), null);
                        walls.SetTile(new Vector3Int(w + x, up, 0), null);
                    }
                } else {
                    for (int x = 0; x < w; x++) {  
                        background.SetTile(new Vector3Int(-1 * x - 1, down, 0), tracks);
                        background.SetTile(new Vector3Int(w + x, down, 0), tracks);                        background.SetTile(new Vector3Int(-1 * x - 1, up, 0), tracks);
                        background.SetTile(new Vector3Int(w + x, up, 0), null);                        background.SetTile(new Vector3Int(-1 * x - 1, up, 0), tracks);
                        background.SetTile(new Vector3Int(-1 * x - 1, up, 0), null);
                    } 
                    leftExit.transform.position -= new Vector3(0, c, 0);
                    rightExit.transform.position -= new Vector3(0, c, 0);
                }
                for (int x = 0; x < pod.GetWidth(); x++) {
                    GameObject thing = pod.Get(x, down);
                    if (thing != null) {
                        thing.transform.position -= new Vector3(0, c, 0);
                    }             
                }      
            }

            lastCameraY = currentCameraY;
        }
    }
}

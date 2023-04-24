using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PodRotation : MonoBehaviour
{

    private Pod myspace;
    private Tilemap stuff;
    private Tilemap walls;

    public TileBase wall;

    private Transform cameraTransform;
    private float lastCameraY;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraY = Mathf.Floor(cameraTransform.position.y);
        myspace = new Pod(9, 9, wall);
        stuff = GetComponent<Tilemap>();
        int w = myspace.GetWidth();
        int c = myspace.GetCircumference();
        for (int y = -c / 2; y < c / 2 + 1; y++) {
            for (int x = 0; x < w; x++) {
                stuff.SetTile(new Vector3Int(x - (w / 2) - 1, y - 1, 0), 
                             myspace.GetSprite(x, y));
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        float currentCameraY = Mathf.Floor(cameraTransform.position.y);
        int c = myspace.GetCircumference();

        if (currentCameraY > lastCameraY) {
            print("Going up!");
            for (int x = 0; x < myspace.GetWidth(); x++) {
                stuff.SetTile(new Vector3Int(x - (myspace.GetWidth() / 2) - 1, (int)lastCameraY + c / 2, 0), 
                             myspace.GetSprite(x, (int)lastCameraY + c / 2));
                stuff.SetTile(new Vector3Int(x - (myspace.GetWidth() / 2) - 1, (int)lastCameraY - (c / 2 + 1), 0), 
                             null);                
            }
        }
        if (currentCameraY < lastCameraY) {
            print("Going down!");
            for (int x = 0; x < myspace.GetWidth(); x++) {
                stuff.SetTile(new Vector3Int(x - (myspace.GetWidth() / 2) - 1, (int)lastCameraY - c / 2 - 1, 0), 
                               myspace.GetSprite(x, (int)lastCameraY + c / 2 - 1));
                stuff.SetTile(new Vector3Int(x - (myspace.GetWidth() / 2) - 1, (int)lastCameraY + c / 2, 0), 
                             null);  
            }
        }
        lastCameraY = currentCameraY;
    }
}

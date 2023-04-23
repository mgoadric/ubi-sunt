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
    }

    // Update is called once per frame
    void Update()
    {
        
        float currentCameraY = Mathf.Floor(cameraTransform.position.y);
        if (currentCameraY > lastCameraY) {
            for (int i = 0; i < myspace.GetWidth(); i++) {
                stuff.SetTile(new Vector3Int(i - (myspace.GetWidth() / 2) - 1,
                                 (int)currentCameraY + 5, 
                                 0), 
                             myspace.GetSprite(i, (int)currentCameraY + 3));
                stuff.SetTile(new Vector3Int(i - (myspace.GetWidth() / 2) - 1,
                                 (int)currentCameraY - 4, 
                                 0), 
                             null);                
            }
        }
        if (currentCameraY < lastCameraY) {
            for (int i = 0; i < myspace.GetWidth(); i++) {
                stuff.SetTile( new Vector3Int(i - (myspace.GetWidth() / 2) - 1, 
                (int)currentCameraY - 5, 0), myspace.GetSprite(i, (int)currentCameraY + 3));
                stuff.SetTile(new Vector3Int(i - (myspace.GetWidth() / 2) - 1,
                                 (int)currentCameraY + 4, 
                                 0), 
                             null);  
            }
        }
        lastCameraY = Mathf.Floor(cameraTransform.position.y);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PodRotation : MonoBehaviour
{

    private Pod pod;
    private Tilemap background;
    private Tilemap walls;

    public TileBase floor;
    public TileBase wall;

    private Transform cameraTransform;
    private float lastCameraY;

    // Start is called before the first frame update
    void Start()
    {
        pod = new Pod(9, 12);

        cameraTransform = Camera.main.transform;
        lastCameraY = Mathf.Floor(cameraTransform.position.y);
        background = transform.Find("Background").gameObject.GetComponent<Tilemap>();
        walls = transform.Find("Walls").gameObject.GetComponent<Tilemap>();
        int w = pod.GetWidth();
        int c = pod.GetCircumference();
        for (int y = 0; y < c; y++) {
            for (int x = 0; x < w; x++) {
                background.SetTile(new Vector3Int(x, y, 0), floor);
            }
        }

        // Walls
        for (int y = 0; y < c; y++) {
            walls.SetTile(new Vector3Int(-1, y, 0), wall);
            walls.SetTile(new Vector3Int(w, y, 0), wall);
        }

        // Entrances

    }

    // Update is called once per frame
    void Update()
    {
        
        float currentCameraY = Mathf.Floor(cameraTransform.position.y);
        int c = pod.GetCircumference();
        int up = (int)lastCameraY + c / 2;
        int down = (int)lastCameraY - c / 2;

        if (currentCameraY > lastCameraY) {
            print("Going up!");
            for (int x = 0; x < pod.GetWidth(); x++) {
                background.SetTile(new Vector3Int(x, up, 0), floor);
                background.SetTile(new Vector3Int(x, down, 0), null);                
            }
            walls.SetTile(new Vector3Int(-1, up, 0), wall);
            walls.SetTile(new Vector3Int(pod.GetWidth(), up, 0), wall);
            walls.SetTile(new Vector3Int(-1, down, 0), null);
            walls.SetTile(new Vector3Int(pod.GetWidth(), down, 0), null);
            
        }
        if (currentCameraY < lastCameraY) {
            print("Going down!");
            for (int x = 0; x < pod.GetWidth(); x++) {
                background.SetTile(new Vector3Int(x, up, 0), null);  
                background.SetTile(new Vector3Int(x, down, 0), floor);
            }
            walls.SetTile(new Vector3Int(-1, up, 0), null);
            walls.SetTile(new Vector3Int(pod.GetWidth(), up, 0), null);
            walls.SetTile(new Vector3Int(-1, down, 0), wall);
            walls.SetTile(new Vector3Int(pod.GetWidth(), down, 0), wall);        }

        lastCameraY = currentCameraY;
    }
}

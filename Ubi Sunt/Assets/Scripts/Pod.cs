using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class Pod
{

    public TileBase[,] storage;

    public TileBase background;

    public Pod(int width, int circumference, TileBase defaultSprite) 
    {
        storage = new TileBase[width, circumference];
        background = defaultSprite;
    }

    public TileBase GetSprite(int x, int y) 
    {
        int cy = RealMod(y, storage.GetLength(1));
        if (storage[x, cy] != null) {
            return storage[x, cy];
        }
        return background;
    }

    private int RealMod(int x, int m) {
        return (x%m + m)%m;
    }

    public int GetWidth() {
        return storage.GetLength(0);
    }

    public int GetCircumference() {
        return storage.GetLength(1);
    }
}

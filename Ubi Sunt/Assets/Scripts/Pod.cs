using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class Pod
{

    public GameObject[,] storage;

    public Pod(int width, int circumference) 
    {
        storage = new GameObject[width, circumference];
    }

    public bool Set(int x, int y, GameObject go) {
        int cy = RealMod(y, GetCircumference());
        if (storage[x, cy] == null) {
            storage[x, cy] = go;
            return true;
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
            GameObject go = storage[x, cy];
            storage[x, cy] = null;
            return go;
        }
        return null;
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

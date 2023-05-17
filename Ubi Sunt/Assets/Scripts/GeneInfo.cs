using System.Collections;
using UnityEngine;

public class Genetics {

    public string name;

    public float waterR;

    public float waterT;

    public float lightR;

    public float lightT;

    public float tempR;

    public float tempT;

    public int timeToGrow;

    public int numFruit;


    public Genetics(string name, float waterRequirements, float waterThreshold,
                          float lightRequirements, float lightThreshold,
                          float tempRequirements, float tempThreshold,
                          int timeToGrow, int numFruit) {
        this.name = name;
        this.waterR = waterRequirements;
        this.waterT = waterThreshold;
        this.lightR = lightRequirements;
        this.lightT = lightThreshold;
        this.tempR = tempRequirements;
        this.tempT = tempThreshold;
        this.timeToGrow = timeToGrow;
        this.numFruit = numFruit;
    }

    public string WaterText() {
        if (waterR < 2) {
            return "ARID";
        } else if (waterR > 8) {
            return "HUMID";
        } else {
            return "MED";
        }
    }
    public string LightText() {
        if (lightR <= 5) {
            return "LOW";
        } else if (lightR > 10) {
            return "PART";
        } else {
            return "FULL";
        }
    }
    public string TempText() {
        if (tempR < -10) {
            return "COLD";
        } else if (tempR > 10) {
            return "HOT";
        } else {
            return "MODERATE";
        }
    }

    public bool Comfortable(float water, float light, float temp) {
        Debug.Log("w = " + water + ", l = " + light + ", t = " + temp);
        if (Mathf.Abs(water - waterR) <= waterT &&
            Mathf.Abs(light - lightR) <= lightT &&
            Mathf.Abs(temp - tempR) <= tempT) {
            return true;
        } else {
            return false;
        }
    }

    public Color WaterColor(float water) {
        if (Mathf.Abs(water - waterR) < waterT) {
            return new Color(0, 1, 0);
        } else {
            return new Color(1, 0, 0);
        }  
    }

    public Color LightColor(float light) {
        if (Mathf.Abs(light - lightR) < lightT) {
            return new Color(0, 1, 0);
        } else {
            return new Color(1, 0, 0);
        }
    }

    public Color TempColor(float temp) {
        if (Mathf.Abs(temp - tempR) < tempT) {
            return new Color(0, 1, 0);
        } else {
            return new Color(1, 0, 0);
        }
    }

    public Genetics Recombine(Genetics other) {
        // TODO
        return this;
    }
}
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

    private Color good = new Color(0, 1, 0);
    private Color ok = new Color(1, 1, 0);
    private Color bad = new Color(1, 0, 0);


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
        float percent = Mathf.Min(10, Mathf.Abs(water - waterR)) / 10;
        float line = waterT / 10;

        if (percent <= line) {
            return Color.Lerp(good, ok, percent * (1 / line));
        } else {
            return Color.Lerp(ok, bad, (percent - 0.5f) * (1 / line));
        }
    }

    public Color LightColor(float light) {
        float percent = Mathf.Min(10, Mathf.Abs(light - lightR)) / 10;
        float line = waterT / 10;

        if (percent <= line) {
            return Color.Lerp(good, ok, percent * (1 / line));
        } else {
            return Color.Lerp(ok, bad, (percent - 0.5f) * (1 / line));
        }
    }

    public Color TempColor(float temp) {
        float percent = Mathf.Min(10, Mathf.Abs(temp - tempR)) / 10;
        float line = waterT / 10;

        if (percent <= line) {
            return Color.Lerp(good, ok, percent * (1 / line));
        } else {
            return Color.Lerp(ok, bad, (percent - 0.5f) * (1 / line));
        }
    }

    public Genetics Recombine(Genetics other) {
        // TODO
        return this;
    }
}
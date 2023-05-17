using System.Collections;
using UnityEngine;

public struct GeneInfo {

    public string name;

    public float waterRequirements;

    public float waterThreshold;

    public float lightRequirements;

    public float lightThreshold;

    public float tempRequirements;

    public float tempThreshold;


    public GeneInfo(string name, float waterRequirements, float waterThreshold,
                          float lightRequirements, float lightThreshold,
                          float tempRequirements, float tempThreshold) {
        this.name = name;
        this.waterRequirements = waterRequirements;
        this.waterThreshold = waterThreshold;
        this.lightRequirements = lightRequirements;
        this.lightThreshold = lightThreshold;
        this.tempRequirements = tempRequirements;
        this.tempThreshold = tempThreshold;
    }

    public string WaterText() {
        if (waterRequirements < 2) {
            return "ARID";
        } else if (waterRequirements > 8) {
            return "HUMID";
        } else {
            return "MED";
        }
    }
    public string LightText() {
        if (lightRequirements <= 5) {
            return "LOW";
        } else if (lightRequirements > 10) {
            return "PART";
        } else {
            return "FULL";
        }
    }
    public string TempText() {
        if (tempRequirements < -10) {
            return "COLD";
        } else if (tempRequirements > 10) {
            return "HOT";
        } else {
            return "MODERATE";
        }
    }

    public bool Comfortable(float water, float light, float temp) {
        Debug.Log("w = " + water + ", l = " + light + ", t = " + temp);
        if (Mathf.Abs(water - waterRequirements) < waterThreshold &&
            Mathf.Abs(light - lightRequirements) < lightThreshold &&
            Mathf.Abs(temp - tempRequirements) < tempThreshold) {
            return true;
        } else {
            return false;
        }
    }

    public Color WaterColor(float water) {
        if (Mathf.Abs(water - waterRequirements) < waterThreshold) {
            return new Color(0, 1, 0);
        } else {
            return new Color(1, 0, 0);
        }  
    }

    public Color LightColor(float light) {
        if (Mathf.Abs(light - lightRequirements) < lightThreshold) {
            return new Color(0, 1, 0);
        } else {
            return new Color(1, 0, 0);
        }
    }

    public Color TempColor(float temp) {
        if (Mathf.Abs(temp - tempRequirements) < tempThreshold) {
            return new Color(0, 1, 0);
        } else {
            return new Color(1, 0, 0);
        }
    }

    public GeneInfo Recombine(GeneInfo other) {
        // TODO
        return this;
    }
}
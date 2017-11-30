using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {

    public float initTime;
    public Color[] colorsTime;
    public Color[] colorsWeather;
    public float speed;
    public GameObject sun;
    public float transYMinSun = - 2.3f;
    public float transYMaxSun = 6f;
    public float transYToggleSun = 0.38f;
    public GameObject moon;
    public GameObject[] mountains;
    public Color[] mountainColors;
    public GameObject[] foregroundObjects;
    public Color[] foregroundObjectColors;
    public float transYMinMoon = - 2.3f;
    public float transYMaxMoon = 6f;
    public float transYToggleMoon = 0.13f;
    public GameObject background;
    public GameObject stars;

    public static float timeOfDay;
    private int day;
    private bool hasSunRisen;
    private bool hasSunSet;
    private bool hasMoonRisen;
    private bool hasMoonSet;
    private float fracSun;
    private float yDiffSun;
    private float ySun;
    private float fracMoon;
    private float yDiffMoon;
    private float yMoon;
    private float fracColor;
    private Color colorTop;
    private Color colorBottom;
    private float fracObjects;
    private Color colorObjects;

    // Use this for initialization
    void Start () {
        timeOfDay = initTime * 60 * 60;
        yDiffSun = transYMaxSun - transYMinSun;
        yDiffMoon = transYMaxMoon - transYMinMoon;
        day = 0;
        if (timeOfDay > 21600 && timeOfDay <= 43200)
        {
            fracSun = (timeOfDay - 21600) / 21600;
            ySun = Mathf.Lerp(transYMinSun, transYMaxSun, fracSun);
            if (ySun >= transYToggleSun)
            {
                hasSunRisen = true;
                sun.GetComponentInChildren<Light>().intensity = 1;
            }
            else
            {
                hasSunRisen = false;
                sun.GetComponentInChildren<Light>().intensity = 0;
            }
            hasSunSet = false;
            sun.GetComponent<Transform>().position = new Vector3(sun.GetComponent<Transform>().position.x, ySun, sun.GetComponent<Transform>().position.z);
            hasMoonRisen = false;
            hasMoonSet = false;
            moon.GetComponent<Transform>().position = new Vector3(moon.GetComponent<Transform>().position.x, transYMinMoon, moon.GetComponent<Transform>().position.z);
        } else if (timeOfDay > 43200 && timeOfDay <= 64800)
        {
            fracSun = (timeOfDay - 43200) / 21600;
            ySun = Mathf.Lerp(transYMaxSun, transYMinSun, fracSun);
            hasSunRisen = true;
            if (ySun >= transYToggleSun)
            {
                hasSunSet = false;
                sun.GetComponentInChildren<Light>().intensity = 1;
            }
            else
            {
                hasSunSet = true;
                sun.GetComponentInChildren<Light>().intensity = 0;
            }
            sun.GetComponent<Transform>().position = new Vector3(sun.GetComponent<Transform>().position.x, ySun, sun.GetComponent<Transform>().position.z);
            hasMoonSet = false;
            hasMoonRisen = false;
            moon.GetComponent<Transform>().position = new Vector3(moon.GetComponent<Transform>().position.x, transYMinMoon, moon.GetComponent<Transform>().position.z);
        } else if (timeOfDay <= 86400 && timeOfDay > 64800)
        {
            fracMoon = (timeOfDay - 64800) / 21600;
            yMoon = Mathf.Lerp(transYMinMoon, transYMaxMoon, fracMoon);
            if (yMoon >= transYToggleMoon)
            {
                hasMoonRisen = true;
            } else
            {
                hasMoonRisen = false;
            }
            hasMoonSet = false;
            moon.GetComponent<Transform>().position = new Vector3(moon.GetComponent<Transform>().position.x, yMoon, moon.GetComponent<Transform>().position.z);
            hasSunRisen = true;
            hasSunSet = true;
            sun.GetComponent<Transform>().position = new Vector3(sun.GetComponent<Transform>().position.x, transYMinSun, sun.GetComponent<Transform>().position.z);
            sun.GetComponentInChildren<Light>().intensity = 0;
        } else
        {
            fracMoon = timeOfDay / 21600;
            yMoon = Mathf.Lerp(transYMaxMoon, transYMinMoon, fracMoon);
            hasMoonRisen = true;
            if (yMoon >= transYToggleMoon)
            {
                hasMoonSet = false;
            }
            else
            {
                hasMoonSet = true;
            }
            moon.GetComponent<Transform>().position = new Vector3(moon.GetComponent<Transform>().position.x, yMoon, moon.GetComponent<Transform>().position.z);
            hasSunRisen = false;
            hasSunSet = false;
            sun.GetComponent<Transform>().position = new Vector3(sun.GetComponent<Transform>().position.x, transYMinSun, sun.GetComponent<Transform>().position.z);
            sun.GetComponentInChildren<Light>().intensity = 0;
        }
        if (timeOfDay > 21600 && timeOfDay <= 64800)
        {
            mountains[0].GetComponent<SpriteRenderer>().color = mountainColors[0];
            mountains[1].GetComponent<SpriteRenderer>().color = mountainColors[1];
            mountains[2].GetComponent<SpriteRenderer>().color = mountainColors[2];

            for (int i = 0; i < foregroundObjects.Length; i++)
            {
                foregroundObjects[i].GetComponent<SpriteRenderer>().color = Color.white;
            }
        } else
        {
            mountains[0].GetComponent<SpriteRenderer>().color = mountainColors[3];
            mountains[1].GetComponent<SpriteRenderer>().color = mountainColors[4];
            mountains[2].GetComponent<SpriteRenderer>().color = mountainColors[5];

            for (int i = 0; i < foregroundObjects.Length; i++)
            {
                foregroundObjects[i].GetComponent<SpriteRenderer>().color = foregroundObjectColors[0];
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        timeOfDay = timeOfDay + Time.deltaTime * speed * 100;
        Debug.Log(timeOfDay);
        if (timeOfDay > 86400)
        {
            timeOfDay = 0;
            day++;
        }
        SunMoonPosition();
        ColorChangeBackgroundWithTime();
        ColorChangeObjects();
        if (timeOfDay >= 56000)
        {
            stars.SetActive(true);
        }
        if (timeOfDay >= 29500 && timeOfDay < 56000)
        {
            stars.SetActive(false);
        }
    }

    private void SunMoonPosition()
    {
        if (timeOfDay > 21600 && timeOfDay <= 43200)
        {
            fracSun = (timeOfDay - 21600) / 21600;
            ySun = Mathf.Lerp(transYMinSun, transYMaxSun, fracSun);
            if (ySun >= transYToggleSun)
            {
                hasSunRisen = true;
                sun.GetComponentInChildren<Light>().intensity = Mathf.Lerp(0, 1, (ySun - transYToggleSun) / (transYMaxSun - transYToggleSun));
            }
            else
            {
                hasSunRisen = false;
                sun.GetComponentInChildren<Light>().intensity = 0;
            }
            hasSunSet = false;
            sun.GetComponent<Transform>().position = new Vector3(sun.GetComponent<Transform>().position.x, ySun, sun.GetComponent<Transform>().position.z);
            hasMoonRisen = false;
            hasMoonSet = false;
        }
        else if (timeOfDay > 43200 && timeOfDay <= 64800)
        {
            fracSun = (timeOfDay - 43200) / 21600;
            ySun = Mathf.Lerp(transYMaxSun, transYMinSun, fracSun);
            hasSunRisen = true;
            if (ySun >= transYToggleSun)
            {
                hasSunSet = false;
                sun.GetComponentInChildren<Light>().intensity = Mathf.Lerp(1, 0, (transYMaxSun - ySun) / (transYMaxSun - transYToggleSun));
            }
            else
            {
                hasSunSet = true;
                sun.GetComponentInChildren<Light>().intensity = 0;
            }
            sun.GetComponent<Transform>().position = new Vector3(sun.GetComponent<Transform>().position.x, ySun, sun.GetComponent<Transform>().position.z);
            hasMoonSet = false;
            hasMoonRisen = false;
        }
        else if (timeOfDay <= 86400 && timeOfDay > 64800)
        {
            fracMoon = (timeOfDay - 64800) / 21600;
            yMoon = Mathf.Lerp(transYMinMoon, transYMaxMoon, fracMoon);
            if (yMoon >= transYToggleMoon)
            {
                hasMoonRisen = true;
            }
            else
            {
                hasMoonRisen = false;
            }
            hasMoonSet = false;
            moon.GetComponent<Transform>().position = new Vector3(moon.GetComponent<Transform>().position.x, yMoon, moon.GetComponent<Transform>().position.z);
            hasSunRisen = true;
            hasSunSet = true;
        }
        else
        {
            fracMoon = timeOfDay / 21600;
            yMoon = Mathf.Lerp(transYMaxMoon, transYMinMoon, fracMoon);
            hasMoonRisen = true;
            if (yMoon >= transYToggleMoon)
            {
                hasMoonSet = false;
            }
            else
            {
                hasMoonSet = true;
            }
            moon.GetComponent<Transform>().position = new Vector3(moon.GetComponent<Transform>().position.x, yMoon, moon.GetComponent<Transform>().position.z);
            hasSunRisen = false;
            hasSunSet = false;
        }
    }

    private void ColorChangeBackgroundWithTime ()
    {
        if (timeOfDay >= 0 && timeOfDay < 10800)
        {
            fracColor = timeOfDay / 10800;
            colorTop = Color.Lerp(colorsTime[0], colorsTime[2], fracColor);
            colorBottom = Color.Lerp(colorsTime[1], colorsTime[3], fracColor);
            background.GetComponent<Renderer>().material.SetColor("_TopColor", colorTop);
            background.GetComponent<Renderer>().material.SetColor("_BottomColor", colorBottom);
        } else if (timeOfDay >= 10800 && timeOfDay < 27000)
        {
            fracColor = (timeOfDay - 10800) / 16200;
            colorTop = Color.Lerp(colorsTime[2], colorsTime[4], fracColor);
            colorBottom = Color.Lerp(colorsTime[3], colorsTime[5], fracColor);
            background.GetComponent<Renderer>().material.SetColor("_TopColor", colorTop);
            background.GetComponent<Renderer>().material.SetColor("_BottomColor", colorBottom);
        } else if (timeOfDay >= 27000 && timeOfDay < 32400)
        {
            fracColor = (timeOfDay - 27000) / 5400;
            colorTop = Color.Lerp(colorsTime[4], colorsTime[6], fracColor);
            colorBottom = Color.Lerp(colorsTime[5], colorsTime[7], fracColor);
            background.GetComponent<Renderer>().material.SetColor("_TopColor", colorTop);
            background.GetComponent<Renderer>().material.SetColor("_BottomColor", colorBottom);
        }
        else if (timeOfDay >= 32400 && timeOfDay < 43200)
        {
            fracColor = (timeOfDay - 32400) / 10800;
            colorTop = Color.Lerp(colorsTime[6], colorsTime[8], fracColor);
            colorBottom = Color.Lerp(colorsTime[7], colorsTime[9], fracColor);
            background.GetComponent<Renderer>().material.SetColor("_TopColor", colorTop);
            background.GetComponent<Renderer>().material.SetColor("_BottomColor", colorBottom);
        }
        else if (timeOfDay >= 43200 && timeOfDay < 54000)
        {
            fracColor = (timeOfDay - 43200) / 10800;
            colorTop = Color.Lerp(colorsTime[8], colorsTime[10], fracColor);
            colorBottom = Color.Lerp(colorsTime[9], colorsTime[11], fracColor);
            background.GetComponent<Renderer>().material.SetColor("_TopColor", colorTop);
            background.GetComponent<Renderer>().material.SetColor("_BottomColor", colorBottom);
        }
        else if (timeOfDay >= 54000 && timeOfDay < 62000)
        {
            fracColor = (timeOfDay - 54000) / 8000;
            colorTop = Color.Lerp(colorsTime[10], colorsTime[12], fracColor);
            colorBottom = Color.Lerp(colorsTime[11], colorsTime[13], fracColor);
            background.GetComponent<Renderer>().material.SetColor("_TopColor", colorTop);
            background.GetComponent<Renderer>().material.SetColor("_BottomColor", colorBottom);
        }
        else if (timeOfDay >= 62000 && timeOfDay < 75600)
        {
            fracColor = (timeOfDay - 62000) / 13600;
            colorTop = Color.Lerp(colorsTime[12], colorsTime[14], fracColor);
            colorBottom = Color.Lerp(colorsTime[13], colorsTime[15], fracColor);
            background.GetComponent<Renderer>().material.SetColor("_TopColor", colorTop);
            background.GetComponent<Renderer>().material.SetColor("_BottomColor", colorBottom);
        }
        else if (timeOfDay >= 75600 && timeOfDay < 86400)
        {
            fracColor = (timeOfDay - 75600) / 10800;
            colorTop = Color.Lerp(colorsTime[14], colorsTime[16], fracColor);
            colorBottom = Color.Lerp(colorsTime[15], colorsTime[17], fracColor);
            background.GetComponent<Renderer>().material.SetColor("_TopColor", colorTop);
            background.GetComponent<Renderer>().material.SetColor("_BottomColor", colorBottom);
        }
    }

    private void ColorChangeObjects()
    {   
        if (timeOfDay >= 57000 && timeOfDay < 62000)
        {
            fracObjects = (timeOfDay - 57000) / 5000;
            colorObjects = Color.Lerp(mountainColors[0], mountainColors[3], fracObjects);
            mountains[0].GetComponent<SpriteRenderer>().color = colorObjects;
            colorObjects = Color.Lerp(mountainColors[1], mountainColors[4], fracObjects);
            mountains[1].GetComponent<SpriteRenderer>().color = colorObjects;
            colorObjects = Color.Lerp(mountainColors[2], mountainColors[5], fracObjects);
            mountains[2].GetComponent<SpriteRenderer>().color = colorObjects;

            for (int i = 0; i < foregroundObjects.Length; i++)
            {
                colorObjects = Color.Lerp(Color.white, foregroundObjectColors[0], fracObjects);
                foregroundObjects[i].GetComponent<SpriteRenderer>().color = colorObjects;
            }
        }
        if (timeOfDay >= 23000 && timeOfDay < 29000)
        {
            fracObjects = (timeOfDay - 23000) / 6000;
            colorObjects = Color.Lerp(mountainColors[3], mountainColors[0], fracObjects);
            mountains[0].GetComponent<SpriteRenderer>().color = colorObjects;
            colorObjects = Color.Lerp(mountainColors[4], mountainColors[1], fracObjects);
            mountains[1].GetComponent<SpriteRenderer>().color = colorObjects;
            colorObjects = Color.Lerp(mountainColors[5], mountainColors[2], fracObjects);
            mountains[2].GetComponent<SpriteRenderer>().color = colorObjects;

            for (int i = 0; i < foregroundObjects.Length; i++)
            {
                colorObjects = Color.Lerp(foregroundObjectColors[0], Color.white, fracObjects);
                foregroundObjects[i].GetComponent<SpriteRenderer>().color = colorObjects;
            }
        }

    }
}

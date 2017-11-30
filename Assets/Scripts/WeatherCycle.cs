using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherCycle : MonoBehaviour {

    public Color[] backgroundColors;
    public Color[] mountainColors;
    public Color[] foregroundObjectColors;
    public GameObject fog;
    public GameObject rain;
    public GameObject lightning;
    public GameObject snow;
    public GameObject Background;
    public GameObject stars;
    public Color fogColor;

    public GameObject background;
    public GameObject[] mountains;
    public GameObject[] foregroundObjects;

    public float timeMin;
    public float timeMax;

    public static int type;
    private float time;
    private float timeFinal;
    public static bool weatherChanging = false;
    private float frac;
    public static float initTime;
    public static float initChangeBackTime;
    private Color[] backgroundColorsCurrent;
    private Color[] mountainColorsPresent;
    private Color foregroundObjectColorsPresent;
    private Color[] backgroundColorsFinal;
    private Color[] mountainColorsFinal;
    private Color foregroundObjectColorsFinal;
    private DayNightCycle dayNightCycle;

    // Use this for initialization
    void Start() {
        time = 0;
        timeFinal = 1000 * Random.Range(timeMin, timeMax);
        weatherChanging = false;
        backgroundColorsCurrent = new Color[2];
        mountainColorsPresent = new Color[3];
        backgroundColorsFinal = new Color[2];
        mountainColorsFinal = new Color[3];
        dayNightCycle = Background.GetComponent<DayNightCycle>();
    }

    // Update is called once per frame
    void Update() {
        time = time + Time.deltaTime * 100 * 5;
        if (time >= timeFinal)
        {
            time = 0;
            timeFinal = 1000 * Random.Range(timeMin, timeMax);
            //type = (int)Random.Range(0, 2);
            type = 0;
            weatherChanging = true;
            initTime = Time.time;
            if (type == 0)
            {
                fog.SetActive(true);
                rain.SetActive(true);
            } else
            {
                snow.SetActive(true);
            }
        }
        if (weatherChanging)
        {
            WeatherChange();
        }
    }

    private void WeatherChange()
    {
        if (type == 0)
        {
            if (Time.time - initTime <= 25)
            {
                backgroundColorsCurrent[0] = background.GetComponent<Renderer>().material.GetColor("_TopColor");
                backgroundColorsCurrent[1] = background.GetComponent<Renderer>().material.GetColor("_BottomColor");
                for (int i = 0; i < 3; i++)
                {
                    mountainColorsPresent[i] = mountains[i].GetComponent<SpriteRenderer>().color;
                }
                foregroundObjectColorsPresent = foregroundObjects[0].GetComponent<SpriteRenderer>().color;
                if (DayNightCycle.timeOfDay < 12000 && DayNightCycle.timeOfDay >= 75000) //Night Time
                {
                    fog.GetComponent<ParticleSystem>().startColor = fogColor;
                    background.GetComponent<Renderer>().material.SetColor("_TopColor", Color.Lerp(backgroundColorsCurrent[0], backgroundColors[0], Time.deltaTime));
                    background.GetComponent<Renderer>().material.SetColor("_BottomColor", Color.Lerp(backgroundColorsCurrent[1], backgroundColors[1], Time.deltaTime));
                    for (int i = 0; i < 3; i++)
                    {
                        mountains[i].GetComponent<SpriteRenderer>().color = Color.Lerp(mountainColorsPresent[i], mountainColors[0], Time.deltaTime);
                    }
                    for (int i = 0; i < foregroundObjects.Length; i++)
                    {
                        foregroundObjects[i].GetComponent<SpriteRenderer>().color = Color.Lerp(foregroundObjectColorsPresent, foregroundObjectColors[0], Time.deltaTime);
                    }
                }
                else if (DayNightCycle.timeOfDay >= 12000 && DayNightCycle.timeOfDay < 29500) //Sun Rise
                {
                    fog.GetComponent<ParticleSystem>().startColor = Color.Lerp(fog.GetComponent<ParticleSystem>().startColor, Color.white, Time.deltaTime);
                    background.GetComponent<Renderer>().material.SetColor("_TopColor", Color.Lerp(backgroundColorsCurrent[0], backgroundColors[2], Time.deltaTime));
                    background.GetComponent<Renderer>().material.SetColor("_BottomColor", Color.Lerp(backgroundColorsCurrent[1], backgroundColors[3], Time.deltaTime));
                    for (int i = 0; i < 3; i++)
                    {
                        mountains[i].GetComponent<SpriteRenderer>().color = Color.Lerp(mountainColorsPresent[i], mountainColors[1], Time.deltaTime);
                    }
                    for (int i = 0; i < foregroundObjects.Length; i++)
                    {
                        foregroundObjects[i].GetComponent<SpriteRenderer>().color = Color.Lerp(foregroundObjectColorsPresent, foregroundObjectColors[1], Time.deltaTime);
                    }
                }
                else if (DayNightCycle.timeOfDay >= 29500 && DayNightCycle.timeOfDay < 56000)//Day Time
                {
                    fog.GetComponent<ParticleSystem>().startColor = Color.white;
                    background.GetComponent<Renderer>().material.SetColor("_TopColor", Color.Lerp(backgroundColorsCurrent[0], backgroundColors[4], Time.deltaTime));
                    background.GetComponent<Renderer>().material.SetColor("_BottomColor", Color.Lerp(backgroundColorsCurrent[1], backgroundColors[5], Time.deltaTime));
                    for (int i = 0; i < 3; i++)
                    {
                        mountains[i].GetComponent<SpriteRenderer>().color = Color.Lerp(mountainColorsPresent[i], mountainColors[2], Time.deltaTime);
                    }
                    for (int i = 0; i < foregroundObjects.Length; i++)
                    {
                        foregroundObjects[i].GetComponent<SpriteRenderer>().color = Color.Lerp(foregroundObjectColorsPresent, foregroundObjectColors[2], Time.deltaTime);
                    }
                }
                else if (DayNightCycle.timeOfDay >= 56000 && DayNightCycle.timeOfDay < 75000)//Sun set
                {
                    fog.GetComponent<ParticleSystem>().startColor = Color.Lerp(fog.GetComponent<ParticleSystem>().startColor, fogColor, Time.deltaTime);
                    background.GetComponent<Renderer>().material.SetColor("_TopColor", Color.Lerp(backgroundColorsCurrent[0], backgroundColors[6], Time.deltaTime));
                    background.GetComponent<Renderer>().material.SetColor("_BottomColor", Color.Lerp(backgroundColorsCurrent[1], backgroundColors[7], Time.deltaTime));
                    for (int i = 0; i < 3; i++)
                    {
                        mountains[i].GetComponent<SpriteRenderer>().color = Color.Lerp(mountainColorsPresent[i], mountainColors[3], Time.deltaTime);
                    }
                    for (int i = 0; i < foregroundObjects.Length; i++)
                    {
                        foregroundObjects[i].GetComponent<SpriteRenderer>().color = Color.Lerp(foregroundObjectColorsPresent, foregroundObjectColors[3], Time.deltaTime);
                    }
                }
                initChangeBackTime = DayNightCycle.timeOfDay;
                backgroundColorsFinal[0] = dayNightCycle.GetBackgroundColorTop(initChangeBackTime);
                backgroundColorsFinal[1] = dayNightCycle.GetBackgroundColorBottom(initChangeBackTime);
                for (int i = 0; i < 3; i++)
                {
                    mountainColorsFinal[i] = dayNightCycle.GetMountainColor(initChangeBackTime, i);
                }
                foregroundObjectColorsFinal = dayNightCycle.GetForegroundObjectColor(initChangeBackTime);
            }
            else if (Time.time - initTime > 25 && Time.time - initTime <= 30)
            {
                frac = (DayNightCycle.timeOfDay - initChangeBackTime) / 2500;
                background.GetComponent<Renderer>().material.SetColor("_TopColor", Color.Lerp(backgroundColorsCurrent[0], backgroundColorsFinal[0], frac));
                background.GetComponent<Renderer>().material.SetColor("_BottomColor", Color.Lerp(backgroundColorsCurrent[1], backgroundColorsFinal[1], frac));

                for (int i = 0; i < 3; i++)
                {
                    mountains[i].GetComponent<SpriteRenderer>().color = Color.Lerp(mountainColorsPresent[i], mountainColorsFinal[i], frac);
                }

                for (int i = 0; i < foregroundObjects.Length; i++)
                {
                    foregroundObjects[i].GetComponent<SpriteRenderer>().color = Color.Lerp(foregroundObjectColorsPresent, foregroundObjectColorsFinal, frac);
                }
            } else
            {
                weatherChanging = false;
                fog.SetActive(false);
                rain.SetActive(false);
            }
        } else
        {
            if (Time.time - initTime <= 25)
            {
                backgroundColorsCurrent[0] = background.GetComponent<Renderer>().material.GetColor("_TopColor");
                backgroundColorsCurrent[1] = background.GetComponent<Renderer>().material.GetColor("_BottomColor");
                if (DayNightCycle.timeOfDay < 12000 && DayNightCycle.timeOfDay >= 75000) //Night Time
                {
                    background.GetComponent<Renderer>().material.SetColor("_TopColor", Color.Lerp(backgroundColorsCurrent[0], backgroundColors[8], Time.deltaTime));
                    background.GetComponent<Renderer>().material.SetColor("_BottomColor", Color.Lerp(backgroundColorsCurrent[1], backgroundColors[9], Time.deltaTime));
                }
                else if (DayNightCycle.timeOfDay >= 12000 && DayNightCycle.timeOfDay < 29500) //Sun Rise
                {
                    background.GetComponent<Renderer>().material.SetColor("_TopColor", Color.Lerp(backgroundColorsCurrent[0], backgroundColors[10], Time.deltaTime));
                    background.GetComponent<Renderer>().material.SetColor("_BottomColor", Color.Lerp(backgroundColorsCurrent[1], backgroundColors[11], Time.deltaTime));
                }
                else if (DayNightCycle.timeOfDay >= 29500 && DayNightCycle.timeOfDay < 56000)//Day Time
                {
                    background.GetComponent<Renderer>().material.SetColor("_TopColor", Color.Lerp(backgroundColorsCurrent[0], backgroundColors[12], Time.deltaTime));
                    background.GetComponent<Renderer>().material.SetColor("_BottomColor", Color.Lerp(backgroundColorsCurrent[1], backgroundColors[13], Time.deltaTime));
                }
                else if (DayNightCycle.timeOfDay >= 56000 && DayNightCycle.timeOfDay < 75000)//Sun set
                {
                    background.GetComponent<Renderer>().material.SetColor("_TopColor", Color.Lerp(backgroundColorsCurrent[0], backgroundColors[14], Time.deltaTime));
                    background.GetComponent<Renderer>().material.SetColor("_BottomColor", Color.Lerp(backgroundColorsCurrent[1], backgroundColors[15], Time.deltaTime));
                }
                initChangeBackTime = DayNightCycle.timeOfDay;
                backgroundColorsFinal[0] = dayNightCycle.GetBackgroundColorTop(initChangeBackTime);
                backgroundColorsFinal[1] = dayNightCycle.GetBackgroundColorBottom(initChangeBackTime);
            }
            else if (Time.time - initTime > 25 && Time.time - initTime <= 30)
            {
                frac = (DayNightCycle.timeOfDay - initChangeBackTime) / 2500;
                background.GetComponent<Renderer>().material.SetColor("_TopColor", Color.Lerp(backgroundColorsCurrent[0], backgroundColorsFinal[0], frac));
                background.GetComponent<Renderer>().material.SetColor("_BottomColor", Color.Lerp(backgroundColorsCurrent[1], backgroundColorsFinal[1], frac));
            }
            else
            {
                weatherChanging = false;
                snow.SetActive(false);
            }
        }
    }

}

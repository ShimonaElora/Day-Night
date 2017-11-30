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

    public GameObject background;

    public float timeMin;
    public float timeMax;

    private int type;
    private float time;
    private float timeFinal;
    private bool weatherChanging;
    private float frac;
    private float initTime;
    private Color[] backgroundColorsCurrent;
    private Color[] mountainColorsPresent;
    private Color[] foregroundObjectColorsPresent;

	// Use this for initialization
	void Start () {
        time = 0;
        timeFinal = 1000 * Random.Range(timeMin, timeMax);
        weatherChanging = false;
	}
	
	// Update is called once per frame
	void Update () {
        time = time + Time.deltaTime * 100 * 5;
        if (time >= timeFinal)
        {
            time = 0;
            timeFinal = 1000 * Random.Range(timeMin, timeMax);
            type = (int)Random.Range(0, 2);
            weatherChanging = true;
            initTime = Time.time;
            backgroundColorsCurrent = {  }
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
            if (DayNightCycle.timeOfDay >= 29500 && DayNightCycle.timeOfDay < 56000)
            {
                frac = (Time.time - initTime) / 5;

            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour {

    public float speed;
    public Color colorNight;
    public Color colorDay;

    private float frac;
    private Color color;

    private void Start()
    {
        if (DayNightCycle.timeOfDay > 21600 && DayNightCycle.timeOfDay <= 64800)
        {
            GetComponent<SpriteRenderer>().color = colorDay;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = colorNight;
        }
    }

    // Update is called once per frame
    void Update () {
        transform.position = new Vector3(transform.position.x + 0.01f * speed, transform.position.y, transform.position.z);

        if (transform.position.x >= 12f)
        {
            Destroy(gameObject);
        }

        if (DayNightCycle.timeOfDay >= 57000 && DayNightCycle.timeOfDay < 62000)
        {
            frac = (DayNightCycle.timeOfDay - 57000) / 5000;
            color = Color.Lerp(colorDay, colorNight, frac);
            GetComponent<SpriteRenderer>().color = color;
        }
        if (DayNightCycle.timeOfDay >= 23000 && DayNightCycle.timeOfDay < 29000)
        {
            frac = (DayNightCycle.timeOfDay - 23000) / 6000;
            color = Color.Lerp(colorNight, colorDay, frac);
            GetComponent<SpriteRenderer>().color = color;
        }
    }
}

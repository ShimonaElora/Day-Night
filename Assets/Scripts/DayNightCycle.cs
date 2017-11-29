using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {

    public float initTime;
    public float[] colors;
    public float speed;
    public GameObject sun;
    public float transYMinSun = -2.3f;
    public float transYMaxSun = 6f;
    public float transYToggleSun = 0.38f;
    public GameObject moon;
    public float transYMinMoon = -2.3f;
    public float transYMaxMoon = 6f;
    public float transYToggleMoon = 0.38f;

    private float timeOfDay;
    private float day;
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

    // Use this for initialization
    void Start () {
        timeOfDay = initTime * 60 * 60;
        yDiffSun = transYMaxSun - transYMinSun;
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
        }
	}
	
	// Update is called once per frame
	void Update () {
        timeOfDay = timeOfDay + Time.deltaTime * speed * 100;
        if (timeOfDay > 21600 && timeOfDay <= 43200)
        {
            fracSun = (timeOfDay - 21600) / 21600;
            Debug.Log(fracSun);
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
        }
        else if (timeOfDay > 43200 && timeOfDay <= 64800)
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
        }
    }
}

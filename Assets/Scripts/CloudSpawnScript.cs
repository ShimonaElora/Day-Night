using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawnScript : MonoBehaviour {

    public GameObject[] cloud;
    public Transform[] spawnPoints;
    public float[] speed;
    public GameObject Cloud;
    public Sprite[] cloudSprite;
    public Color[] cloudColors;

    private bool cloudFrontGenerate;
    private bool cloudMidGenerate;
    private bool cloudBackGenerate;

    private void Start()
    {
        cloudFrontGenerate = true;
        cloudBackGenerate = true;
        cloudMidGenerate = true;
    }

    // Use this for initialization
    void Update () {
        if (cloudFrontGenerate)
        {
            cloudFrontGenerate = false;
            StartCoroutine(CloudGeneratorFront());
        }
        if (cloudBackGenerate)
        {
            cloudBackGenerate = false;
            StartCoroutine(CloudGeneratorBack());
        }
        if (cloudMidGenerate)
        {
            cloudMidGenerate = false;
            StartCoroutine(CloudMidGenerator());
        }
	}
	
    IEnumerator CloudGeneratorFront()
    {
        yield return new WaitForSeconds(5);

        GameObject cloudClone = Instantiate(
            cloud[0], 
            new Vector3(spawnPoints[0].position.x, Random.Range(spawnPoints[0].position.y, spawnPoints[1].position.y), spawnPoints[0].position.z),
            cloud[0].GetComponent<Transform>().rotation
        );
        cloudClone.GetComponent<SpriteRenderer>().sprite = cloudSprite[Random.Range(0, 5)];
        cloudClone.GetComponent<Transform>().parent = Cloud.GetComponent<Transform>();
        cloudClone.GetComponent<CloudScript>().speed = speed[0];
        cloudClone.GetComponent<CloudScript>().colorDay = cloudColors[0];
        cloudClone.GetComponent<CloudScript>().colorNight = cloudColors[3];

        yield return new WaitForSeconds(20);
        cloudFrontGenerate = true;
    }

    IEnumerator CloudGeneratorBack()
    {
        yield return new WaitForSeconds(1);

        GameObject cloudClone = Instantiate(
            cloud[1],
            new Vector3(spawnPoints[0].position.x, Random.Range(spawnPoints[0].position.y, spawnPoints[1].position.y), spawnPoints[0].position.z),
            cloud[1].GetComponent<Transform>().rotation
        );
        cloudClone.GetComponent<SpriteRenderer>().sprite = cloudSprite[Random.Range(0, 5)];
        cloudClone.GetComponent<Transform>().parent = Cloud.GetComponent<Transform>();
        cloudClone.GetComponent<CloudScript>().speed = speed[1];
        cloudClone.GetComponent<CloudScript>().colorDay = cloudColors[1];
        cloudClone.GetComponent<CloudScript>().colorNight = cloudColors[4];

        yield return new WaitForSeconds(50);
        cloudBackGenerate = true;
    }

    IEnumerator CloudMidGenerator()
    {
        yield return new WaitForSeconds(3);

        GameObject cloudClone = Instantiate(
            cloud[2],
            new Vector3(spawnPoints[0].position.x, Random.Range(spawnPoints[0].position.y, spawnPoints[1].position.y), spawnPoints[0].position.z),
            cloud[2].GetComponent<Transform>().rotation
        );
        cloudClone.GetComponent<SpriteRenderer>().sprite = cloudSprite[Random.Range(0, 5)];
        cloudClone.GetComponent<Transform>().parent = Cloud.GetComponent<Transform>();
        cloudClone.GetComponent<CloudScript>().speed = speed[2];
        cloudClone.GetComponent<CloudScript>().colorDay = cloudColors[2];
        cloudClone.GetComponent<CloudScript>().colorNight = cloudColors[5];

        yield return new WaitForSeconds(30);
        cloudMidGenerate = true;
    }
}

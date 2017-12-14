using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public List<GameObject> objectsToSpawn;
    public List<float> objectsSpawnDelays;
    public List<float> spawnDelaysReductors;
    public List<float> reductorsFrequencies;
    BoxCollider2D box;
    public float spawnRotation;
    float a, b, x;
    float xMin, xMax;
    // Use this for initialization

    void Start ()
    {
        box = gameObject.GetComponent<BoxCollider2D>();
        a = Mathf.Tan(Mathf.Deg2Rad * (90 + box.transform.rotation.eulerAngles.z));
        b = box.bounds.center.y - a * box.bounds.center.x;
        xMin = box.bounds.center.x - box.bounds.extents.x;
        xMax = box.bounds.center.x + box.bounds.extents.x;
        for (int i = 0; i < objectsToSpawn.Count; i++)
            StartCoroutine("Spawning", i);
    }

    Vector3 getRandomPoint()
    {
        x = Random.Range(xMin, xMax);
        return new Vector3(x, a * x + b);
    }

    private void Update()
    {
        //Debug.Log("time: " + Time.timeSinceLevelLoad);
        for (int i = 0; i < objectsToSpawn.Count; i++)
        {
            if (Time.timeSinceLevelLoad % reductorsFrequencies[i] <= 0.01f)
                objectsSpawnDelays[i] -= spawnDelaysReductors[i];
        }
        
    }

    IEnumerator Spawning(int selected)
    {
        while(true)
        {
            Quaternion q = Quaternion.identity;
            q.eulerAngles = new Vector3(0, 0, spawnRotation);
            //int selected = Random.Range(0, objectsToSpawn.Count);
            Instantiate(objectsToSpawn[selected], getRandomPoint(), q);
            yield return new WaitForSeconds(objectsSpawnDelays[selected]);
        }
    }
}

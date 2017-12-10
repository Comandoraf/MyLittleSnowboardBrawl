using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public List<GameObject> objectsToSpawn;
    
    BoxCollider2D box;
    public float spawnDelay;
    public float spawnRotation;
    float a, b, x;
    float xMin, xMax;
    // Use this for initialization

    void Start () {
        box = gameObject.GetComponent<BoxCollider2D>();
        a = -Mathf.Tan(Mathf.Deg2Rad * box.transform.rotation.eulerAngles.z);
        b = box.bounds.center.y - a * box.bounds.center.x;
        xMin = box.bounds.center.x - box.bounds.extents.x;
        xMax = box.bounds.center.x + box.bounds.extents.x;
        StartCoroutine("Spawning");
    }

    Vector3 getRandomPoint()
    {
        x = Random.Range(xMin, xMax);
        return new Vector3(x, a * x + b);
    }
    private void Update()
    {
        Debug.Log("time: " + Time.timeSinceLevelLoad);
        if (Time.timeSinceLevelLoad % 10.0f <= 0.01f)
            spawnDelay -= 0.01f;
    }
    IEnumerator Spawning()
    {
        while(true)
        {
            Quaternion q = Quaternion.identity;
            q.eulerAngles = new Vector3(0, 0, spawnRotation);
            Instantiate(objectsToSpawn[Random.Range(0, objectsToSpawn.Count)], getRandomPoint(), q);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}

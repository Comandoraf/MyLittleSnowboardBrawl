using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {

    public Vector2 direction;
    public float minSpeed;
    public float maxSpeed;
    float speed;
	// Use this for initialization
	void Start () {
        speed = Random.Range(minSpeed, maxSpeed);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        gameObject.transform.position += new Vector3(direction.x * speed / direction.magnitude, direction.y * speed / direction.magnitude);
	}
}

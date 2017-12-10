using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBallController : MonoBehaviour {

    [HideInInspector]
    public Vector2 moveVector;
    public ParticleSystem snow;
    public float speed;

    [HideInInspector]
    public PlayerController parent;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, 2f);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        gameObject.transform.position += new Vector3(moveVector.x * speed / moveVector.magnitude, moveVector.y * speed / moveVector.magnitude);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Instantiate(snow, collision.transform);
        if (collision.collider.tag == "Enemy")
        {
            parent.addPoints(10);
            collision.gameObject.GetComponent<Renderer>().enabled = false;
            collision.gameObject.GetComponent<EdgeCollider2D>().enabled = false;
            collision.gameObject.GetComponentInChildren<ParticleSystem>().Stop();
        }
        Destroy(gameObject);
    }
}

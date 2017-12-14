using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBallController : MonoBehaviour {

    [HideInInspector]
    public Vector2 moveVector;
    public ParticleSystem snow;
    public GameObject PointsText;
    public float speed;
    public AudioSource hitSound;
    public AudioSource enemyHitSound;
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
        AudioSource soundObject;
        if (collision.collider.tag == "Enemy")
        {
            if (collision.collider.name.StartsWith("Skier"))
            {
                Instantiate(snow, collision.transform.position, Quaternion.identity);
                GameObject go = Instantiate(PointsText, collision.transform.position, Quaternion.Euler(0, 0, -30.0f));
                Destroy(go, 0.5f);
                parent.addPoints(10);
                collision.gameObject.GetComponent<Renderer>().enabled = false;
                collision.gameObject.GetComponent<EdgeCollider2D>().enabled = false;
                collision.gameObject.GetComponentInChildren<ParticleSystem>().Stop();
                soundObject = Instantiate(enemyHitSound);
                Destroy(soundObject, soundObject.clip.length);
                Destroy(gameObject);
            }
        }
        else
        {
            Instantiate(snow, collision.transform);
            soundObject = Instantiate(hitSound, collision.transform);
            Destroy(soundObject, soundObject.clip.length);
            Destroy(gameObject);
        }
        //Destroy(gameObject);
    }
}

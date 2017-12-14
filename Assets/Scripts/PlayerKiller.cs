using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKiller : MonoBehaviour
{
    public string textToDisplay;
    public AudioSource hitSound;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource soundObject;
        if (collision.collider.tag == "Player")
        {
            if (tag == "Enemy")
            {
                GetComponent<EdgeCollider2D>().enabled = false;
                collision.collider.GetComponent<PlayerController>().PrintMessageForPlayer(textToDisplay);
                if(name.StartsWith("Skier1"))
                    collision.collider.GetComponent<PlayerController>().Connect(GetComponent<Rigidbody2D>());
                else if (name.StartsWith("skierBig"))
                    collision.collider.GetComponent<PlayerController>().KillPlayer(textToDisplay);
            }
            else
            {
                collision.collider.GetComponent<PlayerController>().KillPlayer(textToDisplay);
                soundObject = Instantiate(hitSound);
                Destroy(soundObject, soundObject.clip.length);
            }
        }
        else if (collision.collider.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }

    }

}

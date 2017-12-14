using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollisionManager : MonoBehaviour
{

    public string textToDisplay;
    public AudioSource PlayerHitSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource soundObject;
        //if player hit obstacle
        if (collision.collider.tag == "Player")
        {
            collision.collider.GetComponent<PlayerController>().KillPlayer(textToDisplay);
            //play sound and destory it after playback finished
            soundObject = Instantiate(PlayerHitSound);
            Destroy(soundObject, soundObject.clip.length);
        }
        // if enemy hit obstacle
        else if (collision.collider.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }

    }
}

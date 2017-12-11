using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKiller : MonoBehaviour
{
    public string textToDisplay;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (tag == "Enemy")
            {
                GetComponent<EdgeCollider2D>().enabled = false;
                collision.collider.GetComponent<PlayerController>().Connect(GetComponent<Rigidbody2D>());
                collision.collider.GetComponent<PlayerController>().PrintMessageForPlayer(textToDisplay);
            }
            else
            {
                collision.collider.GetComponent<PlayerController>().KillPlayer(textToDisplay);
            }
        }
        else if (collision.collider.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }

}

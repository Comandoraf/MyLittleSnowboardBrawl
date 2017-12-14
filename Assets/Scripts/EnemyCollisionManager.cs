using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionManager : MonoBehaviour
{
    public string textToDisplay;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            GetComponent<EdgeCollider2D>().enabled = false;
            collision.collider.GetComponent<PlayerController>().PrintMessageForPlayer(textToDisplay);
            if (name.StartsWith("Skier1"))
                collision.collider.GetComponent<PlayerController>().Connect(GetComponent<Rigidbody2D>());
            else if (name.StartsWith("skierBig"))
                collision.collider.GetComponent<PlayerController>().KillPlayer(textToDisplay);
        }

    }

}

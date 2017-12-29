using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionManager : MonoBehaviour
{
    public string textToDisplay;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if enemy hits player
        if (collision.collider.tag == "Player")
        {
            //print message for player
            collision.collider.GetComponent<PlayerController>().PrintMessageForPlayer(textToDisplay);
            //connect player and skier
            if (name.StartsWith("Skier1"))
                collision.collider.GetComponent<PlayerController>().Connect(GetComponent<Rigidbody2D>());
            //kill player
            else if (name.StartsWith("skierBig"))
                collision.collider.GetComponent<PlayerController>().KillPlayer(textToDisplay);
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float loseControlDelay;
    public float shootDelay;
    public GameObject snowBallPrefab;
    public Text pointsText;
    public Text feedbackText;
    public Text ammoText;

    Vector2 moveVector;
    Vector3 snowBallMoveVector;
    
    int points = 0;
    int ammo = 3;
    bool bPlayerControl = true;


    public void addPoints(int amount)
    {
        points += amount;
    }

    /// <summary>
    /// Destroy player and print message on screen
    /// </summary>
    public void KillPlayer(string message)
    {
        feedbackText.text = message;
        feedbackText.enabled = true;
        Destroy(gameObject);
    }
    /// <summary>
    /// Print message on screen for given time
    /// </summary>
    public void PrintMessageForPlayer(string message, float displayTime = 2.0f)
    {
        feedbackText.text = message;
        feedbackText.enabled = true;
        Invoke("HideText", displayTime);
    }

    void HideText()
    {
        feedbackText.enabled = false;
    }

    /// <summary>
    /// Connects player to given rigidbody
    /// </summary>
    public void Connect(Rigidbody2D rb)
    {
        DistanceJoint2D dj2d = GetComponent<DistanceJoint2D>();
        //if player isn't already connected to any rigidbody
        if (!dj2d)
        {
            dj2d = gameObject.AddComponent<DistanceJoint2D>();
            
            dj2d.autoConfigureDistance = false;
            dj2d.distance = 0;
            dj2d.connectedBody = rb;

            //disallow player controling avatar for some delay
            bPlayerControl = false;
            loseControlDelay = 3.0f;
        }
    }

	void Update () {
        //if player can control avatar
        if (bPlayerControl)
        {
            if (Input.GetKey(KeyCode.A))
            {
                moveVector.x = -1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                moveVector.x = 1;
            }
            if (Input.GetKey(KeyCode.W))
            {
                moveVector.y = 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                moveVector.y = -1;
            }
           
            //player tries shoot
            if (Input.GetMouseButtonDown(0) )
            {
                
                if(ammo > 0)
                {
                    //spawn snowball, set it move vector to mouse direction and set player as parent
                    ammo--;
                    ammoText.text = ammo.ToString();
                    GameObject sb = Instantiate(snowBallPrefab, gameObject.transform.position, Quaternion.identity);
                    snowBallMoveVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position;
                    sb.GetComponent<SnowBallController>().moveVector = snowBallMoveVector.normalized;
                    sb.GetComponent<SnowBallController>().parent = this;
                }
                else
                {
                    PrintMessageForPlayer("No ammo", 1f);
                }
                
            }
        }
        else
        {
            
            if (loseControlDelay <= 0)
            {
                //return player movement ability, destroy enemy and joint
                bPlayerControl = true;
                Destroy(GetComponent<DistanceJoint2D>().connectedBody.gameObject);
                Destroy(GetComponent<DistanceJoint2D>());
            }
            //if player hits A or D it will return him movement ability
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                loseControlDelay -= 0.3f;
            }
        }
    }

    private void FixedUpdate()
    {
        pointsText.text = "Points: " + points;
        //movement
        if (moveVector.magnitude != 0)
            gameObject.transform.position += new Vector3(moveVector.x * speed / moveVector.magnitude, moveVector.y * speed / moveVector.magnitude);
        moveVector.Set(0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //collecting ammo
        if (collision.collider.tag == "Ammo")
        {
            ammo += 3;
            ammoText.text = ammo.ToString();
            Destroy(collision.gameObject);
        }
    }
}

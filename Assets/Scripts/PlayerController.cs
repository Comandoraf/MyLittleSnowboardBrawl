using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public GameObject snowBallPrefab;
    public Text pointsText;
    public Text gameOver;

    Vector2 moveVector;
    Vector3 snowBallMoveVector;
    public float shootDelay;
    float counter = 0f;
    int points;
    bool bPlayerControl = true;
    public float loseControlDelay;

    IEnumerator GainPoints()
    {
        while (true)
        {
            points += 1;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void addPoints(int amount)
    {
        points += amount;
    }

    private void Start()
    {
        StartCoroutine(GainPoints());
    }

    public void GameOver(string message)
    {
        gameOver.text = message;
        gameOver.enabled = true;
        Destroy(gameObject);
    }

    public void Connect(Rigidbody2D rb)
    {
        DistanceJoint2D dj2d = GetComponent<DistanceJoint2D>();
        if (!dj2d)
        {
            dj2d = gameObject.AddComponent<DistanceJoint2D>();
            loseControlDelay = 3.0f;
            dj2d.autoConfigureDistance = false;
            dj2d.distance = 0;
            dj2d.connectedBody = rb;
            bPlayerControl = false;
        }
    }

	void Update () {
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

            counter += Time.deltaTime;
            if (Input.GetMouseButtonDown(0) && counter > shootDelay)
            {
                counter = 0;
                GameObject sb = Instantiate(snowBallPrefab, gameObject.transform.position, Quaternion.identity);
                snowBallMoveVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position;
                sb.GetComponent<SnowBallController>().moveVector = snowBallMoveVector.normalized;
                sb.GetComponent<SnowBallController>().parent = this;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                loseControlDelay -= 0.3f;
            if (loseControlDelay <= 0)
            {
                bPlayerControl = true;
                Destroy(GetComponent<DistanceJoint2D>().connectedBody.gameObject);
                Destroy(GetComponent<DistanceJoint2D>());
            }
        }
    }

    private void FixedUpdate()
    {
        pointsText.text = "Points: " + points;
        gameObject.transform.position += new Vector3(moveVector.x * speed, moveVector.y * speed);
        moveVector.Set(0, 0);
    }
}

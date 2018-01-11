using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MyGameManager : MonoBehaviour {
    
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public Text feedbackText;
    public Text highScoreInfoText;
    public Text pointsText;
    bool bGameOver;
    bool saveHighScore;

    void Start()
    {
        bGameOver = false;
        saveHighScore = false;
        Cursor.SetCursor(cursorTexture, new Vector2(cursorTexture.width/2, cursorTexture.height/2), cursorMode);
    }

    // Update is called once per frame
    void Update ()
    {
        if (bGameOver)
        {
            if (Input.GetKey(KeyCode.L))
            {
                SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
            }
            if (Input.GetKey(KeyCode.Escape))
            {
                GameManager.instance.PlayMusic();
                if(saveHighScore)
                {
                    GameManager.instance.currentPoints = int.Parse(pointsText.text);
                    GameManager.instance.fromGame = true;
                    SceneManager.LoadScene("HighScoreScene", LoadSceneMode.Single);
                }
                else
                {
                    SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
                }
            }
        }
    }

    /// <summary>
    /// Stops speeding up enemies spawn, checks if score is highscore and prints message to player 
    /// </summary>
    public void GameOver()
    {
        foreach(Spawner s in FindObjectsOfType<Spawner>())
        {
            for(int i = 0; i < s.objectsToSpawn.Count; i++)
            {
                s.spawnDelaysReductors[i] = 0.0f;
                s.reductorsFrequencies[i] = 0.0f;
            }
        }
        feedbackText.text += "\nPress L to fast restart without saving score";
        
        bGameOver = true;

        if (GameManager.instance.IsRecord(int.Parse(pointsText.text)))
        {
            saveHighScore = true;
            highScoreInfoText.text = "New Record!";
            feedbackText.text += "\nPress ESC to save score and get back to menu";
        }
        else if (GameManager.instance.IsHighScore(int.Parse(pointsText.text)))
        {
            saveHighScore = true;
            highScoreInfoText.text = "Highscore!";
            feedbackText.text += "\nPress ESC to save score and get back to menu";
        }
        else
        {
            highScoreInfoText.text = "Low score :(";
            feedbackText.text += "\nPress ESC to get back to menu";
        }
    }
}

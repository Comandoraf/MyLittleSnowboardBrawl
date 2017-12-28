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
    bool bGameOver;
    void Start()
    {
        bGameOver = false;
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
        }
    }

    public void GameOver()
    {
        feedbackText.text += "\nPress L to fast restart without saving score";
        feedbackText.text += "\nPress ESC to save score and get back to menu";
        bGameOver = true;
    }
}

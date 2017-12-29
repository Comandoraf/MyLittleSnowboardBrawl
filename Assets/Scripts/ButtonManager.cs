using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Cursor.SetCursor(null, new Vector2(), CursorMode.Auto);
	}

    public void onStarGameClick()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    public void onHighScoreClick()
    {
        SceneManager.LoadScene("HighScoreScene", LoadSceneMode.Single);
    }

    public void onExitGameClick()
    {
        Application.Quit();
    }
    
}

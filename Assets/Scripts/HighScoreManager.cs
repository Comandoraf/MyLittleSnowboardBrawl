using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoreManager : MonoBehaviour {

    public Text scoreText;
    public Text inputNameText;
    public Text highScoresText;
    public GameObject newHighScore;

    //sets cursor and allows player to save highscore, refreshes highscoretable
    void Start () {
        Cursor.SetCursor(null, new Vector2(), CursorMode.Auto);
        if(GameManager.instance.fromGame)
        {
            scoreText.text = GameManager.instance.currentPoints.ToString();
            newHighScore.SetActive(true);
            GameManager.instance.fromGame = false;
        }
        refreshHighScoresText();
	}

    //prints all highscores on screen
    private void refreshHighScoresText()
    {
        highScoresText.text = "";
        for(int i = 0; i < GameManager.instance.highScoresData.Count; i++)
        {
            highScoresText.text += (i + 1).ToString() + ". " + GameManager.instance.highScoresData[i].Key + "\t" + GameManager.instance.highScoresData[i].Value.ToString() + "\n";
        }
    }
    /// <summary>
    /// add new highscore to list, and save it, hide GUI, which allows to add new highscore
    /// </summary>
    public void onSaveClick()
    {
        if (inputNameText.text.Length > 0)
        {
            GameManager.instance.highScoresData.Add(new KeyValuePair<string, int>(inputNameText.text, int.Parse(scoreText.text)));
            GameManager.instance.highScoresData.Sort((x, y) => -1 * x.Value.CompareTo(y.Value));
            if (GameManager.instance.highScoresData.Count > 10)
            {
                GameManager.instance.highScoresData.RemoveAt(9);
            }
            newHighScore.SetActive(false);
            refreshHighScoresText();
            Save();
        }
    }

    private void Save()
    {
        //very ugly way to keep data, but Unity's serialization is not so good...
        for (int i = 0; i < GameManager.instance.highScoresData.Count; i++)
        {
            PlayerPrefs.SetString("Name"+(i + 1).ToString(), GameManager.instance.highScoresData[i].Key);
            PlayerPrefs.SetInt("Score"+(i + 1).ToString(), GameManager.instance.highScoresData[i].Value);
        }
    }

    public void onMenuClick()
    {
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public AudioSource musicPrefab;
    AudioSource backgroundMusic;
    public List<KeyValuePair<string, int>> highScoresData = new List<KeyValuePair<string, int>>();
    public bool fromGame = false;
    public int currentPoints = 0;
    //Awake is always called before any Start functions
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //Load highscores
        Load();
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        if (backgroundMusic == null)
        {
            backgroundMusic = Instantiate(musicPrefab);
            backgroundMusic.loop = true;
            backgroundMusic.Play();
            DontDestroyOnLoad(backgroundMusic);
        }
    }

    public void PlayMusic()
    {
        backgroundMusic.Play();
    }

    public void StopMusic()
    {
        backgroundMusic.Stop();
    }
    private void Load()
    {
        //very ugly way to keep data, but Unity's serialization is not so good...
        for (int i = 0; i < 10; i++)
        {
            string name = PlayerPrefs.GetString("Name" + (i + 1).ToString(), "Empty");
            int score = PlayerPrefs.GetInt("Score" + (i + 1).ToString(), -1);
            if (name != "Empty" && score != -1)
            {
                highScoresData.Add(new KeyValuePair<string, int>(name, score));
                highScoresData.Sort((x, y) => -1 * x.Value.CompareTo(y.Value));
            }
            else
            {
                break;
            }
        }
    }
    /// <summary>
    /// Returns true if given score can be in highscore table
    /// </summary>
    public bool IsHighScore(int points)
    {
        if (highScoresData.Count < 10)
            return true;
        else if (highScoresData[highScoresData.Count - 1].Value < points)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Returns true if given score can be save as a record
    /// </summary>
    public bool IsRecord(int points)
    {
        if (highScoresData.Count == 0)
            return true;
        else if (highScoresData[0].Value < points)
            return true;
        else
            return false;
    }
}

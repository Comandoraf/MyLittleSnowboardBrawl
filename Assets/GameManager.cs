using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // Use this for initialization
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    void Start()
    {
        Cursor.SetCursor(cursorTexture, new Vector2(cursorTexture.width/2, cursorTexture.height/2), cursorMode);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.L))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}

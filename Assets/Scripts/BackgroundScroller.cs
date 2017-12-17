using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

    public Vector2 offset;
    Sprite spr;
	// Update is called once per frame
    void Start()
    {
        spr = GetComponent<SpriteRenderer>().sprite;
        Debug.Log(spr.bounds.center.y + spr.bounds.extents.y);
    }
	void Update () {
        if (offset.x != 0.0f && (transform.position.x >= offset.x))
        {
            float defect = transform.position.x - offset.x;
            transform.position = new Vector3(0, -(offset.x - defect), 0);
        }
        if (offset.y != 0.0f && (transform.position.y >= offset.y))
        {
            float defect = transform.position.y - offset.y;
            transform.position = new Vector3(0, -(offset.y - defect), 0);
        }
	}
}

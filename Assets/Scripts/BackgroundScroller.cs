using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

    public Vector2 offset;

	void Update () {
        //because of moving object discretely there can be a defect if object jumped over offset
        float defect;
        //moving object on x axis UNUSED, but for future
        if (offset.x != 0.0f && (transform.position.x >= offset.x))
        {
            defect = transform.position.x - offset.x;
            transform.position = new Vector3(0, -(offset.x - defect), 0);
        }
        //moving object on y axis
        if (offset.y != 0.0f && (transform.position.y >= offset.y))
        {
            defect = transform.position.y - offset.y;
            transform.position = new Vector3(0, -(offset.y - defect), 0);
        }
	}
}
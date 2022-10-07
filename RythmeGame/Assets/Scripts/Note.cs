using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float _timing;
    public Define.LaneNumber _laneNumber;

    private void Start()
    {
        float yPosition = (_timing / 256.0f) * transform.parent.GetComponent<Bar>()._scrollSpeed;
        switch (_laneNumber)
        {
            case Define.LaneNumber.First:
                transform.localPosition = new Vector2(-1.8f, yPosition);
                break;
            case Define.LaneNumber.Second:
                transform.localPosition = new Vector2(-0.6f, yPosition);
                break;
            case Define.LaneNumber.Third:
                transform.localPosition = new Vector2(0.6f, yPosition);
                break;
            case Define.LaneNumber.Fourth:
                transform.localPosition = new Vector2(1.8f, yPosition);
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum LaneNumber
    {
        Unknown,
        First,
        Second,
        Third,
        Fourth,
    }

    public enum Beat
    {
        OneOverOne,
        OneOverTwo,
        OneOverFour,
        OneOverEight,
        OneOverSixty,
    }

    public enum EditorStatus
    {
        Unknown,
        Play,
        Edit,
    }
}

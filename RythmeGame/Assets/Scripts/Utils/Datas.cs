using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Datas
{
    [Serializable]
    public class NoteData
    {
        public float _timing;
        public Define.LaneNumber _laneNumber;
    }

    [Serializable]
    public class BarData
    {
        public float _scrollSpeed = 5.0f;
        public NoteData[] _noteDatas;
    }
}

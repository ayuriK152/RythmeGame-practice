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
        public float _scrollSpeed;
        public NoteData[] _noteDatas;
    }

    [Serializable]
    public class MusicPattern
    {
        public AudioSource _music;
        public Datas.BarData[] _barDatas;
        public int _bpm;
        public float _songOffset = 0.0f;
    }
}

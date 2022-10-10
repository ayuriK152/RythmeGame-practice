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
        public int _barIndex;
        public float _scrollSpeed;
        public List<NoteData> _noteDatas;
    }

    [Serializable]
    public class MusicPattern
    {
        public AudioSource _music;
        public List<Datas.BarData> _barDatas;
        public int _bpm;
        public float _songOffset = 0.0f;
    }
}

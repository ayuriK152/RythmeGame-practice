using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Datas;

public class Datas
{
    [Serializable]
    public class NoteData
    {
        public float _judgeTiming;
        public float _timing;
        public Define.LaneNumber _laneNumber;

        public NoteData() { }

        public NoteData(float timing, Define.LaneNumber laneNumber, float judgeTiming)
        {
            _timing = timing;
            _laneNumber = laneNumber;
            _judgeTiming = judgeTiming;
        }
    }

    [Serializable]
    public class BarData
    {
        public int _barIndex;
        public float _scrollSpeed;
        public List<NoteData> _noteDatas;

        public BarData()
        {
            _noteDatas = new List<NoteData>();
        }
    }

    [Serializable]
    public class MusicPattern
    {
        public AudioSource _music;
        public List<BarData> _barDatas;
        public int _bpm;
        public float _songOffset = 0.0f;

        public MusicPattern()
        {
            _barDatas = new List<BarData>();
            _bpm = 120;
            _songOffset = 0.0f;
        }
    }
}

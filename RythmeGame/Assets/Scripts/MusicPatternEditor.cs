using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPatternEditor : MonoBehaviour
{
    private GameObject _bar;
    private AudioSource _music = null;
    public Datas.BarData[] _barDatas;
    public int _bpm;
    public float _songOffset = 0.0f;


    private void Start()
    {
        _bar = Resources.Load<GameObject>("Prefabs/Bar");
    }
}

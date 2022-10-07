using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPatterns : MonoBehaviour
{
    GameObject _bar;
    public Datas.BarData[] _barDatas;
    public int _bpm;
    public float _songOffset = 0.0f;

    public bool _playGame = false;      // For Test

    private AudioSource _music;

    [SerializeField]
    private int _barIndex = 0;

    private void Awake()
    {
        _bar = Resources.Load<GameObject>("Prefabs/Bar");
        _music = transform.GetComponent<AudioSource>();
        _music.volume = 0.1f;
        InstantiateBars();
    }

    private void InstantiateBars()
    {
        float height = 0.0f;
        for (int i = 0; i < _barDatas.Length; i++)
        {
            GameObject temp = Instantiate(_bar);
            temp.transform.parent = transform;
            temp.transform.localPosition = new Vector2(0, height);
            temp.GetComponent<Bar>()._scrollSpeed = _barDatas[i]._scrollSpeed;
            temp.GetComponent<Bar>()._noteDatas = _barDatas[i]._noteDatas;
            temp.SetActive(true);

            height += _barDatas[i]._scrollSpeed;
        }
    }

    float _startTime;

    private void Update()
    {
        if (_playGame)
        {
            ScrollPattern();
            _startTime += Time.deltaTime;
            if ((_startTime * ((float)_bpm / 240.0f)) >= (1.0f + _songOffset) && _music.isPlaying == false)
                _music.Play();
        }
    }

    public void ScrollPattern()
    {
        _barIndex = (int)(_startTime * (_bpm / 240.0f));
        transform.Translate(Vector2.down * Time.deltaTime * _barDatas[_barIndex]._scrollSpeed * ((float)_bpm / 240.0f));
    }
}

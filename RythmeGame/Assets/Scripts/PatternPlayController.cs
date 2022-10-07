using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternPlayController : MonoBehaviour
{
    [SerializeField]
    private Datas.MusicPattern _musicPattern;
    private GameObject _bar;

    public bool _playGame = false;      // For Test

    [SerializeField]
    private int _barIndex = 0;

    private void Awake()
    {
        _bar = Resources.Load<GameObject>("Prefabs/Bar");
        _musicPattern._music = transform.GetComponent<AudioSource>();
        _musicPattern._music.volume = 0.1f;
        InstantiateBars();
    }

    private void InstantiateBars()
    {
        float height = 0.0f;
        for (int i = 0; i < _musicPattern._barDatas.Length; i++)
        {
            GameObject temp = Instantiate(_bar);
            temp.transform.parent = transform;
            temp.transform.localPosition = new Vector2(0, height);
            temp.GetComponent<Bar>()._scrollSpeed = _musicPattern._barDatas[i]._scrollSpeed;
            temp.GetComponent<Bar>()._noteDatas = _musicPattern._barDatas[i]._noteDatas;
            temp.SetActive(true);

            height += _musicPattern._barDatas[i]._scrollSpeed;
        }
    }

    float _startTime;

    private void Update()
    {
        if (_playGame)
        {
            ScrollPattern();
            _startTime += Time.deltaTime;
            if ((_startTime * ((float)_musicPattern._bpm / 240.0f)) >= (1.0f + _musicPattern._songOffset) && _musicPattern._music.isPlaying == false)
                _musicPattern._music.Play();
        }
    }

    private void ScrollPattern()
    {
        _barIndex = (int)(_startTime * (_musicPattern._bpm / 240.0f));
        transform.Translate(Vector2.down * Time.deltaTime * _musicPattern._barDatas[_barIndex]._scrollSpeed * ((float)_musicPattern._bpm / 240.0f));
    }
}

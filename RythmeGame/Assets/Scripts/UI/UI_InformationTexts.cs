using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_InformationTexts : MonoBehaviour
{
    private TextMeshProUGUI _timeText;
    private TextMeshProUGUI _percentageText;
    private MusicPatternEditorController _editorController;

    private int _min = 0;
    private float _sec = 0.0f;
    private float _percentage = 0.0f;

    private void Start()
    {
        _timeText = GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>();
        _percentageText = GameObject.Find("PercentageText").GetComponent<TextMeshProUGUI>();
        _editorController = GameObject.Find("PatternEditor").GetComponent<MusicPatternEditorController>();
    }

    private void UpdatePercentage()
    {
        _percentage = _editorController._patternLengthValue * 100.0f;
        _percentageText.text = string.Format("{0:F1}%", _percentage);
    }

    private void UpdateTime()
    {
        _sec = _editorController.actualPlayTime;
        if (_sec / 60.0f >= 1.0f)
        {
            _min = Convert.ToInt32(_sec / 60.0f);
            _sec -= 60.0f * _min;
        }

        if (_sec < 10.0f)
            _timeText.text = string.Format("{0:D2}:0{1:F2}", _min, _sec);
        else
            _timeText.text = string.Format("{0:D2}:{1:F2}", _min, _sec);
    }

    void Update()
    {
        UpdateTime();
        UpdatePercentage();
    }
}

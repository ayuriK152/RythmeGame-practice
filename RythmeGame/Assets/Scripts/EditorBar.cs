using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Datas;

public class EditorBar : MonoBehaviour
{
    GameObject _note;

    public float _scrollSpeed;
    public List<Datas.NoteData> _noteDatas;
    public int _barIndex;

    private TextMeshProUGUI _indexText;
    private TMP_InputField _scrollSpeedInputField;

    [SerializeField]
    static public Define.Beat _beat = Define.Beat.OneOverFour;

    private void Awake()
    {
        _note = Resources.Load<GameObject>("Prefabs/EditorNote");
        _indexText = transform.Find("BarUICanvas").Find("IndexText").GetComponent<TextMeshProUGUI>();
        _indexText.text = Convert.ToString(_barIndex);
        _scrollSpeedInputField = transform.Find("BarUICanvas").Find("ScrollSpeedInputField").GetComponent<TMP_InputField>();
        _scrollSpeedInputField.text = Convert.ToString(_scrollSpeed);
        MusicPatternEditorController.EditorBeatUpdateEvent += UpdateBeat;
        UpdateBeat();
    }

    public void UpdateBeat()
    {
        if (_beat == Define.Beat.OneOverOne)
        {
            transform.Find("1over2").gameObject.SetActive(false);
            transform.Find("1over4").gameObject.SetActive(false);
            transform.Find("1over8").gameObject.SetActive(false);
            transform.Find("1over16").gameObject.SetActive(false);
        }
        else if (_beat == Define.Beat.OneOverTwo)
        {
            transform.Find("1over2").gameObject.SetActive(true);
            transform.Find("1over4").gameObject.SetActive(false);
            transform.Find("1over8").gameObject.SetActive(false);
            transform.Find("1over16").gameObject.SetActive(false);
        }
        else if (_beat == Define.Beat.OneOverFour)
        {
            transform.Find("1over2").gameObject.SetActive(true);
            transform.Find("1over4").gameObject.SetActive(true);
            transform.Find("1over8").gameObject.SetActive(false);
            transform.Find("1over16").gameObject.SetActive(false);
        }
        else if (_beat == Define.Beat.OneOverEight)
        {
            transform.Find("1over2").gameObject.SetActive(true);
            transform.Find("1over4").gameObject.SetActive(true);
            transform.Find("1over8").gameObject.SetActive(true);
            transform.Find("1over16").gameObject.SetActive(false);
        }
        else if (_beat == Define.Beat.OneOverSixty)
        {
            transform.Find("1over2").gameObject.SetActive(true);
            transform.Find("1over4").gameObject.SetActive(true);
            transform.Find("1over8").gameObject.SetActive(true);
            transform.Find("1over16").gameObject.SetActive(true);
        }
    }

    public void UpdateScrollSpeed()
    {
        float tempBpm = Convert.ToSingle(_scrollSpeedInputField.text);

        if (tempBpm <= 0)
        {
            Debug.LogWarning("Scroll Speed must have positive real number value!");
            _scrollSpeedInputField.text = Convert.ToString(_scrollSpeed);
            return;
        }

        _scrollSpeed = Convert.ToSingle(_scrollSpeedInputField.text);
    }

    public void UpdateScrollSpeed(bool isOnLoadPattern)
    {
        if (!isOnLoadPattern)
        {
            UpdateScrollSpeed();
        }

        else
        {
            _scrollSpeedInputField.text = Convert.ToString(_scrollSpeed);
        }
    }
}

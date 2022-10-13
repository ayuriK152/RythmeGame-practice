using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Datas;

public class MusicPatternEditorController : MonoBehaviour
{
    [SerializeField]
    public MusicPattern _musicPattern;

    private GameObject _bar;
    private GameObject _note;
    private float height = 0.0f;
    public int _barIndex = 0;

    public List<Datas.NoteData> _noteDatas;
    public List<GameObject> _notes;
    public List<Datas.BarData> _barDatas;
    public List<GameObject> _bars;

    private TMP_InputField _bpmInputField;
    private TMP_InputField _offsetInputField;

    static public Action EditorBeatUpdateEvent = null;

    private void Start()
    {
        _musicPattern = new Datas.MusicPattern();
        Init();
        _bar = Resources.Load<GameObject>("Prefabs/EditorBar");
        _note = Resources.Load<GameObject>("Prefabs/Note");
        _bpmInputField = GameObject.Find("BPMInputField").GetComponent<TMP_InputField>();
        _bpmInputField.text = Convert.ToString(_musicPattern._bpm);
        _offsetInputField = GameObject.Find("OffsetInputField").GetComponent<TMP_InputField>();
        _offsetInputField.text = Convert.ToString(_musicPattern._songOffset);
    }

    private void Init()
    {
        height = 0.0f;
        _barIndex = 0;

        _noteDatas = new List<NoteData>();
        _notes = new List<GameObject>();
        _barDatas = new List<BarData>();
        _bars = new List<GameObject>();
    }

    private void Update()
    {
        PatternScrolling();
    }

    static public void ChangeEditorBeat(int index)
    {
        if (index == 0)
            EditorBar._beat = Define.Beat.OneOverOne;
        else if (index == 1)
            EditorBar._beat = Define.Beat.OneOverTwo;
        else if (index == 2)
            EditorBar._beat = Define.Beat.OneOverFour;
        else if (index == 3)
            EditorBar._beat = Define.Beat.OneOverEight;
        else if (index == 4)
            EditorBar._beat = Define.Beat.OneOverSixty;

        if (EditorBeatUpdateEvent != null)
            EditorBeatUpdateEvent.Invoke();
    }

    public void AddBar()
    {
        GameObject temp = Instantiate(_bar);
        temp.transform.parent = transform;
        temp.transform.localPosition = new Vector2(0, height);
        temp.GetComponent<EditorBar>()._barIndex = _barIndex;
        _bars.Add(temp);

        BarData tempData = new BarData();
        tempData._scrollSpeed = temp.GetComponent<EditorBar>()._scrollSpeed;
        _barDatas.Add(tempData);
        temp.SetActive(true);

        height += 4;
        _barIndex++;
    }

    public void DeleteBar()
    {
        if (_barIndex == 0)
            return;

        GameObject delBar = _bars[_barIndex - 1];

        List<GameObject> notesToBeDeleted = new List<GameObject>();

        foreach (GameObject note in _notes)
            if (note.GetComponent<EditorNote>()._editorBar == delBar)
                notesToBeDeleted.Add(note);

        foreach (GameObject childNote in notesToBeDeleted)
            if (childNote.GetComponent<EditorNote>()._isSelected)
                childNote.GetComponent<EditorNote>().DeleteEditorNote();

        EditorBeatUpdateEvent -= delBar.GetComponent<EditorBar>().UpdateBeat;
        Destroy(delBar);
        _bars.Remove(delBar);
        _barDatas.Remove(_barDatas[_barIndex - 1]);

        height -= 4;
        _barIndex--;
    }

    public void SavePatternData()
    {
        for (int i = 0; i < _barDatas.Count; i++)
        {
            //_barDatas[i]._noteDatas = new List<NoteData>();
            for (int j = 0; j < _notes.Count; j++)
            {
                EditorNote currentNote = _notes[j].GetComponent<EditorNote>();
                Datas.NoteData tempNoteData = new Datas.NoteData(currentNote._timing, currentNote._laneNumber);
                _barDatas[i]._noteDatas.Add(tempNoteData);
            }
        }
        _musicPattern._barDatas = _barDatas;
        Managers.Data.SaveAsJson(_musicPattern, "TestPattern");
    }

    public void LoadPatternData()
    {
        _musicPattern = Managers.Data.LoadJson<Datas.MusicPattern>("TestPattern");

        if (_musicPattern != null)
        {
            Init();
            for (int i = 0; i < _musicPattern._barDatas.Count; i++)
            {
                AddBar();
                GameObject currentLoadBar = _bars[_barIndex - 1];
                EditorNote[] childNotes = currentLoadBar.GetComponentsInChildren<EditorNote>();

                for (int j = 0; j < _musicPattern._barDatas[i]._noteDatas.Count; j++)
                {
                    foreach (EditorNote childNote in childNotes)
                    {
                        if (childNote.GetComponent<EditorNote>()._timing == _musicPattern._barDatas[i]._noteDatas[j]._timing
                            && childNote.GetComponent<EditorNote>()._laneNumber == _musicPattern._barDatas[i]._noteDatas[j]._laneNumber)
                        {
                            childNote.GetComponent<EditorNote>().CreateEditorNote();
                        }
                    }
                }
                _barDatas[i]._noteDatas = _musicPattern._barDatas[i]._noteDatas;
            }
        }

        else
        {
            Debug.LogError("Pattern loading is failed! Check file name or location. The file may be corrupted also.");
        }
    }

    public void UpdatePatternBPM()
    {
        int tempBpm = Convert.ToInt32(_bpmInputField.text);

        if (tempBpm <= 0)
        {
            Debug.LogWarning("BPM must have positive integer value!");
            _bpmInputField.text = Convert.ToString(_musicPattern._bpm);
            return;
        }

        _musicPattern._bpm = Convert.ToInt32(_bpmInputField.text);
    }

    public void UpdatePatternOffset()
    {
        float tempOffset = Convert.ToInt32(_offsetInputField.text);

        if (tempOffset < 0)
        {
            Debug.LogWarning("Pattern offset must have positive real number value!");
            _offsetInputField.text = Convert.ToString(_musicPattern._songOffset);
            return;
        }

        _musicPattern._songOffset = Convert.ToInt32(_offsetInputField.text);
    }

    private void PatternScrolling()
    {
        Vector2 wheelInput = -Input.mouseScrollDelta;
        if (wheelInput.y < 0 && _barIndex > 0 && transform.position.y > (_barIndex + 1) * -4.0f)      //휠을 올릴경우
            transform.Translate(wheelInput);
        if (wheelInput.y > 0 && transform.position.y < -4.0f)       // 휠을 내릴경우
            transform.Translate(wheelInput);

        if (transform.position.y > -4.0f)
            transform.position = new Vector2(0.0f, -4.0f);
        if (transform.position.y < (_barIndex + 1) * -4.0f)
            transform.position = new Vector2(0.0f, -_barIndex * -4.0f);
    }
}

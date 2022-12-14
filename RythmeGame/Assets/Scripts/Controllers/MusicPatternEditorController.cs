using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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
    private float _height = 0.0f;
    private float _editorTiming;
    public float actualPlayTime;
    public int _barIndex = 0;
    public float _patternLengthValue;
    public float noteTermValue;

    public List<Datas.NoteData> _noteDatas;
    public List<GameObject> _notes;
    public List<Datas.BarData> _barDatas;
    public List<GameObject> _bars;

    private TMP_InputField _bpmInputField;
    private TMP_InputField _offsetInputField;
    private UI_EditorSlider _editorSlider;

    public bool _scrollPattern = false;
    private bool _isOnLoadPattern = false;
    public Define.EditorStatus _status;

    static public Action EditorBeatUpdateEvent = null;
    static public Action EditorBPMUpdateEvent = null;
    static public Action ScrollStopEvent = null;

    private void Init()
    {
        _height = 0.0f;
        _barIndex = 0;

        _noteDatas = new List<NoteData>();
        _notes = new List<GameObject>();
        _barDatas = new List<BarData>();
        _bars = new List<GameObject>();

        _status = Define.EditorStatus.Edit;

        _musicPattern._music = transform.GetComponent<AudioSource>();
        _musicPattern._music.clip = Resources.Load("Musics/Grievous_Lady") as AudioClip;
        _musicPattern._music.volume = 0.2f;
    }

    private void Start()
    {
        _musicPattern = new Datas.MusicPattern();
        Init();
        _musicPattern._music = transform.GetComponent<AudioSource>();
        _musicPattern._music.clip = Resources.Load("Musics/Grievous_Lady") as AudioClip;
        _musicPattern._music.volume = 0.2f;
        _bar = Resources.Load<GameObject>("Prefabs/EditorBar");
        _note = Resources.Load<GameObject>("Prefabs/Note");
        _bpmInputField = GameObject.Find("BPMInputField").GetComponent<TMP_InputField>();
        _bpmInputField.text = Convert.ToString(_musicPattern._bpm);
        noteTermValue = 1.0f / ((float)_musicPattern._bpm / 15);
        _offsetInputField = GameObject.Find("OffsetInputField").GetComponent<TMP_InputField>();
        _offsetInputField.text = Convert.ToString(_musicPattern._songOffset);
        _editorSlider = GameObject.Find("EditorSlider").GetComponent<UI_EditorSlider>();
    }

    private void Update()
    {
        UpdatePatternLengthValue();
        ScrollPattern();
        ScrollPatternWithValue();
    }

    static public void ChangeEditorBeat(int index)      //???? ???? ???? ????
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

    public void AddBar()        //???? ????
    {
        GameObject temp = Instantiate(_bar);
        temp.transform.parent = transform;
        temp.transform.localPosition = new Vector2(0, _height);
        temp.GetComponent<EditorBar>()._barIndex = _barIndex;
        _bars.Add(temp);

        BarData tempData = new BarData();
        tempData._scrollSpeed = temp.GetComponent<EditorBar>()._scrollSpeed;
        tempData._barIndex = _barIndex;
        _barDatas.Add(tempData);
        temp.SetActive(true);

        _height += 4;
        _barIndex++;
        _editorSlider.UpdateSlider();
    }

    public void DeleteBar()     //???? ????, LIFO
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

        _height -= 4;
        _barIndex--;
        _editorSlider.UpdateSlider();
    }

    public void SavePatternData()
    {
        for (int i = 0; i < _barDatas.Count; i++)
        {
            foreach (GameObject note in _notes)
            {
                EditorNote currentNote = note.GetComponent<EditorNote>();
                if (currentNote._editorBar == _bars[i])
                {
                    Datas.NoteData tempNoteData = new Datas.NoteData(currentNote._timing, currentNote._laneNumber, currentNote.judgeTiming);
                    _barDatas[i]._noteDatas.Add(tempNoteData);
                }
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
            _isOnLoadPattern = true;

            int barDeleteCount = _bars.Count;

            for (int i = 0; i < barDeleteCount; i++)
                DeleteBar();

            Init();

            Define.Beat currentBeat = EditorBar._beat;
            ChangeEditorBeat(4);

            for (int i = 0; i < _musicPattern._barDatas.Count; i++)
            {
                AddBar();
                GameObject currentLoadBar = _bars[_barIndex - 1];
                EditorNote[] childNotes = currentLoadBar.GetComponentsInChildren<EditorNote>();

                for (int j = 0; j < _musicPattern._barDatas[i]._noteDatas.Count; j++)
                {
                    foreach (EditorNote childNote in childNotes)
                    {
                        childNote.GetComponent<EditorNote>().UpdateByBPM();
                        if (childNote.GetComponent<EditorNote>()._timing == _musicPattern._barDatas[i]._noteDatas[j]._timing
                            && childNote.GetComponent<EditorNote>()._laneNumber == _musicPattern._barDatas[i]._noteDatas[j]._laneNumber)
                        {
                            childNote.GetComponent<EditorNote>().CreateEditorNote();
                        }
                    }
                }
                _barDatas[i]._noteDatas = _musicPattern._barDatas[i]._noteDatas;
            }

            UpdatePatternBPM();
            UpdatePatternOffset();

            ChangeEditorBeat((int)currentBeat);
            _isOnLoadPattern = false;
        }

        else
        {
            Debug.LogError("Pattern loading is failed! Check file name or location. The file may be corrupted also.");
        }
    }

    public void UpdatePatternBPM()
    {
        if (!_isOnLoadPattern)
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

        else
        {
            _bpmInputField.text = Convert.ToString(_musicPattern._bpm);
        }

        noteTermValue = 1.0f / ((float)_musicPattern._bpm / 15);
        if (EditorBPMUpdateEvent != null)
            EditorBPMUpdateEvent.Invoke();
    }

    public void UpdatePatternOffset()
    {
        if (!_isOnLoadPattern)
        {
            float tempOffset = Convert.ToSingle(_offsetInputField.text);

            if (tempOffset < 0)
            {
                Debug.LogWarning("Pattern offset must have positive real number value!");
                _offsetInputField.text = Convert.ToString(_musicPattern._songOffset);
                return;
            }
            _musicPattern._songOffset = Convert.ToSingle(_offsetInputField.text);
        }

        else
        {
            _offsetInputField.text = Convert.ToString(_musicPattern._songOffset);
        }
    }

    private void ScrollPatternWithValue()
    {
        float currentPosition = (float)_barIndex * 4.0f * _patternLengthValue;
        transform.position = new Vector2(0, -currentPosition - 4.0f);
    }

    public void PlayPatternOnEditor()
    {
        _editorTiming = ((-transform.position.y - 4) / ((float)_musicPattern._bpm / 60)) - (_musicPattern._songOffset / 1000);

        if (_editorTiming < 0.0f)
        {
            _musicPattern._music.time = 0.0f;
            StartCoroutine("DelayForSongOffset");
        }
        else
        {
            _musicPattern._music.time = _editorTiming;
            _musicPattern._music.Play();
        }

        _scrollPattern = true;
        ScrollStopEvent.Invoke();
    }

    public void PausePatternOnEditor()
    {
        _musicPattern._music.Pause();
        _scrollPattern = false;
        ScrollStopEvent.Invoke();
    }

    public void StopPatternOnEditor()
    {
        PausePatternOnEditor();
        _patternLengthValue = 0;
        ScrollStopEvent.Invoke();
    }

    private void ScrollPattern()        //???????????? ?????? ?????? ??????, ?????? ???? ???? ??????.
    {
        actualPlayTime = (-transform.position.y - 4) / ((float)_musicPattern._bpm / 60);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_scrollPattern)
                PausePatternOnEditor();

            else
                PlayPatternOnEditor();
        }

        if (_scrollPattern)
        {
            _patternLengthValue += ((float)_musicPattern._bpm / 240) * Time.deltaTime / _barIndex;
        }

        if (_patternLengthValue == 1.0f)
        {
            _musicPattern._music.Pause();
            _scrollPattern = false;
        }
    }

    private IEnumerator DelayForSongOffset()        // ?????? ??????
    {
        yield return new WaitForSeconds(-_editorTiming);
        _musicPattern._music.Play();
    }

    private void UpdatePatternLengthValue()     //?????? ???????? ??, ?????? ?????? ?????? ?????? ????.
    {
        Vector2 wheelInput = -Input.mouseScrollDelta;

        if (wheelInput.y != 0)
        {
            _musicPattern._music.Pause();
            _scrollPattern = false;
        }

        if (wheelInput.y < 0 && _barIndex > 0 && _patternLengthValue <= 1.0f)      //???? ????????
            _patternLengthValue += (15.0f / (_barIndex + 1)) * Time.deltaTime;
        if (wheelInput.y > 0 && _patternLengthValue >= 0.0f)       //???? ????????
            _patternLengthValue -= (15.0f / (_barIndex + 1)) * Time.deltaTime;

        if (_patternLengthValue > 1.0f)
            _patternLengthValue = 1.0f;
        if (_patternLengthValue < 0.0f)
            _patternLengthValue = 0.0f;

        _editorSlider.UpdateSlider();
    }
}

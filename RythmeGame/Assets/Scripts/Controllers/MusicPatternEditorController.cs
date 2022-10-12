using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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

    static public Action EditorBeatUpdateEvent = null;

    private void Start()
    {
        _bar = Resources.Load<GameObject>("Prefabs/EditorBar");
        _note = Resources.Load<GameObject>("Prefabs/Note");
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
        tempData._noteDatas = null;
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
            for (int j = 0; j < _notes.Count; j++)
            {
                EditorNote currentNote = _notes[j].GetComponent<EditorNote>();
                Datas.NoteData tempNoteData = new Datas.NoteData(currentNote._timing, currentNote._laneNumber);
                _barDatas[i]._noteDatas.Add(tempNoteData);
            }
        }
        _musicPattern._barDatas = _barDatas;
        Managers.Data.saveAsJson(_musicPattern, "TestPattern");
    }

    public void LoadPatternData()
    {

    }

    private void PatternScrolling()
    {
        Vector2 wheelInput = -Input.mouseScrollDelta;
        if (wheelInput.y < 0 && _barIndex > 0 && transform.position.y > _barIndex * -4.0f)      //휠을 올릴경우
            transform.Translate(wheelInput);
        if (wheelInput.y > 0 && transform.position.y < -4.0f)       // 휠을 내릴경우
            transform.Translate(wheelInput);

        if (transform.position.y > -4.0f)
            transform.position = new Vector2(0.0f, -4.0f);
        if (transform.position.y < _barIndex * -4.0f)
            transform.position = new Vector2(0.0f, -_barIndex * -4.0f);
    }
}

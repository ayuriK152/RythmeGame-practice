using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Datas;

public class MusicPatternEditor : MonoBehaviour
{
    [SerializeField]
    private MusicPattern _musicPattern;

    private GameObject _bar;
    private GameObject _note;
    private float height = 0.0f;
    public int _barIndex = 0;

    private List<GameObject> _barTempDatas;
    private List<GameObject> _tempBars;

    static public Action EditorBeatUpdateEvent = null;

    private void Start()
    {
        _bar = Resources.Load<GameObject>("Prefabs/EditorBar");
        _note = Resources.Load<GameObject>("Prefabs/Note");
    }

    public void AddBar()
    {
        GameObject temp = Instantiate(_bar);
        temp.transform.parent = transform;
        temp.transform.localPosition = new Vector2(0, height);

        BarData tempData = new BarData();
        tempData._scrollSpeed = temp.GetComponent<EditorBar>()._scrollSpeed;
        tempData._noteDatas = null;
        _musicPattern._barDatas.Add(tempData);
        temp.SetActive(true);

        height += 4;
        _barIndex++;
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

    public void DeleteBar()
    {

    }

    public void AddNote()
    {

    }

    public void DeleteNote()
    {

    }

    public void savePatternData()
    {

    }

    public void loadPatternData()
    {

    }
}

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
        tempData._scrollSpeed = temp.GetComponent<Bar>()._scrollSpeed;
        tempData._noteDatas = null;
        _musicPattern._barDatas.Add(tempData);
        temp.SetActive(true);

        height += 4;
        _barIndex++;
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

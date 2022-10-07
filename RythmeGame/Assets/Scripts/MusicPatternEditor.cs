using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Datas;

public class MusicPatternEditor : MonoBehaviour
{
    private GameObject _bar;
    private GameObject _note;
    private float height = 0.0f;
    public int _barIndex = 0;



    private void Start()
    {
        _bar = Resources.Load<GameObject>("Prefabs/Bar");
        _note = Resources.Load<GameObject>("Prefabs/Note");
    }

    public void AddBar()
    {
        GameObject temp = Instantiate(_bar);
        temp.transform.parent = transform;
        temp.transform.localPosition = new Vector2(0, height);
        temp.SetActive(true);

        height += temp.GetComponent<Bar>()._scrollSpeed;
        _barIndex++;
    }

    public void AddNote()
    {

    }

    public void savePatternData()
    {

    }

    public void loadPatternData()
    {

    }
}

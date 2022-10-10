using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Note;

public class Bar : MonoBehaviour
{
    GameObject _note;

    public float _scrollSpeed;
    public List<Datas.NoteData> _noteDatas;

    private void Awake()
    {
        _note = Resources.Load<GameObject>("Prefabs/Note");
        CreateNotes();
    }

    void CreateNotes()
    {
        for (int i = 0; i < _noteDatas.Count; i++)
        {
            GameObject temp = Instantiate(_note);
            temp.GetComponent<Note>()._timing = _noteDatas[i]._timing;
            temp.GetComponent<Note>()._laneNumber = _noteDatas[i]._laneNumber;
            temp.transform.parent = transform;
            temp.SetActive(true);
        }
    }
}

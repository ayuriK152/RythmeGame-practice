using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EditorBar : MonoBehaviour
{
    GameObject _note;

    public float _scrollSpeed;
    public List<Datas.NoteData> _noteDatas;

    private void Awake()
    {
        _note = Resources.Load<GameObject>("Prefabs/Note");

    }
}

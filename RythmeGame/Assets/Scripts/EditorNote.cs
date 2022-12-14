using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class EditorNote : MonoBehaviour
{
    public float judgeTiming;
    public float _timing;
    public Define.LaneNumber _laneNumber;
    public bool _isSelected = false;
    private bool _isTriggered = false;

    GameObject _lineGo;
    GameObject _selectedNotesParent;
    public GameObject _editorBar;
    MusicPatternEditorController _editorController;
    AudioSource _hitSound;

    private void Awake()
    {
        _lineGo = transform.parent.gameObject;
        _editorBar = transform.parent.parent.parent.gameObject;
        _selectedNotesParent = _editorBar.transform.Find("SelectedNotes").gameObject;
        _editorController = GameObject.Find("PatternEditor").GetComponent<MusicPatternEditorController>();
        MusicPatternEditorController.EditorBPMUpdateEvent += UpdateByBPM;
        MusicPatternEditorController.ScrollStopEvent += UpdateTriggeredBoolValue;
        _hitSound = transform.GetComponent<AudioSource>();
        _hitSound.enabled = false;
        InitializeLaneNumber();
        InitializeTiming();
    }

    private void OnMouseEnter()
    {
        if (_isSelected)
            return;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255.0f, 255.0f, 255.0f, 0.25f);
    }

    private void OnMouseExit()
    {
        if (_isSelected)
            return;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255.0f, 255.0f, 255.0f, 0.0f);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
            CreateEditorNote();

        if (Input.GetMouseButtonDown(1))
            DeleteEditorNote();
    }

    public void CreateEditorNote()
    {
        _isSelected = true;
        transform.parent = _selectedNotesParent.transform;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255.0f, 255.0f, 255.0f, 1.0f);
        _editorController._notes.Add(gameObject);
        _hitSound.enabled = true;
        transform.GetComponent<Collider2D>().enabled = true;
    }

    public void DeleteEditorNote()
    {
        _isSelected = false;
        transform.parent = _lineGo.transform;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255.0f, 255.0f, 255.0f, 0.0f);
        _editorController._notes.Remove(gameObject);
        _hitSound.enabled = false;
        transform.GetComponent<Collider2D>().enabled = false;
    }

    private void InitializeLaneNumber()
    {
        if (transform.name == "Note1")
            _laneNumber = Define.LaneNumber.First;
        else if (transform.name == "Note2")
            _laneNumber = Define.LaneNumber.Second;
        else if (transform.name == "Note3")
            _laneNumber = Define.LaneNumber.Third;
        else if (transform.name == "Note4")
            _laneNumber = Define.LaneNumber.Fourth;
        else
            _laneNumber = Define.LaneNumber.Unknown;
    }

    private void InitializeTiming()
    {
        GameObject timeGo = _lineGo.transform.parent.gameObject;

        if (timeGo.name == "1over1")
        {
            _timing = 0;
        }

        else
        {
            if (timeGo.name == "1over2")
                _timing = 8;

            else if (timeGo.name == "1over4")
            {
                if (_lineGo.name == "Line1")
                    _timing = 4;

                else if (_lineGo.name == "Line2")
                    _timing = 12;
            }

            else if (timeGo.name == "1over8")
            {
                if (_lineGo.name == "Line1")
                    _timing = 2;

                else if (_lineGo.name == "Line2")
                    _timing = 6;

                else if (_lineGo.name == "Line3")
                    _timing = 10;

                else if (_lineGo.name == "Line4")
                    _timing = 14;
            }

            else if (timeGo.name == "1over16")
            {
                if (_lineGo.name == "Line1")
                    _timing = 1;

                else if (_lineGo.name == "Line2")
                    _timing = 3;

                else if (_lineGo.name == "Line3")
                    _timing = 5;

                else if (_lineGo.name == "Line4")
                    _timing = 7;

                else if (_lineGo.name == "Line5")
                    _timing = 9;

                else if (_lineGo.name == "Line6")
                    _timing = 11;

                else if (_lineGo.name == "Line7")
                    _timing = 13;

                else if (_lineGo.name == "Line8")
                    _timing = 15;
            }
        }

        judgeTiming = _editorController.noteTermValue * _timing + _editorBar.GetComponent<EditorBar>()._barIndex * _editorController.noteTermValue * 16.0f;
    }
    
    public void UpdateByBPM()
    {
        judgeTiming = _editorController.noteTermValue * _timing + _editorBar.GetComponent<EditorBar>()._barIndex * _editorController.noteTermValue * 16.0f;
    }

    public void UpdateTriggeredBoolValue()
    {
        if (judgeTiming - _editorController.actualPlayTime <= 0.002f)
            _isTriggered = true;
        else
            _isTriggered = false;
    }

    private void Update()
    {
        if (_isSelected && _editorController._scrollPattern && !_isTriggered)
        {
            if (judgeTiming - _editorController.actualPlayTime <= 0.002f)
            {
                _hitSound.PlayOneShot(_hitSound.clip);
                Debug.Log(judgeTiming);
                _isTriggered = true;
            }
        }
    }
}

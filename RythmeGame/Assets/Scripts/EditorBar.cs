using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EditorBar : MonoBehaviour
{
    GameObject _note;

    public float _scrollSpeed;
    public List<Datas.NoteData> _noteDatas;

    [SerializeField]
    static public Define.Beat _beat = Define.Beat.OneOverFour;

    private void Awake()
    {
        _note = Resources.Load<GameObject>("Prefabs/EditorNote");
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
}

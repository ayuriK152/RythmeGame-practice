using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EditorSlider : MonoBehaviour
{
    private Scrollbar _editorSlider;
    private MusicPatternEditorController _editorController;

    private void Start()
    {
        _editorSlider = gameObject.GetComponent<Scrollbar>();
        _editorController = GameObject.Find("PatternEditor").GetComponent<MusicPatternEditorController>();
    }

    public void UpdateSliderValue()
    {
        _editorController._patternLengthValue = _editorSlider.value;
    }

    public void UpdateSlider()
    {
        _editorSlider.size = 1.0f / (_editorController._barIndex + 1);
        _editorSlider.value = _editorController._patternLengthValue;
    }
}

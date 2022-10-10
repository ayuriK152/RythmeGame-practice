using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UI;

public class UI_BeatSelectDropdown : MonoBehaviour
{
    private TMP_Dropdown _dropdown;

    private void Start()
    {
        _dropdown = transform.GetComponent<TMP_Dropdown>();
    }

    public void SelectBeat()
    {
        MusicPatternEditor.ChangeEditorBeat(_dropdown.value);
    }
}

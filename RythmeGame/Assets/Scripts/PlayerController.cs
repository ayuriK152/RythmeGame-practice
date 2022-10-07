using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    GameObject InputLine;

    [SerializeField]
    GameObject[] InputKeys;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        Managers.Input.KeyAction -= OnKeyEvent;
        Managers.Input.KeyAction += OnKeyEvent;

        if (GameObject.Find("InputLine") != null)
        {
            SpriteRenderer[] keyRenderer;
            InputLine = GameObject.Find("InputLine");
            keyRenderer = InputLine.gameObject.GetComponentsInChildren<SpriteRenderer>();

            for (int i = 0; i < 4; i++)
                InputKeys[i] = keyRenderer[i].gameObject;
        }
    }

    public void OnKeyEvent()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {

        }
        if (Input.GetKeyDown(KeyCode.S))
        {

        }
        if (Input.GetKeyDown(KeyCode.Semicolon))
        {

        }
        if (Input.GetKeyDown(KeyCode.Quote))
        {

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject.Find("MusicPattern").GetComponent<PatternPlayController>()._playGame = true;
        }
    }
}

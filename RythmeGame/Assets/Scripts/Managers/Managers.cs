using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers _managers;
    static Managers Manager { get { Init(); return _managers; } }

    InputManager _input = new InputManager();
    DataManager _data = new DataManager();

    public static InputManager Input { get { return Manager._input; } }
    public static DataManager Data { get { return Manager._data; } }

    void Start()
    {
        Init();
    }

    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        if (_managers == null)
        {
            GameObject go = GameObject.Find("@Manager");
            if (go == null)
            {

            }

            DontDestroyOnLoad(go);
            _managers = go.GetComponent<Managers>();
        }
    }
}
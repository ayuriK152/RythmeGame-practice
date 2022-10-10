using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorNote : MonoBehaviour
{
    public float _timing;
    public Define.LaneNumber _laneNumber;

    private void Awake()
    {
        InitializeLaneNumber();
        InitializeTiming();
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
        GameObject lineGo = transform.parent.gameObject;
        GameObject timeGo;

        if (lineGo.name == "BaseLine")
        {
            _timing = 0;
        }

        else
        {
            timeGo = lineGo.transform.parent.gameObject;
            if (timeGo.name == "2by1")
                _timing = 8;

            else if (timeGo.name == "4by1")
            {
                if (lineGo.name == "Line1")
                    _timing = 4;

                else if (lineGo.name == "Line2")
                    _timing = 12;
            }

            else if (timeGo.name == "8by1")
            {
                if (lineGo.name == "Line1")
                    _timing = 2;

                else if (lineGo.name == "Line2")
                    _timing = 6;

                else if (lineGo.name == "Line3")
                    _timing = 10;

                else if (lineGo.name == "Line4")
                    _timing = 14;
            }

            else if (timeGo.name == "16by1")
            {
                if (lineGo.name == "Line1")
                    _timing = 1;

                else if (lineGo.name == "Line2")
                    _timing = 3;

                else if (lineGo.name == "Line3")
                    _timing = 5;

                else if (lineGo.name == "Line4")
                    _timing = 7;

                else if (lineGo.name == "Line5")
                    _timing = 9;

                else if (lineGo.name == "Line6")
                    _timing = 11;

                else if (lineGo.name == "Line7")
                    _timing = 13;

                else if (lineGo.name == "Line8")
                    _timing = 15;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager
{
    public void saveAsJson<T>(T obj, string path)
    {
        File.WriteAllText(Application.dataPath + "/Resources/Datas/" + path, JsonUtility.ToJson(obj));
    }
}

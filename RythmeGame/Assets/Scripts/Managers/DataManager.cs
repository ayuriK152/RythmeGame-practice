using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager
{
    public void saveAsJson<T>(T obj, string fileName)
    {
        File.WriteAllText(Application.dataPath + "/Resources/Datas/" + fileName + ".json", JsonUtility.ToJson(obj));
    }
}

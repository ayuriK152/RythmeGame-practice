using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static Datas;

public class DataManager
{
    public void SaveAsJson<T>(T obj, string fileName)
    {
        File.WriteAllText(Application.dataPath + "/Resources/Datas/" + fileName + ".json", JsonUtility.ToJson(obj));
    }

    public T LoadJson<T>(string fileName)
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Datas/" + fileName);
        if (textAsset != null)
        {
            T loadedData = JsonUtility.FromJson<T>(textAsset.text);
            return loadedData;
        }
        else
            return default(T);
    }
}

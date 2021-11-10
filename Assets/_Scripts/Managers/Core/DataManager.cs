using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}


public class DataManager
{
    public AnswerSheetData answerSheetData;
    public SituationData situationAnalysisData;
    public RecordData recordData = new RecordData();
   

    public void Init()
    {
        for (Define.SituationCode i = Define.SituationCode.S1_1; i <= Define.SituationCode.S4_14; i++)
        {
            recordData.handCleanRecords.Add(i, false);
        }
        answerSheetData = Managers.Resource.Load<AnswerSheetData>("ScriptableObj/AnswerSheet");
        situationAnalysisData = Managers.Resource.Load<SituationData>("ScriptableObj/SituationData");
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

    public void Save()
    {
        
        string json = JsonConvert.SerializeObject(recordData);
        Debug.Log(json);
    }
    //public void Load()
    //{
    //    //tring json = ReadFromFile($"{fileName}.txt");
    //    //JsonUtility.FromJsonOverwrite(json, recordData);
    //}
    //public void WriteToFile(string json)
    //{
    //    string path = GetFilePath($"{fileName}.txt");
    //    FileStream fileStream = new FileStream(path, FileMode.Create);

    //    using (StreamWriter writer = new StreamWriter(fileStream))
    //    {
    //        writer.Write(json);
    //    }

    //}
    //public string ReadFromFile(string fileName)
    //{
    //    string path = GetFilePath($"{fileName}.txt");
    //    if (File.Exists(path))
    //    {
    //        using (StreamReader reader = new StreamReader(path))
    //        {
    //            string json = reader.ReadToEnd();
    //            return json;
    //        }
    //    }
    //    else
    //        Debug.LogError("File Not Found!");
        
    //    return "";
    //}
    //public string GetFilePath(string fileName)
    //{
    //    return Application.persistentDataPath + "/" + fileName;
    //}
}

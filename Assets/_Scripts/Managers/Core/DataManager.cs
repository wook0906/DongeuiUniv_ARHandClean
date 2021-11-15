using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

//public interface ILoader<Key, Value>
//{
//    Dictionary<Key, Value> MakeDict();
//}


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

    //Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    //{
    //    TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
    //    return JsonUtility.FromJson<Loader>(textAsset.text);
    //}
    
    //여기서 Json 만듭니당
    public void SaveRecord()
    {
        string json = JsonConvert.SerializeObject(recordData);
        json = json.Replace("\"handCleanRecords\":{", "");
        json = json.Remove(json.Length-1);
        Send.Result(json);
    }
    
}

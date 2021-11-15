using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnswerSheetDictionary : SerializableDictionary<Define.SituationCode, AnswerSheet> { }

[CreateAssetMenu(menuName = "AnswerSheet")]
public class AnswerSheetData : ScriptableObject
{
    public AnswerSheetDictionary answerDict;
}
[System.Serializable]
public class AnswerSheet
{
    public bool answer;
    public List<Define.HandCleanSituationType> typeList;
}

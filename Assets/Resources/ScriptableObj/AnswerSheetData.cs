﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnswerSheetDictionary : SerializableDictionary<Define.HandCleanRecord, bool> { }

[CreateAssetMenu(menuName = "AnswerSheet")]
public class AnswerSheetData : ScriptableObject
{
    public AnswerSheetDictionary answerDict;
}
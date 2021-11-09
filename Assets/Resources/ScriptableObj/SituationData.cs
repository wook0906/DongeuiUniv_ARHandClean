using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SituationData")]
public class SituationData : ScriptableObject
{
    public SituationDataDictionary SituationDict;
}

[Serializable]
public class SituationDataDictionary : SerializableDictionary<Define.HandCleanRecord, Situation> { }

[Serializable]
public class Situation
{
    public string right;
    public string wrong;
    [TextArea]
    public string analysis;
}

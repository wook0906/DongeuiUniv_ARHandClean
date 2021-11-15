using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Json;
using System;

public class RecordData
{
    public string endTime = "None";
    public string sex = "None";
    public int age = 0;
    public int grade = 0;
    public string email = "Temp@Temp.com";
    public string phoneNumber = "00000000000";
    public Dictionary<Define.SituationCode, bool> handCleanRecords = new Dictionary<Define.SituationCode, bool>();
    //public string weakestSituationType = "None"; 
}

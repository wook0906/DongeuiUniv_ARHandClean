using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Json;

public class RecordData
{

    public string studNum = "00000000";
    public Dictionary<Define.HandCleanRecord, bool> handCleanRecords = new Dictionary<Define.HandCleanRecord, bool>();

   
}

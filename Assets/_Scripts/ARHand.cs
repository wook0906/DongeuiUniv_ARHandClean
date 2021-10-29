using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARHand : MonoBehaviour
{
    public SkinnedMeshRenderer mr;
    public bool isOn;

    private void Awake()
    {
        mr = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        isOn = mr.enabled;
    }
    
}

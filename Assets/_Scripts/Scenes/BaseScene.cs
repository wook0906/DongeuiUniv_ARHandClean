using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public abstract class BaseScene : MonoBehaviour
{
  
    Define.Scene _sceneType = Define.Scene.UnKnown;

    public Define.Scene SceneType { get; protected set; }
    [HideInInspector]
    public List<UIPopup> needRenewPositionPopupList = new List<UIPopup>();

    public Dictionary<Define.HandCleanRecord, bool> handCleanRecord = new Dictionary<Define.HandCleanRecord, bool>();
    public Define.HandCleanRecord currentRecord = Define.HandCleanRecord.None;
    public bool isDidCleanHand = false;

    void Awake()
    {
        Init();
    }

    public void RenewFocusPositions()
    {
        foreach (var item in needRenewPositionPopupList)
        {
            item.RenewFocusPostion();
        }
    }
    
    public virtual void StartScenario()
    {

    }

    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
    }
    public abstract void TakeDoneCallback();
    public abstract void Clear();
}

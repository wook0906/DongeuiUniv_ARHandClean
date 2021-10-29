using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public abstract class BaseScene : MonoBehaviour
{
  
    Define.Scene _sceneType = Define.Scene.UnKnown;

    public Define.Scene SceneType { get; protected set; }
    [HideInInspector]
    public List<UIBase> needRenewPositionPopupList = new List<UIBase>();

    public Define.HandCleanRecord currentRecord = Define.HandCleanRecord.None;
    [HideInInspector]
    public bool isDidCleanHand = false;
    public Define.Infection infectionState = Define.Infection.None;

    public ARHand rightHand;
    public ARHand leftHand;

    List<GameObject> covidList = new List<GameObject>(); 

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

        rightHand = GameObject.Find("RightHand").GetComponent<ARHand>();
        leftHand = GameObject.Find("LeftHand").GetComponent<ARHand>();
    }
    public abstract void TakeDoneCallback();
    public abstract void Clear();

    public bool IsHandOn()
    {
        if (rightHand.isOn && leftHand.isOn)
            return true;
        return false;
    }
    public void ShowCovid()
    {
        foreach (var item in covidList)
        {
            item.SetActive(true);
        }
    }
    public void GenerateCovid(Transform target, Define.Infection infectionState, bool isSetParent = false)
    {
        
        if (infectionState == Define.Infection.None) return;

        GameObject covid;
        if (!isSetParent)
        {
            covid = Managers.Resource.Instantiate("covid");
            covid.transform.position = target.position;
        }
        else
        {
            covid = Managers.Resource.Instantiate("covid", target);
            covid.transform.position = Vector3.zero;
        }
        if (infectionState == Define.Infection.Left)
            foreach (Transform item in covid.transform)
                item.GetComponent<MeshRenderer>().material.color = Color.red;
        else if (infectionState == Define.Infection.Right)
            foreach (Transform item in covid.transform)
                item.GetComponent<MeshRenderer>().material.color = Color.blue;
        else
            foreach (Transform item in covid.transform)
                item.GetComponent<MeshRenderer>().material.color = new Color(1f, 0, 1f);

        covid.SetActive(false);
        covidList.Add(covid);
    }
}

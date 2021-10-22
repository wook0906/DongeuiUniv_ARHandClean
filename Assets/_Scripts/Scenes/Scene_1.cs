using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scene_1 : BaseScene
{
    bool isTakeDone = false;

    public GameObject bedFenceUp;
    public GameObject bedFenceDown;
    public GameObject oxygenChecker;
    public Transform oxygenCheckerTargetTransform;

    Basic_UI ui;
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Scene_1;

        ui = Managers.UI.ShowSceneUI<Basic_UI>();
        Briefing_Popup briefing = Managers.UI.ShowPopupUI<Briefing_Popup>();
        TextAsset textAsset = new TextAsset("[상황제시]\n\n당신은 보이는 병실의 담당간호사입니다.\n환자1은 2시간마다  체위를 변경하고 있습니다.\n환자1의 체위를 변경하기 위해 병실로 들어가면서 상황이 시작됩니다.\n손소독제를 누르면 손위생을 수행하는\n20초 동안은 다른 활동을 해서는 안됩니다.");
 
        briefing.SetText(textAsset);
    }
    IEnumerator Scenario()
    {
        yield return new WaitUntil(() => Managers.UI.GetSceneUI<Basic_UI>() != null);
        ui.SetGloveButton(false);

        Guide_Popup guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("***환자쪽에서 알람이 울리고있다!", Define.Views.Right_Patient);
        yield return new WaitUntil(() => guide == null);

        Focusing_Popup focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("MonitorAlarm").transform, Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(focus);
        
        yield return StartCoroutine(WaitTakeDone());
        Debug.Log("모니터 알람 꺼짐!");

        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("***환자의 산소포화도 측정기가 빠져있다!",Define.Views.Right_Patient);
        yield return new WaitUntil(() => guide == null);

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("RightBedFence").transform, Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(focus);

        yield return StartCoroutine(WaitTakeDone());
        bedFenceUp.SetActive(false);
        bedFenceDown.SetActive(true);
        Debug.Log("펜스 내려감!");


        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("OxygenChecker").transform , Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(focus);

        yield return StartCoroutine(WaitTakeDone());
        oxygenChecker.transform.position = oxygenCheckerTargetTransform.position;
        Debug.Log("산소포화도 측정기 원위치 함!");

        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("***환자의 호출!", Define.Views.Left_Patient_RightView);
        yield return new WaitUntil(() => guide == null);

        SpeechBubble_Popup speech = Managers.UI.ShowPopupUI<SpeechBubble_Popup>();
        speech.SetText(new TextAsset("침대 좀 올려주세요"));
        speech.SetAnchor(GameObject.Find("LeftPatientHead").transform, Define.Views.Left_Patient_RightView);
        needRenewPositionPopupList.Add(speech);
        yield return new WaitUntil(() => speech == null);

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftBedLever").transform, Define.Views.Left_BedLever);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        Debug.Log("왼쪽 환자의 침대를 올려줌!");

        //이때 환자는 오른쪽으로 돌아누워있어야함.
        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftPatientBody").transform, Define.Views.Left_Patient_RightView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        Debug.Log("오른쪽으로 돌아누워있는 환자 등에있는 베게를 치움!");
        //이때 환자는 바로 눕혀져야함.

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftPatientBody").transform, Define.Views.Left_Patient_RightView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        Debug.Log("환자를 왼쪽으로 돌려 눕힘!");

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftPatientBody").transform, Define.Views.Left_Patient_RightView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        Debug.Log("베게를 등에 갖다 대어줌!");

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftPatientBody").transform, Define.Views.Left_Patient_RightView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        Debug.Log("이불 덮어줌!");

        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("처치를 다 했으니 퇴실하실?", Define.Views.Left_Patient_RightView);
        yield return new WaitUntil(() => guide == null);

        Debug.Log("평가 데이터 전송, 상황1 종료");
    }

    IEnumerator WaitTakeDone()
    {
        yield return new WaitUntil(() => isTakeDone);
        isTakeDone = false;
    }

    public override void StartScenario()
    {
        StartCoroutine(Scenario());
    }
    public override void TakeDoneCallback()
    {
        isTakeDone = true;
    }
    public override void Clear()
    {

    }
  
}

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
    public GameObject pillowRight;
    public GameObject rightLyingPatient;
    public GameObject pillowLeft;
    public GameObject leftLyingPatient;
    public GameObject originLeftPatient;
    public GameObject leftBedBlanket;



    Basic_UI ui;
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Scene_1;

        ui = Managers.UI.ShowSceneUI<Basic_UI>();
        Briefing_Popup briefing = Managers.UI.ShowPopupUI<Briefing_Popup>();
        TextAsset textAsset = new TextAsset("[상황제시]\n\n당신은 보이는 병실의 담당간호사입니다.\n환자1은 2시간마다  체위를 변경하고 있습니다.\n환자1의 체위를 변경하기 위해 병실로 들어가면서 상황이 시작됩니다.\n손소독제를 누르면 손위생을 수행하는\n20초 동안은 다른 활동을 해서는 안됩니다.");

        handCleanRecord.Clear();
        for(Define.HandCleanRecord key = Define.HandCleanRecord.S1_1; key <= Define.HandCleanRecord.S1_10; key++)
        {
            handCleanRecord.Add(key, false);
        }
 
        briefing.SetText(textAsset);
    }
    IEnumerator Scenario()
    {
        yield return new WaitUntil(() => Managers.UI.GetSceneUI<Basic_UI>() != null);
        ui.SetGloveButton(false);

        Guide_Popup guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("***환자쪽에서 알람이 울리고있다!", Define.Views.Right_Patient);
        yield return new WaitUntil(() => guide == null);

        currentRecord = Define.HandCleanRecord.S1_1;

        Focusing_Popup focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("MonitorAlarm").transform, Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(focus);
        
        yield return StartCoroutine(WaitTakeDone());
        handCleanRecord[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;
        Debug.Log("모니터 알람 꺼짐!");

        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("***환자의 산소포화도 측정기가 빠져있다!",Define.Views.Right_Patient);
        yield return new WaitUntil(() => guide == null);

        currentRecord = Define.HandCleanRecord.S1_2;

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("RightBedFence").transform, Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(focus);

        yield return StartCoroutine(WaitTakeDone());
        bedFenceUp.SetActive(false);
        bedFenceDown.SetActive(true);
        Debug.Log("펜스 내려감!");
        handCleanRecord[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        currentRecord = Define.HandCleanRecord.S1_3;

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("OxygenChecker").transform , Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(focus);

        yield return StartCoroutine(WaitTakeDone());
        oxygenChecker.transform.position = oxygenCheckerTargetTransform.position;
        Debug.Log("산소포화도 측정기 원위치 함!");
        handCleanRecord[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;


        currentRecord = Define.HandCleanRecord.S1_4;
        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("RightBedFenceDown").transform, Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        bedFenceUp.SetActive(true);
        bedFenceDown.SetActive(false);
        handCleanRecord[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        currentRecord = Define.HandCleanRecord.S1_5;
        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("***환자의 호출!", Define.Views.Left_Patient_RightView);
        yield return new WaitUntil(() => guide == null);
        handCleanRecord[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

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
        

        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("환자의 체위를 바꿔주자.", Define.Views.Left_Patient_RightView);
        yield return new WaitUntil(() => guide == null);

        currentRecord = Define.HandCleanRecord.S1_6;
        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("RightPillow").transform, Define.Views.Left_Patient_RightView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        Debug.Log("오른쪽으로 돌아누워있는 환자 등에있는 베게를 치움!");
        pillowRight.SetActive(false);
        originLeftPatient.SetActive(true);
        rightLyingPatient.SetActive(false);
        handCleanRecord[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        currentRecord = Define.HandCleanRecord.S1_7;
        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftPatientBody").transform, Define.Views.Left_Patient_RightView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        Debug.Log("환자를 왼쪽으로 돌려 눕힘!");
        originLeftPatient.SetActive(false);
        leftLyingPatient.SetActive(true);
        handCleanRecord[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;


        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("왼쪽에 베개를 끼워주자.", Define.Views.Left_Patient_LeftView);
        yield return new WaitUntil(() => guide == null);

        currentRecord = Define.HandCleanRecord.S1_8;
        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftPillow").transform, Define.Views.Left_Patient_LeftView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        Debug.Log("베게를 등에 갖다 대어줌!");
        pillowLeft.SetActive(true);
        handCleanRecord[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        currentRecord = Define.HandCleanRecord.S1_9;
        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftPatientBody").transform, Define.Views.Left_Patient_LeftView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        Debug.Log("이불 덮어줌!");
        leftBedBlanket.SetActive(true);
        handCleanRecord[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        currentRecord = Define.HandCleanRecord.S1_10;
        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("처치를 다 했으니 퇴실하실?", Define.Views.Both);
        yield return new WaitUntil(() => guide == null);
        handCleanRecord[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        Debug.Log("평가 데이터 전송, 상황1 종료");

        for (Define.HandCleanRecord i = Define.HandCleanRecord.S1_1; i <= Define.HandCleanRecord.S1_10; i++)
        {
            Debug.Log($"입력 : {handCleanRecord[i]}, 답 : {Managers.Data.answerSheetData.answerDict[i]}");
        }
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

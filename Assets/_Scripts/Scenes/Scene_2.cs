﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scene_2 : BaseScene
{
    bool isTakeDone = false;
    public GameObject trayOnTable;
    public GameObject trayOnFluid;
    public GameObject trayOnNewLine;
    public GameObject originIv;
    public GameObject newIv;
    public GameObject newIvLine;
    public GameObject originIvLine;
    public GameObject leftBedFenceUp;
    public GameObject leftBedFenceDown;

    public GameObject rightBedFenceUp;
    public GameObject rightBedFenceDown;
    public GameObject rightBedPeeBag;
    public GameObject originPole;
    public GameObject positionChangeIvPole;
    public GameObject originRightPatient;
    public GameObject sitRightPatient;

    Basic_UI ui;

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Scene_2;

        handCleanRecord.Clear();
        for (Define.HandCleanRecord key = Define.HandCleanRecord.S2_1; key <= Define.HandCleanRecord.S2_12; key++)
        {
            handCleanRecord.Add(key, false);
        }

        ui = Managers.UI.ShowSceneUI<Basic_UI>();
        Briefing_Popup briefing = Managers.UI.ShowPopupUI<Briefing_Popup>();
        TextAsset textAsset = new TextAsset("[상황제시]\n\n상황2에 대한 브리핑 UI입니다.");

        briefing.SetText(textAsset);
    }
    IEnumerator Scenario()
    {
        yield return new WaitUntil(() => Managers.UI.GetSceneUI<Basic_UI>() != null);
        ui.SetGloveButton(false);

        Guide_Popup guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("***트레이를 내려놓아야 함", Define.Views.Left_Patient_RightView);
        yield return new WaitUntil(() => guide == null);

        currentRecord = Define.HandCleanRecord.S2_1;
        Focusing_Popup focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftBedTable").transform, Define.Views.Left_Patient_RightView);
        needRenewPositionPopupList.Add(focus);

        yield return StartCoroutine(WaitTakeDone());
        foreach (var item in ui.camList)
        {
            item.transform.Find("tray").gameObject.SetActive(false);
        }
        trayOnTable.SetActive(true);
        handCleanRecord[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;
        Debug.Log("트레이 내려놓음!");

        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("새로운 수액으로 교체해주자!", Define.Views.Left_Patient_RightView);
        yield return new WaitUntil(() => guide == null);

        
        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftBedTable").transform, Define.Views.Left_Patient_RightView);
        needRenewPositionPopupList.Add(focus);

        yield return StartCoroutine(WaitTakeDone());
        trayOnFluid.SetActive(false);
        Debug.Log("수액 집어듬!");
        

        currentRecord = Define.HandCleanRecord.S2_2;
        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftEmptyFluid").transform, Define.Views.Left_Patient_Stand);
        needRenewPositionPopupList.Add(focus);

        yield return StartCoroutine(WaitTakeDone());
        originIv.SetActive(false);
        newIv.SetActive(true);
        handCleanRecord[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        SpeechBubble_Popup speech = Managers.UI.ShowPopupUI<SpeechBubble_Popup>();
        speech.SetText(new TextAsset("수액 교체완료"));
        speech.SetAnchor(GameObject.Find("LeftPump").transform, Define.Views.Left_Patient_Stand);
        needRenewPositionPopupList.Add(speech);
        yield return new WaitUntil(() => speech == null);

        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("정맥 펌프를 끄세요", Define.Views.Left_Patient_Stand);
        yield return new WaitUntil(() => guide == null);

        currentRecord = Define.HandCleanRecord.S2_3;
        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftPump").transform, Define.Views.Left_Patient_Stand);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        handCleanRecord[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        speech = Managers.UI.ShowPopupUI<SpeechBubble_Popup>();
        speech.SetText(new TextAsset("정맥펌프 끔"));
        speech.SetAnchor(GameObject.Find("LeftEmptyFluid").transform, Define.Views.Left_Patient_Stand);
        needRenewPositionPopupList.Add(speech);
        yield return new WaitUntil(() => speech == null);

        currentRecord = Define.HandCleanRecord.S2_4;
        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftBedFence").transform, Define.Views.Left_Patient_LeftView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        leftBedFenceUp.SetActive(false);
        leftBedFenceDown.SetActive(true);
        Debug.Log("난간을 내림");
        handCleanRecord[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        currentRecord = Define.HandCleanRecord.S2_5;
        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftPatientBody").transform, Define.Views.Left_Patient_LeftView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        Debug.Log("이불 제거!");
        handCleanRecord[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        currentRecord = Define.HandCleanRecord.S2_6;
        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftPatientArm").transform, Define.Views.Left_Patient_LeftView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        originIvLine.SetActive(false);
        Debug.Log("기존 줄 제거!");
        handCleanRecord[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftBedTable").transform, Define.Views.Left_Patient_LeftView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        trayOnNewLine.SetActive(false);
        Debug.Log("새로운 줄 집어듬!");


        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftPatientArm").transform, Define.Views.Left_Patient_LeftView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        newIvLine.SetActive(true);
        Debug.Log("새로운 줄 장착!");

        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("정맥 펌프를 다시 켜세요", Define.Views.Left_Patient_Stand);
        yield return new WaitUntil(() => guide == null);

        currentRecord = Define.HandCleanRecord.S2_7;
        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftPump").transform, Define.Views.Left_Patient_Stand);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        Debug.Log("정맥주입펌프 재가동!");
        handCleanRecord[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        currentRecord = Define.HandCleanRecord.S2_8;
        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("오른쪽 환자가 부릅니다", Define.Views.Right_Patient);
        yield return new WaitUntil(() => guide == null);
        handCleanRecord[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        speech = Managers.UI.ShowPopupUI<SpeechBubble_Popup>();
        speech.SetText(new TextAsset("화장실 가고싶으니 소변백 처리 부탁"));
        speech.SetAnchor(GameObject.Find("RightPatientHead").transform, Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(speech);
        yield return new WaitUntil(() => speech == null);

        currentRecord = Define.HandCleanRecord.S2_9;
        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("RightBedFence").transform, Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        rightBedFenceUp.SetActive(false);
        rightBedFenceDown.SetActive(true);
        Debug.Log("오른쪽 침대 난간내림!");
        handCleanRecord[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        currentRecord = Define.HandCleanRecord.S2_10;
        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("RightBedPee").transform, Define.Views.Right_Bed_Pee);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        rightBedPeeBag.SetActive(false);
        originPole.SetActive(false);
        positionChangeIvPole.SetActive(true);
        handCleanRecord[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("오른쪽 환자가 부릅니다", Define.Views.Right_Patient);
        yield return new WaitUntil(() => guide == null);

        currentRecord = Define.HandCleanRecord.S2_11;
        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("RightPatienBody").transform, Define.Views.Right_Patient_Sit);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        originRightPatient.SetActive(false);
        sitRightPatient.SetActive(true);
        handCleanRecord[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        currentRecord = Define.HandCleanRecord.S2_12;
        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("처치를 다 했으니 퇴실하실?", Define.Views.Both);
        yield return new WaitUntil(() => guide == null);
        handCleanRecord[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        Debug.Log("평가 데이터 전송, 상황2 종료");

        for (Define.HandCleanRecord i = Define.HandCleanRecord.S2_1; i <= Define.HandCleanRecord.S2_12; i++)
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

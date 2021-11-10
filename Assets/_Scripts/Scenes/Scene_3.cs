using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scene_3 : BaseScene
{
    bool isTakeDone = false;
    public GameObject trayOnCabinet;
    public GameObject o2MaskTakeOff;
    public GameObject o2MaskTakeOn;
    public GameObject blanket;
    public GameObject notepad;
    Basic_UI ui;

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Scene_3;
        
        ui = Managers.UI.ShowSceneUI<Basic_UI>();
        Briefing_Popup briefing = Managers.UI.ShowPopupUI<Briefing_Popup>();
        TextAsset textAsset = new TextAsset("[상황제시]\n\n당신은 221호 담당간호사입니다.\n환자 1은 우측 쇄골 아래 반창고 부착 부위의 표피의 찰과상으로 피부소독이 필요한 상태입니다.\n장갑을 착용하고 드레싱을 하고 있는 상황에서 시작됩니다.\n손소독제를 누르면 손위생을 수행하는 20초 동안은 다른 활동을 해서는 안됩니다.");

        briefing.SetText(textAsset);
    }
    IEnumerator Scenario()
    {
        yield return new WaitUntil(() => Managers.UI.GetSceneUI<Basic_UI>() != null);
        ui.MoveSceneButtonOff();
        ui.SetGloveButton(true);

        currentRecord = Define.SituationCode.S3_1;
        Guide_Popup guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("***트레이를 내려놓아야 함", Define.Views.Left_Patient_RightView);
        yield return new WaitUntil(() => guide == null);

        Focusing_Popup focus = Managers.UI.MakeWorldSpaceUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftCabinet").transform, Define.Views.Left_Patient_RightView);
        needRenewPositionPopupList.Add(focus);

        yield return StartCoroutine(WaitTakeDone());
        GenerateCovid(GameObject.Find("LeftCabinet").transform, infectionState);
        infectionState |= Define.Infection.Left;
        
        GameObject.Find("tray").gameObject.SetActive(false);

        trayOnCabinet.SetActive(true);
        Debug.Log("트레이 내려놓음!");

        
        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("산소마스크가 벗겨져있음. 소독된 장갑을 벗고, 마스크를 바로 씌워주자.", Define.Views.Left_Patient_RightView_CloseUp);
        yield return new WaitUntil(() => guide == null && !ui.isGloveOn);
        Managers.Data.recordData.handCleanRecords[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;


        currentRecord = Define.SituationCode.S3_2;
        focus = Managers.UI.MakeWorldSpaceUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftPatientO2Mask").transform, Define.Views.Left_Patient_RightView_CloseUp);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        GenerateCovid(GameObject.Find("LeftPatientO2Mask").transform, infectionState);
        infectionState |= Define.Infection.Left;
        o2MaskTakeOff.SetActive(false);
        o2MaskTakeOn.SetActive(true);
        Managers.Data.recordData.handCleanRecords[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("환자 쇄골쪽을 드레싱해야한다.", Define.Views.Left_Patient_RightView_CloseUp);
        yield return new WaitUntil(() => guide == null && !ui.isGloveOn);

        currentRecord = Define.SituationCode.S3_3;
        focus = Managers.UI.MakeWorldSpaceUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftPatientBody").transform, Define.Views.Left_Patient_RightView_CloseUp);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        GenerateCovid(GameObject.Find("LeftPatientBody").transform, infectionState);
        infectionState |= Define.Infection.Left;
        blanket.SetActive(false);
        Managers.Data.recordData.handCleanRecords[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("드레싱 할 때는 소독장갑을 반드시 착용해야함.", Define.Views.Left_Patient_RightView_CloseUp);
        yield return new WaitUntil(() => guide == null && ui.isGloveOn);

        focus = Managers.UI.MakeWorldSpaceUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftPatientDressing").transform, Define.Views.Left_Patient_RightView_CloseUp);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        GenerateCovid(GameObject.Find("LeftPatientDressing").transform, infectionState);
        infectionState |= Define.Infection.Left;
        //TODO 환자의 material을 교체.
        //Tray에 의료폐기물 배치
        Debug.Log("드레싱 완료");

        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("장갑을 벗고 이불을 다시 덮어주자", Define.Views.Left_Patient_RightView);
        yield return new WaitUntil(() => guide == null && !ui.isGloveOn);

        currentRecord = Define.SituationCode.S3_4;
        focus = Managers.UI.MakeWorldSpaceUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftPatientLeg").transform, Define.Views.Left_Patient_RightView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        GenerateCovid(GameObject.Find("LeftPatientLeg").transform, infectionState);
        infectionState |= Define.Infection.Left;
        blanket.SetActive(true);
        Managers.Data.recordData.handCleanRecords[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        currentRecord = Define.SituationCode.S3_5;
        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("오른쪽 환자의 호출", Define.Views.Right_Patient);
        yield return new WaitUntil(() => guide == null && !ui.isGloveOn);
        Managers.Data.recordData.handCleanRecords[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        SpeechBubble_Popup speech = Managers.UI.MakeWorldSpaceUI<SpeechBubble_Popup>();
        speech.SetText(new TextAsset("소변 보고싶은데 잠금장치좀 풀어주소"));
        speech.SetAnchor(GameObject.Find("RightPatientHead").transform, Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(speech);
        yield return new WaitUntil(() => speech == null);

        currentRecord = Define.SituationCode.S3_6;
        focus = Managers.UI.MakeWorldSpaceUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("RightBedPee").transform, Define.Views.Right_Bed_Pee);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        GenerateCovid(GameObject.Find("RightBedPee").transform, infectionState);
        infectionState |= Define.Infection.Right;
        Debug.Log("소변 잠금장치 풀어줌");
        Managers.Data.recordData.handCleanRecords[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        currentRecord = Define.SituationCode.S3_7;
        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("소변 배액량을 확인합니까?", Define.Views.Right_Bed_Pee);
        yield return new WaitUntil(() => guide == null);
        Managers.Data.recordData.handCleanRecords[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        speech = Managers.UI.MakeWorldSpaceUI<SpeechBubble_Popup>();
        speech.SetText(new TextAsset("소변 배액량을 확인하였음! 기록지에 작성하여야함"));
        speech.SetAnchor(GameObject.Find("RightBedPee").transform, Define.Views.Right_Bed_Pee);
        needRenewPositionPopupList.Add(speech);
        yield return new WaitUntil(() => speech == null);

        currentRecord = Define.SituationCode.S3_8;
        focus = Managers.UI.MakeWorldSpaceUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("Notepad").transform, Define.Views.Right_Bed_Pee);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        GenerateCovid(GameObject.Find("Notepad").transform, infectionState);
        infectionState |= Define.Infection.Right;
        notepad.SetActive(true);
        Managers.Data.recordData.handCleanRecords[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        speech = Managers.UI.MakeWorldSpaceUI<SpeechBubble_Popup>();
        speech.SetText(new TextAsset("작성완료!"));
        speech.SetAnchor(GameObject.Find("RightBedPee").transform, Define.Views.Right_Bed_Pee);
        needRenewPositionPopupList.Add(speech);
        yield return new WaitUntil(() => speech == null);

        currentRecord = Define.SituationCode.S3_9;
        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("왼쪽 환자의 호출", Define.Views.Left_Patient_RightView);
        yield return new WaitUntil(() => guide == null);
        Managers.Data.recordData.handCleanRecords[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        speech = Managers.UI.MakeWorldSpaceUI<SpeechBubble_Popup>();
        speech.SetText(new TextAsset("이것들 가져가셔야죠?"));
        speech.SetAnchor(GameObject.Find("LeftPatientHead").transform, Define.Views.Left_Patient_RightView);
        needRenewPositionPopupList.Add(speech);
        yield return new WaitUntil(() => speech == null);

        currentRecord = Define.SituationCode.S3_10;
        focus = Managers.UI.MakeWorldSpaceUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftCabinet").transform, Define.Views.Left_Patient_RightView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        GenerateCovid(GameObject.Find("LeftCabinet").transform, infectionState);
        infectionState |= Define.Infection.Left;
        trayOnCabinet.SetActive(false);
        Debug.Log("의료폐기물이 든 트레이를 챙김!");
        Managers.Data.recordData.handCleanRecords[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        currentRecord = Define.SituationCode.S3_11;
        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("모든 처치를 완료하였음. 퇴실합니까?", Define.Views.Both);
        yield return new WaitUntil(() => guide == null);
        GenerateCovid(GameObject.Find("RightHand").transform, infectionState, true);
        GenerateCovid(GameObject.Find("LeftHand").transform, infectionState, true);
        Managers.Data.recordData.handCleanRecords[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        Debug.Log("평가 데이터 전송, 상황3 종료");

        Debriefing_Popup debriefing = Managers.UI.ShowPopupUI<Debriefing_Popup>();
        yield return new WaitUntil(() => debriefing == null);
        BehaviourAnalysisTable_Popup analysis = Managers.UI.ShowPopupUI<BehaviourAnalysisTable_Popup>();
        yield return new WaitUntil(() => analysis == null);

        HandWait_Popup waitPopup = Managers.UI.ShowPopupUI<HandWait_Popup>();
        yield return new WaitUntil(() => IsHandOn());
        waitPopup.ClosePopupUI();
        ShowCovid();
        ui.MoveSceneButtonOn();

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

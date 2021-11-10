using System.Collections;
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
    public GameObject blanket;

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

        ui = Managers.UI.ShowSceneUI<Basic_UI>();
        Briefing_Popup briefing = Managers.UI.ShowPopupUI<Briefing_Popup>();
        string textAsset = "[상황제시]\n\n상황2에 대한 브리핑 UI입니다.";

        briefing.SetText(textAsset);
    }
    IEnumerator Scenario()
    {
        yield return new WaitUntil(() => Managers.UI.GetSceneUI<Basic_UI>() != null);
        ui.MoveSceneButtonOff();
        ui.SetGloveButton(false);

        Guide_Popup guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("***트레이를 내려놓아야 함", Define.Views.Left_Patient_RightView);
        yield return new WaitUntil(() => guide == null);

        currentRecord = Define.SituationCode.S2_1;
        Focusing_Popup focus = Managers.UI.MakeWorldSpaceUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftBedTable").transform, Define.Views.Left_Patient_RightView);
        needRenewPositionPopupList.Add(focus);

        yield return StartCoroutine(WaitTakeDone());
        GenerateCovid(GameObject.Find("LeftBedTable").transform, infectionState);
        infectionState |= Define.Infection.Left;
  
        GameObject.Find("tray").gameObject.SetActive(false);

        trayOnTable.SetActive(true);
        Managers.Data.recordData.handCleanRecords[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;
        Debug.Log("트레이 내려놓음!");

        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("새로운 수액으로 교체해주자!", Define.Views.Left_Patient_RightView);
        yield return new WaitUntil(() => guide == null);

        
        focus = Managers.UI.MakeWorldSpaceUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftBedTable").transform, Define.Views.Left_Patient_RightView);
        needRenewPositionPopupList.Add(focus);

        yield return StartCoroutine(WaitTakeDone());
        GenerateCovid(GameObject.Find("LeftBedTable").transform, infectionState);
        infectionState |= Define.Infection.Left;
        trayOnFluid.SetActive(false);
        Debug.Log("수액 집어듬!");
        

        currentRecord = Define.SituationCode.S2_2;
        focus = Managers.UI.MakeWorldSpaceUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftEmptyFluid").transform, Define.Views.Left_Patient_Stand);
        needRenewPositionPopupList.Add(focus);

        yield return StartCoroutine(WaitTakeDone());
        GenerateCovid(GameObject.Find("LeftEmptyFluid").transform, infectionState);
        infectionState |= Define.Infection.Left;
        originIv.SetActive(false);
        newIv.SetActive(true);
        Managers.Data.recordData.handCleanRecords[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        SpeechBubble_Popup speech = Managers.UI.MakeWorldSpaceUI<SpeechBubble_Popup>();
        speech.SetText(new TextAsset("수액 교체완료"));
        speech.SetAnchor(GameObject.Find("LeftPump").transform, Define.Views.Left_Patient_Stand);
        needRenewPositionPopupList.Add(speech);
        yield return new WaitUntil(() => speech == null);

        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("정맥 펌프를 끄세요", Define.Views.Left_Patient_Stand);
        yield return new WaitUntil(() => guide == null);

        currentRecord = Define.SituationCode.S2_3;
        focus = Managers.UI.MakeWorldSpaceUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftPump").transform, Define.Views.Left_Patient_Stand);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        GenerateCovid(GameObject.Find("LeftPump").transform, infectionState);
        infectionState |= Define.Infection.Left;
        Managers.Data.recordData.handCleanRecords[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        speech = Managers.UI.MakeWorldSpaceUI<SpeechBubble_Popup>();
        speech.SetText(new TextAsset("정맥펌프 끔"));
        speech.SetAnchor(GameObject.Find("LeftEmptyFluid").transform, Define.Views.Left_Patient_Stand);
        needRenewPositionPopupList.Add(speech);
        yield return new WaitUntil(() => speech == null);

        currentRecord = Define.SituationCode.S2_4;
        focus = Managers.UI.MakeWorldSpaceUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftBedFence").transform, Define.Views.Left_Patient_LeftView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        GenerateCovid(GameObject.Find("LeftBedFence").transform, infectionState);
        infectionState |= Define.Infection.Left;
        leftBedFenceUp.SetActive(false);
        leftBedFenceDown.SetActive(true);
        Debug.Log("난간을 내림");
        Managers.Data.recordData.handCleanRecords[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        currentRecord = Define.SituationCode.S2_5;
        focus = Managers.UI.MakeWorldSpaceUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftPatientBody").transform, Define.Views.Left_Patient_LeftView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        blanket.SetActive(false);
        GenerateCovid(GameObject.Find("LeftPatientBody").transform, infectionState);
        infectionState |= Define.Infection.Left;
        Managers.Data.recordData.handCleanRecords[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        currentRecord = Define.SituationCode.S2_6;
        focus = Managers.UI.MakeWorldSpaceUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftPatientArm").transform, Define.Views.Left_Patient_LeftView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        originIvLine.SetActive(false);
        Debug.Log("기존 줄 제거!");
        GenerateCovid(GameObject.Find("LeftPatientArm").transform, infectionState);
        infectionState |= Define.Infection.Left;
        Managers.Data.recordData.handCleanRecords[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        focus = Managers.UI.MakeWorldSpaceUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftBedTable").transform, Define.Views.Left_Patient_LeftView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        GenerateCovid(GameObject.Find("LeftBedTable").transform, infectionState);
        infectionState |= Define.Infection.Left;
        trayOnNewLine.SetActive(false);
        Debug.Log("새로운 줄 집어듬!");


        focus = Managers.UI.MakeWorldSpaceUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftPatientArm").transform, Define.Views.Left_Patient_LeftView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        GenerateCovid(GameObject.Find("LeftPatientArm").transform, infectionState);
        infectionState |= Define.Infection.Left;
        newIvLine.SetActive(true);
        Debug.Log("새로운 줄 장착!");

        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("정맥 펌프를 다시 켜세요", Define.Views.Left_Patient_Stand);
        yield return new WaitUntil(() => guide == null);

        currentRecord = Define.SituationCode.S2_7;
        focus = Managers.UI.MakeWorldSpaceUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftPump").transform, Define.Views.Left_Patient_Stand);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        GenerateCovid(GameObject.Find("LeftPump").transform, infectionState);
        infectionState |= Define.Infection.Left;
        Debug.Log("정맥주입펌프 재가동!");
        Managers.Data.recordData.handCleanRecords[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        currentRecord = Define.SituationCode.S2_8;
        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("오른쪽 환자가 부릅니다", Define.Views.Right_Patient);
        yield return new WaitUntil(() => guide == null);
        Managers.Data.recordData.handCleanRecords[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        speech = Managers.UI.MakeWorldSpaceUI<SpeechBubble_Popup>();
        speech.SetText(new TextAsset("화장실 가고싶으니 소변백 처리 부탁"));
        speech.SetAnchor(GameObject.Find("RightPatientHead").transform, Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(speech);
        yield return new WaitUntil(() => speech == null);

        currentRecord = Define.SituationCode.S2_9;
        focus = Managers.UI.MakeWorldSpaceUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("RightBedFence").transform, Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        GenerateCovid(GameObject.Find("RightBedFence").transform, infectionState);
        infectionState |= Define.Infection.Right;
        rightBedFenceUp.SetActive(false);
        rightBedFenceDown.SetActive(true);
        Debug.Log("오른쪽 침대 난간내림!");
        Managers.Data.recordData.handCleanRecords[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        currentRecord = Define.SituationCode.S2_10;
        focus = Managers.UI.MakeWorldSpaceUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("RightBedPee").transform, Define.Views.Right_Bed_Pee);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        GenerateCovid(GameObject.Find("RightBedPee").transform, infectionState);
        infectionState |= Define.Infection.Right;
        rightBedPeeBag.SetActive(false);
        originPole.SetActive(false);
        positionChangeIvPole.SetActive(true);
        Managers.Data.recordData.handCleanRecords[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("환자를 천천히 일으켜주세요", Define.Views.Right_Patient);
        yield return new WaitUntil(() => guide == null);

        currentRecord = Define.SituationCode.S2_11;
        focus = Managers.UI.MakeWorldSpaceUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("RightPatientBody").transform, Define.Views.Right_Patient_Sit);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        GenerateCovid(GameObject.Find("RightPatientBody").transform, infectionState);
        infectionState |= Define.Infection.Right;
        originRightPatient.SetActive(false);
        sitRightPatient.SetActive(true);
        Managers.Data.recordData.handCleanRecords[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        currentRecord = Define.SituationCode.S2_12;
        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("모든 처치를 완료하였음. 퇴실합니까?", Define.Views.Both);
        yield return new WaitUntil(() => guide == null);
        GenerateCovid(GameObject.Find("RightHand").transform, infectionState, true);
        GenerateCovid(GameObject.Find("LeftHand").transform, infectionState, true);
        Managers.Data.recordData.handCleanRecords[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        Debug.Log("평가 데이터 전송, 상황2 종료");

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

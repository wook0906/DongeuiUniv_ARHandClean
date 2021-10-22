using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scene_4 : BaseScene
{
    bool isTakeDone = false;
    public GameObject bedDeskUp;
    public GameObject bedDeskDown;
    public GameObject trayOnTable;
    public GameObject rightPatientArmDown;
    public GameObject rightPatientArmUp;
    public GameObject checker;
    public GameObject mergedChecker;
    public GameObject checkerOnFinger;
    public GameObject cottonOnTray;
    public GameObject cottonOnFinger;
    public GameObject needleOnTray;
    public GameObject needleOnFinger;
    public GameObject blood;

    public GameObject leftBedTray;
    public GameObject leftBedTableDown;
    public GameObject leftBedTableUp;
    public GameObject originLeftPatient;
    public GameObject toothLeftPatient;
    public GameObject toothBrushOnTray;
    public GameObject toothPasteOnTray;

    Basic_UI ui;

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Scene_4;

        ui = Managers.UI.ShowSceneUI<Basic_UI>();
        Briefing_Popup briefing = Managers.UI.ShowPopupUI<Briefing_Popup>();
        TextAsset textAsset = new TextAsset("[상황제시]\n\n당신은 이 병실의 담당간호사입니다.\n환자 2에게 혈당검사를 한 후 환자 1에게 칫솔을 이용하여 구강간호를 시행해야 하는 상황에서 시작합니다.\n손소독제를 누르면 손위생을 수행하는 20초 동안은 다른 활동을 해서는 안됩니다. ");

        briefing.SetText(textAsset);
    }
    IEnumerator Scenario()
    {
        yield return new WaitUntil(() => Managers.UI.GetSceneUI<Basic_UI>() != null);
        ui.SetGloveButton(false);

        Guide_Popup guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("***침대 테이블을 올려서 트레이를 내려놓자", Define.Views.Right_Patient);
        yield return new WaitUntil(() => guide == null);

        Focusing_Popup focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("RightBedTableDown").transform, Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        bedDeskUp.SetActive(true);
        bedDeskDown.SetActive(false);

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("RightBedTable").transform, Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        trayOnTable.SetActive(true);
        foreach (var item in ui.camList)
        {
            item.transform.Find("tray").gameObject.SetActive(false);
        }
        Debug.Log("트레이 내려놓음!");

        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("환자의 혈당을 측정해라.", Define.Views.Right_Patient);
        yield return new WaitUntil(() => guide == null);

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("RightPatientRightArm").transform, Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        rightPatientArmUp.SetActive(true);
        rightPatientArmDown.SetActive(false);


        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("채혈기 세팅하고 채혈하세요.", Define.Views.Right_Patient);
        yield return new WaitUntil(() => guide == null);

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("RightBedTable").transform, Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        checker.SetActive(false);
        mergedChecker.SetActive(true);

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("RightBedTable").transform, Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        cottonOnTray.SetActive(false);

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("RightPatientFinger").transform, Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        cottonOnFinger.SetActive(true);

        yield return new WaitForSeconds(1f);
        cottonOnFinger.SetActive(false);

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("RightBedTable").transform, Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        needleOnTray.SetActive(false);

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("RightPatientFinger").transform, Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        needleOnFinger.SetActive(true);

        yield return new WaitForSeconds(1f);
        blood.SetActive(true);
        needleOnFinger.SetActive(false);


        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("RightBedTable").transform, Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        mergedChecker.SetActive(false);

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("RightPatientFinger").transform, Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        checkerOnFinger.SetActive(true);
        yield return new WaitForSeconds(1f);
        checkerOnFinger.SetActive(false);

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("RightPatientFinger").transform, Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        cottonOnFinger.SetActive(true);
        blood.SetActive(false);
        
        yield return new WaitForSeconds(1f);
        cottonOnFinger.SetActive(false);
        rightPatientArmUp.SetActive(false);
        rightPatientArmDown.SetActive(true);

        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("채혈은 완료됐고, 채혈침을 버렸다. 혈당기록지에 작성하자.", Define.Views.Right_Patient);
        yield return new WaitUntil(() => guide == null);

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("Notepad").transform, Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());

        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("혈당지에 기록완료, 이제 왼쪽환자의 구강간호를 하자.", Define.Views.Left_Patient_RightView);
        yield return new WaitUntil(() => guide == null);

        foreach (var item in ui.camList)
        {
            item.transform.Find("tray").gameObject.SetActive(true);
        }

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftBedTableDown").transform, Define.Views.Left_Patient_RightView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        leftBedTableDown.SetActive(false);
        leftBedTableUp.SetActive(true);

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftBedTable").transform, Define.Views.Left_Patient_RightView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        leftBedTray.SetActive(true);
        foreach (var item in ui.camList)
        {
            item.transform.Find("tray").gameObject.SetActive(false);
        }
        ui.SetGloveButton(true);

        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("장갑을 착용하고, 트레이의 도구를 이용해 구강간호를 실시하자.", Define.Views.Left_Patient_RightView);
        yield return new WaitUntil(() => guide == null && ui.isGloveOn);

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftBedTable").transform, Define.Views.Left_Patient_RightView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        toothBrushOnTray.SetActive(false);
        toothPasteOnTray.SetActive(false);

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftPatientMouth").transform, Define.Views.Left_Patient_RightView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        originLeftPatient.SetActive(false);
        toothLeftPatient.SetActive(true);

        yield return new WaitForSeconds(2f);

        toothLeftPatient.SetActive(false);
        originLeftPatient.SetActive(true);
        toothBrushOnTray.SetActive(true);
        toothPasteOnTray.SetActive(true);

        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("장갑을 벗고, 환자 주변을 정리한 후에 퇴실합시다.", Define.Views.Left_Patient_RightView);
        yield return new WaitUntil(() => guide == null && !ui.isGloveOn);

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftBedTable").transform, Define.Views.Left_Patient_RightView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        leftBedTray.SetActive(false);

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftBedTable").transform, Define.Views.Left_Patient_RightView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        leftBedTableUp.SetActive(false);
        leftBedTableDown.SetActive(true);

        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("모든 처치를 완료하였음. 퇴실하실?", Define.Views.Both);
        yield return new WaitUntil(() => guide == null);

        Debug.Log("평가 데이터 전송, 상황4 종료");
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

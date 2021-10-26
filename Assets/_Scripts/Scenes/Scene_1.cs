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
        TextAsset textAsset = new TextAsset("[��Ȳ����]\n\n����� ���̴� ������ ��簣ȣ���Դϴ�.\nȯ��1�� 2�ð�����  ü���� �����ϰ� �ֽ��ϴ�.\nȯ��1�� ü���� �����ϱ� ���� ���Ƿ� ���鼭 ��Ȳ�� ���۵˴ϴ�.\n�ռҵ����� ������ �������� �����ϴ�\n20�� ������ �ٸ� Ȱ���� �ؼ��� �ȵ˴ϴ�.");

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
        guide.SetInfo("***ȯ���ʿ��� �˶��� �︮���ִ�!", Define.Views.Right_Patient);
        yield return new WaitUntil(() => guide == null);

        currentRecord = Define.HandCleanRecord.S1_1;

        Focusing_Popup focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("MonitorAlarm").transform, Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(focus);
        
        yield return StartCoroutine(WaitTakeDone());
        handCleanRecord[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;
        Debug.Log("����� �˶� ����!");

        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("***ȯ���� �����ȭ�� �����Ⱑ �����ִ�!",Define.Views.Right_Patient);
        yield return new WaitUntil(() => guide == null);

        currentRecord = Define.HandCleanRecord.S1_2;

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("RightBedFence").transform, Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(focus);

        yield return StartCoroutine(WaitTakeDone());
        bedFenceUp.SetActive(false);
        bedFenceDown.SetActive(true);
        Debug.Log("�潺 ������!");
        handCleanRecord[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        currentRecord = Define.HandCleanRecord.S1_3;

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("OxygenChecker").transform , Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(focus);

        yield return StartCoroutine(WaitTakeDone());
        oxygenChecker.transform.position = oxygenCheckerTargetTransform.position;
        Debug.Log("�����ȭ�� ������ ����ġ ��!");
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
        guide.SetInfo("***ȯ���� ȣ��!", Define.Views.Left_Patient_RightView);
        yield return new WaitUntil(() => guide == null);
        handCleanRecord[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        SpeechBubble_Popup speech = Managers.UI.ShowPopupUI<SpeechBubble_Popup>();
        speech.SetText(new TextAsset("ħ�� �� �÷��ּ���"));
        speech.SetAnchor(GameObject.Find("LeftPatientHead").transform, Define.Views.Left_Patient_RightView);
        needRenewPositionPopupList.Add(speech);
        yield return new WaitUntil(() => speech == null);

        
        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftBedLever").transform, Define.Views.Left_BedLever);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        Debug.Log("���� ȯ���� ħ�븦 �÷���!");
        

        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("ȯ���� ü���� �ٲ�����.", Define.Views.Left_Patient_RightView);
        yield return new WaitUntil(() => guide == null);

        currentRecord = Define.HandCleanRecord.S1_6;
        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("RightPillow").transform, Define.Views.Left_Patient_RightView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        Debug.Log("���������� ���ƴ����ִ� ȯ�� ��ִ� ���Ը� ġ��!");
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
        Debug.Log("ȯ�ڸ� �������� ���� ����!");
        originLeftPatient.SetActive(false);
        leftLyingPatient.SetActive(true);
        handCleanRecord[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;


        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("���ʿ� ������ ��������.", Define.Views.Left_Patient_LeftView);
        yield return new WaitUntil(() => guide == null);

        currentRecord = Define.HandCleanRecord.S1_8;
        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftPillow").transform, Define.Views.Left_Patient_LeftView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        Debug.Log("���Ը� � ���� �����!");
        pillowLeft.SetActive(true);
        handCleanRecord[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        currentRecord = Define.HandCleanRecord.S1_9;
        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftPatientBody").transform, Define.Views.Left_Patient_LeftView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        Debug.Log("�̺� ������!");
        leftBedBlanket.SetActive(true);
        handCleanRecord[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        currentRecord = Define.HandCleanRecord.S1_10;
        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("óġ�� �� ������ ����Ͻ�?", Define.Views.Both);
        yield return new WaitUntil(() => guide == null);
        handCleanRecord[currentRecord] = isDidCleanHand;
        isDidCleanHand = false;

        Debug.Log("�� ������ ����, ��Ȳ1 ����");

        for (Define.HandCleanRecord i = Define.HandCleanRecord.S1_1; i <= Define.HandCleanRecord.S1_10; i++)
        {
            Debug.Log($"�Է� : {handCleanRecord[i]}, �� : {Managers.Data.answerSheetData.answerDict[i]}");
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

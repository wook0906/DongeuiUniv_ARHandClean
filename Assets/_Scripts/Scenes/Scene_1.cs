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
        TextAsset textAsset = new TextAsset("[��Ȳ����]\n\n����� ���̴� ������ ��簣ȣ���Դϴ�.\nȯ��1�� 2�ð�����  ü���� �����ϰ� �ֽ��ϴ�.\nȯ��1�� ü���� �����ϱ� ���� ���Ƿ� ���鼭 ��Ȳ�� ���۵˴ϴ�.\n�ռҵ����� ������ �������� �����ϴ�\n20�� ������ �ٸ� Ȱ���� �ؼ��� �ȵ˴ϴ�.");
 
        briefing.SetText(textAsset);
    }
    IEnumerator Scenario()
    {
        yield return new WaitUntil(() => Managers.UI.GetSceneUI<Basic_UI>() != null);
        ui.SetGloveButton(false);

        Guide_Popup guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("***ȯ���ʿ��� �˶��� �︮���ִ�!", Define.Views.Right_Patient);
        yield return new WaitUntil(() => guide == null);

        Focusing_Popup focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("MonitorAlarm").transform, Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(focus);
        
        yield return StartCoroutine(WaitTakeDone());
        Debug.Log("����� �˶� ����!");

        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("***ȯ���� �����ȭ�� �����Ⱑ �����ִ�!",Define.Views.Right_Patient);
        yield return new WaitUntil(() => guide == null);

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("RightBedFence").transform, Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(focus);

        yield return StartCoroutine(WaitTakeDone());
        bedFenceUp.SetActive(false);
        bedFenceDown.SetActive(true);
        Debug.Log("�潺 ������!");


        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("OxygenChecker").transform , Define.Views.Right_Patient);
        needRenewPositionPopupList.Add(focus);

        yield return StartCoroutine(WaitTakeDone());
        oxygenChecker.transform.position = oxygenCheckerTargetTransform.position;
        Debug.Log("�����ȭ�� ������ ����ġ ��!");

        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("***ȯ���� ȣ��!", Define.Views.Left_Patient_RightView);
        yield return new WaitUntil(() => guide == null);

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

        //�̶� ȯ�ڴ� ���������� ���ƴ����־����.
        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftPatientBody").transform, Define.Views.Left_Patient_RightView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        Debug.Log("���������� ���ƴ����ִ� ȯ�� ��ִ� ���Ը� ġ��!");
        //�̶� ȯ�ڴ� �ٷ� ����������.

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftPatientBody").transform, Define.Views.Left_Patient_RightView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        Debug.Log("ȯ�ڸ� �������� ���� ����!");

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftPatientBody").transform, Define.Views.Left_Patient_RightView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        Debug.Log("���Ը� � ���� �����!");

        focus = Managers.UI.ShowPopupUI<Focusing_Popup>();
        yield return new WaitUntil(() => focus);
        focus.SetAnchor(GameObject.Find("LeftPatientBody").transform, Define.Views.Left_Patient_RightView);
        needRenewPositionPopupList.Add(focus);
        yield return StartCoroutine(WaitTakeDone());
        Debug.Log("�̺� ������!");

        guide = Managers.UI.ShowPopupUI<Guide_Popup>();
        guide.SetInfo("óġ�� �� ������ ����Ͻ�?", Define.Views.Left_Patient_RightView);
        yield return new WaitUntil(() => guide == null);

        Debug.Log("�� ������ ����, ��Ȳ1 ����");
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

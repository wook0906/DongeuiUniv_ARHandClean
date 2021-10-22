using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble_Popup : UIPopup
{
    [SerializeField]
    Transform anchor;
    [SerializeField]
    Define.Views showView;
    Image bg;
    Text speechText;
    bool isInit = false;

    BaseScene curScene;

    enum Images
    {
        BG,
    }
    enum Texts
    {
        Speech_Text
    }
    // Start is called before the first frame update


    public override void Init()
    {
        base.Init();
        curScene = GameObject.Find("@Scene").GetComponent<BaseScene>();

        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        speechText = Get<Text>((int)Texts.Speech_Text);
        bg = Get<Image>((int)Images.BG);
        isInit = true;
    }
    public void SetText(TextAsset text)
    {
        StartCoroutine(CorSetText(text));
    }
    IEnumerator CorSetText(TextAsset text)
    {
        yield return new WaitUntil(() => isInit);
        speechText.text = text.text;
    }
    public void SetAnchor(Transform anchor, Define.Views showView)
    {
        this.anchor = anchor;
        this.showView = showView;
        RenewFocusPostion();
    }
    public override void RenewFocusPostion()
    {
        StartCoroutine(CorRenewFocusPosition());
    }
    IEnumerator CorRenewFocusPosition()
    {
        yield return new WaitUntil(() => isInit);
        if (Managers.UI.GetSceneUI<Basic_UI>().CurrentView == showView)
        {
            bg.enabled = true;
            speechText.enabled = true;
            bg.transform.position = Managers.UI.GetSceneUI<Basic_UI>().currentCam.WorldToScreenPoint(anchor.transform.position);
            StartCoroutine(ShowText());
        }
        else
        {
            bg.enabled = false;
            speechText.enabled = false;
        }
    }

    IEnumerator ShowText()
    {
        yield return new WaitForSeconds(3f);
        curScene.needRenewPositionPopupList.Remove(this);
        ClosePopupUI();
    }
}

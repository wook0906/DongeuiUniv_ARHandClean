using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble_Popup : UIBase
{
    [SerializeField]
    Transform anchor;
    [SerializeField]
    Define.Views showView;
    Image bg;
    Text speechText;
    bool isInit = false;

    BaseScene curScene;

    GameObject ARCam;

    enum Images
    {
        BG,
    }
    enum Texts
    {
        Speech_Text
    }
    // Start is called before the first frame update

    private void Update()
    {
        if(bg)
            bg.transform.rotation = Camera.main.transform.rotation;
    }
    public override void Init()
    {
       
        curScene = GameObject.Find("@Scene").GetComponent<BaseScene>();
        ARCam = GameObject.Find("ARCamera");

        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        speechText = Get<Text>((int)Texts.Speech_Text);
        bg = Get<Image>((int)Images.BG);
        isInit = true;

        transform.localScale *= Vector3.Distance(ARCam.transform.position, this.transform.position);
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
            bg.transform.position = anchor.position;//Managers.UI.GetSceneUI<Basic_UI>().ARCam.WorldToScreenPoint(anchor.transform.position);
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
        Destroy(this.gameObject);
    }
}

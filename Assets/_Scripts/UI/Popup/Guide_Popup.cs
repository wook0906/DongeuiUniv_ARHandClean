using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guide_Popup : UIPopup
{
   [SerializeField]
    Define.Views targetView;

    Button btn;

    public Text guide;
    bool isInit = false;

    enum Texts
    {
        Guide_Text
    }
    enum Buttons
    {
        Ok_Button,
    }
    
    // Start is called before the first frame update


    public override void Init()
    {
        base.Init();
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        
        btn = Get<Button>((int)Buttons.Ok_Button);
        btn.onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            MoveToTargetView();
            ClosePopupUI();
        }));

        guide = Get<Text>((int)Texts.Guide_Text);
        isInit = true;
    }
    public void SetInfo(string guideText, Define.Views targetView)
    {
        StartCoroutine(CorSetInfo(guideText, targetView));
    }
    IEnumerator CorSetInfo(string guideText, Define.Views targetView)
    {
        yield return new WaitUntil(() => isInit);
        guide.text = guideText;
        if (targetView == Define.Views.None) yield break;
        this.targetView = targetView;
    }

    void MoveToTargetView()
    {
        Managers.UI.GetSceneUI<Basic_UI>().MoveView(targetView);
    }
}

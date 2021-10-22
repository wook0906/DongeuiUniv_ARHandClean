using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Focusing_Popup : UIPopup
{
    Transform anchor;
    BaseScene curScene;
    Define.Views showView;

    Button btn;

    bool isInit = false;
    
    enum Buttons
    {
        FocusIcon_Button
    }
    // Start is called before the first frame update


    public override void Init()
    {
        base.Init();
        curScene = GameObject.Find("@Scene").GetComponent<BaseScene>();
        Bind<Button>(typeof(Buttons));
        btn = Get<Button>((int)Buttons.FocusIcon_Button);
        btn.onClick.AddListener(() => {
            curScene.TakeDoneCallback();
            curScene.needRenewPositionPopupList.Remove(this);
            ClosePopupUI();
        });
        isInit = true;
    }
    public void SetAnchor(Transform anchor, Define.Views showView)
    {
        this.anchor = anchor;
        this.showView = showView;
        RenewFocusPostion();
    }
    
    public override void RenewFocusPostion()
    {
        if (Managers.UI.GetSceneUI<Basic_UI>().CurrentView == showView)
        {
            btn.GetComponent<Image>().enabled = true;
            btn.transform.position = Managers.UI.GetSceneUI<Basic_UI>().currentCam.WorldToScreenPoint(anchor.transform.position);
        }
        else
            btn.GetComponent<Image>().enabled = false;
    }
}

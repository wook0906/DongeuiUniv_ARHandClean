using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Analysis_Popup : UIPopup
{
    enum Buttons
    {
        BG,
    }
    enum Texts
    {
        Analysis_Text
    }
    public override void Init()
    {
        base.Init();
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.BG).onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            ClosePopupUI();
        }));
    }
    public void SetText(TextAsset textAsset)
    {
        GetText((int)Texts.Analysis_Text).text = textAsset.text;
    }
}

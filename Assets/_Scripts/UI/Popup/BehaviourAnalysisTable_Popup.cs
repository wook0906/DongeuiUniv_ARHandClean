using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BehaviourAnalysisTable_Popup : UIPopup
{
    bool isInit = false;

    enum Texts
    {
        HandCleanPercent_Text,
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

        Get<Button>((int)Buttons.Ok_Button).onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            ClosePopupUI();
        }));
        isInit = true;
    }
}

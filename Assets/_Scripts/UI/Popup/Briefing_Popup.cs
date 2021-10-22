using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Briefing_Popup : UIPopup
{
    BaseScene curScene;
    Button btn;
    bool isInit = false;

    enum Texts
    {
        Briefing_Content,
    }
    enum Buttons
    {
        Ok_Button
    }
    // Start is called before the first frame update


    public override void Init()
    {
        base.Init();
        curScene = GameObject.Find("@Scene").GetComponent<BaseScene>();
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));

        Get<Text>((int)Texts.Briefing_Content);

        btn = Get<Button>((int)Buttons.Ok_Button);
        btn.onClick.AddListener(() => {
            curScene.StartScenario();
            ClosePopupUI();
        });
        isInit = true;
    }

    public void SetText(TextAsset textAsset)
    {
        StartCoroutine(CorSetText(textAsset));
    }
    IEnumerator CorSetText(TextAsset textAsset)
    {
        yield return new WaitUntil(() => isInit);
        Get<Text>((int)Texts.Briefing_Content).text = textAsset.text;
    }
   
}

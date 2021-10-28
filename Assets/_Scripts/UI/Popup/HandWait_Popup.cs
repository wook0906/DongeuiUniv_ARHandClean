using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandWait_Popup : UIPopup
{
    [SerializeField]
    Transform anchor;
    [SerializeField]
    Define.Views showView;

    bool isInit = false;

    Text content;
    BaseScene curScene;

    enum Images
    {
        BlockBG,
    }
    enum Texts
    {
        Content_Text
    }
    // Start is called before the first frame update


    public override void Init()
    {
        base.Init();
        curScene = GameObject.Find("@Scene").GetComponent<BaseScene>();

        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        Get<Text>((int)Texts.Content_Text).text = "양손의 QR코드를 인식시켜 주세요";
        isInit = true;
    }
}

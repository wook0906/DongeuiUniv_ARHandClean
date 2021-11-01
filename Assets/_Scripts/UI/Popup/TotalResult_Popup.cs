using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalResult_Popup : UIPopup
{
    enum Texts
    {
        TotalRate_Text,
    }
    enum Buttons
    {
        Ok_Button,
    }

    int correctCnt = 0;
    public override void Init()
    {
        base.Init();
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        for (Define.HandCleanRecord i = Define.HandCleanRecord.S1_1; i <= Define.HandCleanRecord.S4_14; i++)
        {
            if (Managers.Data.recordData.handCleanRecords[i])
                correctCnt++;
        }

        Get<Text>((int)Texts.TotalRate_Text).text = string.Format("종합 손위생\n 이행률\n : ({0:0.#})%", ((float)correctCnt / Managers.Data.recordData.handCleanRecords.Count) * 100);
        Get<Button>((int)Buttons.Ok_Button).onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            Managers.Scene.LoadScene(Define.Scene.StartScene);
        }));

    }
}

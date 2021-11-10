using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debriefing_Popup : UIPopup
{
    enum Texts
    {
        HandCleanPercent_Text,
    }
    enum Buttons
    {
        Ok_Button,
    }

    int totalCnt = 0;
    int correctCnt = 0;
    public override void Init()
    {
        base.Init();
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        switch (Managers.Scene.currentScene.SceneType)
        {
            case Define.Scene.Scene_1:
                totalCnt = 10;
                for (Define.SituationCode i = Define.SituationCode.S1_1; i <= Define.SituationCode.S1_10; i++)
                {
                    if (Managers.Data.recordData.handCleanRecords[i] == Managers.Data.answerSheetData.answerDict[i])
                        correctCnt++;
                }
                break;
            case Define.Scene.Scene_2:
                totalCnt = 12;
                for (Define.SituationCode i = Define.SituationCode.S2_1; i <= Define.SituationCode.S2_12; i++)
                {
                    if (Managers.Data.recordData.handCleanRecords[i] == Managers.Data.answerSheetData.answerDict[i])
                        correctCnt++;
                }
                break;
            case Define.Scene.Scene_3:
                totalCnt = 11;
                for (Define.SituationCode i = Define.SituationCode.S3_1; i <= Define.SituationCode.S3_11; i++)
                {
                    if (Managers.Data.recordData.handCleanRecords[i] == Managers.Data.answerSheetData.answerDict[i])
                        correctCnt++;
                }
                break;
            case Define.Scene.Scene_4:
                totalCnt = 14;
                for (Define.SituationCode i = Define.SituationCode.S4_1; i <= Define.SituationCode.S4_14; i++)
                {
                    if (Managers.Data.recordData.handCleanRecords[i] == Managers.Data.answerSheetData.answerDict[i])
                        correctCnt++;
                }
                break;
            default:
                break;
        }

        //Debug.Log($"corCnt : {correctCnt}, total : {totalCnt}");
        Get<Text>((int)Texts.HandCleanPercent_Text).text = string.Format("손위생 이행률 : {0:0.#}%", ((float)correctCnt / totalCnt) * 100);
        Get<Button>((int)Buttons.Ok_Button).onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            ClosePopupUI();
        }));

    }
}

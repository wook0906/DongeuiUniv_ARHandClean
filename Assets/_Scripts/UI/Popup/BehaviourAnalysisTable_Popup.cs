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

        switch (Managers.Scene.currentScene.SceneType)
        {
            case Define.Scene.Scene_1:
                for (Define.HandCleanRecord i = Define.HandCleanRecord.S1_1; i <= Define.HandCleanRecord.S1_10; i++)
                {
                    GameObject.Find(i.ToString()).transform.Find("Input").GetComponent<Text>().text = Managers.Data.recordData.handCleanRecords[i] ? "O" : "X";
                    GameObject.Find(i.ToString()).transform.Find("Check").GetComponent<Text>().text = Managers.Data.recordData.handCleanRecords[i] == Managers.Data.answerSheetData.answerDict[i] ? "O" : "X";
                }
                break;
            case Define.Scene.Scene_2:
                for (Define.HandCleanRecord i = Define.HandCleanRecord.S2_1; i <= Define.HandCleanRecord.S2_12; i++)
                {
                    GameObject.Find(i.ToString()).transform.Find("Input").GetComponent<Text>().text = Managers.Data.recordData.handCleanRecords[i] ? "O" : "X";
                    GameObject.Find(i.ToString()).transform.Find("Check").GetComponent<Text>().text = Managers.Data.recordData.handCleanRecords[i] == Managers.Data.answerSheetData.answerDict[i] ? "O" : "X";
                }
                break;
            case Define.Scene.Scene_3:
                for (Define.HandCleanRecord i = Define.HandCleanRecord.S3_1; i <= Define.HandCleanRecord.S3_11; i++)
                {
                    GameObject.Find(i.ToString()).transform.Find("Input").GetComponent<Text>().text = Managers.Data.recordData.handCleanRecords[i] ? "O" : "X";
                    GameObject.Find(i.ToString()).transform.Find("Check").GetComponent<Text>().text = Managers.Data.recordData.handCleanRecords[i] == Managers.Data.answerSheetData.answerDict[i] ? "O" : "X";
                }
                break;
            case Define.Scene.Scene_4:
                for (Define.HandCleanRecord i = Define.HandCleanRecord.S4_1; i <= Define.HandCleanRecord.S4_14; i++)
                {
                    GameObject.Find(i.ToString()).transform.Find("Input").GetComponent<Text>().text = Managers.Data.recordData.handCleanRecords[i] ? "O" : "X";
                    GameObject.Find(i.ToString()).transform.Find("Check").GetComponent<Text>().text = Managers.Data.recordData.handCleanRecords[i] == Managers.Data.answerSheetData.answerDict[i] ? "O" : "X";
                }
                break;
            default:
                break;
        }
        isInit = true;
    }
}

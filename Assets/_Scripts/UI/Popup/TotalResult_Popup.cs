using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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
    enum Images
    {
        BeforeContactingThePatient,
        BeforeCleanlinessAndAsepticTreatment,
        AfterTheRiskOfExposureToBodyFluids,
        AfterContactingThePatient,
        AfterContactingThePatientsSurroundings
    }

    int correctCnt = 0;
    int totalCnt = 0;
    int[] handCleanSituationTypeArray = new int[] { 0, 0, 0, 0, 0 };

    public override void Init()
    {
        base.Init();
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));

        for (Define.SituationCode i = Define.SituationCode.S1_1; i <= Define.SituationCode.S4_14; i++)
        {
            if (Managers.Data.answerSheetData.answerDict[i].answer)
                totalCnt++;
            else continue;

            if (Managers.Data.recordData.handCleanRecords[i] == Managers.Data.answerSheetData.answerDict[i].answer)
            {
                correctCnt++;
                if (Managers.Data.answerSheetData.answerDict[i].typeList.Count == 0) continue;

                foreach (var item in Managers.Data.answerSheetData.answerDict[i].typeList)
                {
                    handCleanSituationTypeArray[(int)item]++;
                }
            }
        }

        int min = handCleanSituationTypeArray.Min();
        for (Define.HandCleanSituationType type = Define.HandCleanSituationType.BeforeContactingThePatient; type < Define.HandCleanSituationType.None; type++)
        {
            Debug.Log($"Min : {min}, type : {type}");
            if(handCleanSituationTypeArray[(int)type] == min)
                GetImage((int)type).color = Color.red;
        }


        Get<Text>((int)Texts.TotalRate_Text).text = string.Format("종합 손위생\n 이행률\n : ({0:0.#})%", ((float)correctCnt / totalCnt) * 100);
        Get<Button>((int)Buttons.Ok_Button).onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            Managers.Scene.LoadScene(Define.Scene.StartScene);
        }));
    }
}

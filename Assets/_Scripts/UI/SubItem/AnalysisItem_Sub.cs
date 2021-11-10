using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AnalysisItem_Sub : UIBase
{
    enum Texts
    {
        Result_Text,
        Check,
    }
    enum Buttons
    {
        Button_Item
    }

    Define.SituationCode itemRecordType;

    public override void Init()
    {
        
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        
        Get<Button>((int)Buttons.Button_Item).onClick.AddListener(new UnityAction(() =>
        {
            OnClickAnalysisButton();
        }));
    }


    public void SetInfo(Define.SituationCode code)
    {
        itemRecordType = code;
        Text checkText = Get<Text>((int)Texts.Check);
        checkText.text = Managers.Data.recordData.handCleanRecords[code] == Managers.Data.answerSheetData.answerDict[code] ? "O" : "X";
        checkText.color = Managers.Data.recordData.handCleanRecords[code] == Managers.Data.answerSheetData.answerDict[code] ? Color.blue : Color.red;
        Get<Text>((int)Texts.Result_Text).text = Managers.Data.recordData.handCleanRecords[code] ? Managers.Data.situationAnalysisData.SituationDict[code].right : Managers.Data.situationAnalysisData.SituationDict[code].wrong;
    }
    public void OnClickAnalysisButton()
    {
        StartCoroutine(ShowAnalysis());
    }
    IEnumerator ShowAnalysis()
    {
        Analysis_Popup popup = Managers.UI.ShowPopupUI<Analysis_Popup>();
        yield return new WaitUntil(() => popup);
        popup.SetText(new TextAsset(Managers.Data.situationAnalysisData.SituationDict[itemRecordType].analysis));
    }
}

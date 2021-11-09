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

    Define.HandCleanRecord itemRecordType;

    public override void Init()
    {
        
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        
        Get<Button>((int)Buttons.Button_Item).onClick.AddListener(new UnityAction(() =>
        {
            OnClickAnalysisButton();
        }));
    }


    public void SetInfo(Define.HandCleanRecord recordType)
    {
        itemRecordType = recordType;
        Get<Text>((int)Texts.Check).text = Managers.Data.recordData.handCleanRecords[recordType] ? "O" : "X";
        Get<Text>((int)Texts.Result_Text).text = Managers.Data.recordData.handCleanRecords[recordType] ? "O" : "X";
    }
    public void OnClickAnalysisButton()
    {

    }
}

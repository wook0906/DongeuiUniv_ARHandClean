using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScene_UI : UIScene
{
    string sex = "00000000";
    enum InputFields
    {
        StudentNumberField,
    }
    enum Buttons
    {
        Start_Button,
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<InputField>(typeof(InputFields));

        Get<InputField>((int)InputFields.StudentNumberField).onEndEdit.AddListener(delegate { SaveStudNum(Get<InputField>((int)InputFields.StudentNumberField).text); });
        Get<Button>((int)Buttons.Start_Button).onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            Managers.Data.recordData.sex = sex;
            Debug.Log($"Save Sex : {sex}");
            Managers.Scene.LoadScene(Define.Scene.Scene_1);
        }));
    }
    void SaveStudNum(string sex)
    {
        if(sex != string.Empty)
            this.sex = sex;
    }
}

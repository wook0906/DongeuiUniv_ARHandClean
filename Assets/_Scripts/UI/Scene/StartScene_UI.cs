using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScene_UI : UIScene
{
    string sex = "Did not choose";
    int age = 0;
    int grade = 0;
    string email = "None";
    string phoneNumber = "None";

    Toggle femaleToggle;
    Toggle maleToggle;

    enum Toggles
    {
        MaleToggle,
        FemaleToggle
    }
    enum InputFields
    {
        AgeField,
        GradeField,
        EmailField,
        PhoneNumberField,
    }
    enum Buttons
    {
        Start_Button,
    }
    public override void Init()
    {
        base.Init();
        Bind<Toggle>(typeof(Toggles));
        Bind<Button>(typeof(Buttons));
        Bind<InputField>(typeof(InputFields));

        femaleToggle = Get<Toggle>((int)Toggles.FemaleToggle);
        femaleToggle.onValueChanged.AddListener(delegate { SaveSex(femaleToggle); });
        maleToggle = Get<Toggle>((int)Toggles.MaleToggle);
        maleToggle.onValueChanged.AddListener(delegate { SaveSex(maleToggle); });

        Get<InputField>((int)InputFields.AgeField).onEndEdit.AddListener(delegate { SaveAge(Get<InputField>((int)InputFields.AgeField).text); });
        Get<InputField>((int)InputFields.GradeField).onEndEdit.AddListener(delegate { SaveGrade(Get<InputField>((int)InputFields.GradeField).text); });
        Get<InputField>((int)InputFields.EmailField).onEndEdit.AddListener(delegate { SaveEmail(Get<InputField>((int)InputFields.EmailField).text); });
        Get<InputField>((int)InputFields.PhoneNumberField).onEndEdit.AddListener(delegate { SavePhoneNumber(Get<InputField>((int)InputFields.PhoneNumberField).text); });

        Get<Button>((int)Buttons.Start_Button).onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            if (maleToggle.isOn)
                sex = "Male";
            else if (femaleToggle.isOn)
                sex = "Female";
            Managers.Data.recordData.sex = sex;
            Debug.Log($"Save Sex : {sex}");
            Managers.Data.recordData.age = age;
            Debug.Log($"Save age : {age}");

            Managers.Data.recordData.grade = grade;
            Debug.Log($"Save grade : {grade}");
            Managers.Data.recordData.email = email;
            Debug.Log($"Save email : {email}");
            Managers.Data.recordData.phoneNumber = phoneNumber;
            Debug.Log($"Save phoneNumber : {phoneNumber}");
            Managers.Scene.LoadScene(Define.Scene.Scene_1);
        }));
    }
    void SaveSex(Toggle toggle)
    {
        if (toggle == maleToggle)
        {
            if (toggle.isOn)
                femaleToggle.isOn = false;
            else
                femaleToggle.isOn = true;
        }
        else
        {
            if (toggle.isOn)
                maleToggle.isOn = false;
            else
                maleToggle.isOn = true;
        }
    }
    void SaveAge(string age)
    {
        if(age != string.Empty)
            this.age = int.Parse(age);
    }
    void SaveGrade(string grade)
    {
        if (grade != string.Empty)
            this.grade = int.Parse(grade);
    }
    void SaveEmail(string email)
    {
        if (email != string.Empty)
            this.email = email;
    }
    void SavePhoneNumber(string phoneNumber)
    {
        if (phoneNumber != string.Empty)
            this.phoneNumber = phoneNumber;
    }

}

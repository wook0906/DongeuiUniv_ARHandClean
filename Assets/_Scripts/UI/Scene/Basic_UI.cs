using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basic_UI : UIScene
{
    public List<Camera> camList = new List<Camera>();
    
    public Camera currentCam;
    Define.Views prevView;
    [SerializeField]
    Define.Views currentView = Define.Views.Both;
    public Define.Views CurrentView
    {
        get { return currentView; }
        set
        {
            currentView = value;
            if (value < Define.Views.Left_Patient_Stand)
            {
                currentView = Define.Views.Left_Patient_Stand;
            }
            else if (value > Define.Views.Right_BedLever)
            {
                currentView = Define.Views.Right_BedLever;
            }
                

            Debug.Log($"current : {currentView}");
            currentCam.gameObject.SetActive(false);
            currentCam = camList[(int)currentView-1];
            currentCam.gameObject.SetActive(true);

            GameObject.Find("@Scene").GetComponent<BaseScene>().RenewFocusPositions();
        }
    }
    public bool isGloveOn = false;
    bool isTimerOn = false;


    enum Images
    {
        Timer_Image,
        Hand_Image,
    }

    enum Buttons
    {
        LeftView_Button,
        RightView_Button,
        HandClean_Button,
        Glove_Button,
    }
    enum Texts
    {
        Count_Text
    }
    public override void Init()
    {
        base.Init();
        foreach (Transform item in GameObject.Find("Cams").transform)
        {
            camList.Add(item.GetComponent<Camera>());
        }
        currentCam = camList[(int)CurrentView];

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        Get<Text>((int)Texts.Count_Text).enabled = false;
        Get<Image>((int)Images.Hand_Image).enabled = false;
        Get<Image>((int)Images.Timer_Image).enabled = false;

        Get<Button>((int)Buttons.LeftView_Button).onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            CurrentView--;        
        }));
        Get<Button>((int)Buttons.RightView_Button).onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            CurrentView++;
        }));
        Get<Button>((int)Buttons.HandClean_Button).onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            if (isTimerOn)
                return;
            else
                isTimerOn = true;
            StartCoroutine(DoHandClean());
        }));
        Get<Button>((int)Buttons.Glove_Button).onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            OnClickGloveButton();
        }));
    }
    public void MoveView(Define.Views requestView)
    {
        CurrentView = requestView;
    }

    public void SetGloveButton(bool on)
    {
        Get<Button>((int)Buttons.Glove_Button).gameObject.SetActive(on);
    }
    public void OnClickGloveButton()
    {
        if (isGloveOn)
            isGloveOn = false;
        else
            isGloveOn = true;
    }

    IEnumerator DoHandClean()
    {
        prevView = CurrentView;
        CurrentView = Define.Views.Both;
        Get<Text>((int)Texts.Count_Text).enabled = true;
        Get<Image>((int)Images.Hand_Image).enabled = true;
        Get<Image>((int)Images.Timer_Image).enabled = true;

        float count = 20;
        Get<Text>((int)Texts.Count_Text).text = count.ToString();
        while (count > 0)
        {
            if (CurrentView != Define.Views.Both)
            {
                CurrentView = prevView;
                isTimerOn = false;
                Get<Image>((int)Images.Timer_Image).fillAmount = 1;
                Get<Text>((int)Texts.Count_Text).enabled = false;
                Get<Image>((int)Images.Hand_Image).enabled = false;
                Get<Image>((int)Images.Timer_Image).enabled = false;
                yield break;
            }
            count -= Time.deltaTime;
            Get<Text>((int)Texts.Count_Text).text = count.ToString();
            Get<Image>((int)Images.Timer_Image).fillAmount -= Time.deltaTime / 20;
            yield return null;
        }
        isTimerOn = false;
        CurrentView = prevView;
        GameObject.Find("@Scene").GetComponent<BaseScene>().isNeedHandClean = false;
        Get<Image>((int)Images.Timer_Image).fillAmount = 1;
        Get<Text>((int)Texts.Count_Text).enabled = false;
        Get<Image>((int)Images.Hand_Image).enabled = false;
        Get<Image>((int)Images.Timer_Image).enabled = false;

    }
}

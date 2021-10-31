using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basic_UI : UIScene
{
    public Dictionary<Define.Views, Transform> viewTransformDict = new Dictionary<Define.Views, Transform>();


    public Camera ARCam;
    public Camera UICam;
    Define.Views prevView;
    [SerializeField]
    Define.Views currentView = Define.Views.Both;
    public Define.Views CurrentView
    {
        get { return currentView; }
        set
        {
            
            if (value < Define.Views.Left_Patient_Stand)
            {
                currentView = Define.Views.Left_Patient_Stand;
            }
            else if (value > Define.Views.Right_BedLever)
            {
                currentView = Define.Views.Right_BedLever;
            }
            else
            {
                currentView = value;
            }

            ARCam.gameObject.SetActive(false);
            ARCam.transform.position = viewTransformDict[currentView].position;
            ARCam.transform.rotation = viewTransformDict[currentView].rotation;
            ARCam.gameObject.SetActive(true);

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
        Retry_Button,
        Next_Button,
    }
    enum Texts
    {
        Count_Text
    }
    public override void Init()
    {
        base.Init();
        ARCam = GameObject.Find("ARCamera").GetComponent<Camera>();
        UICam = GameObject.Find("UICamera").GetComponent<Camera>();
        

        for (Define.Views i = Define.Views.Left_Patient_Stand; i <= Define.Views.Right_BedLever; i++)
        {
            viewTransformDict.Add(i, GameObject.Find(i.ToString()).transform);
        }
        

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
        Get<Button>((int)Buttons.Retry_Button).onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            Managers.Scene.LoadScene(Managers.Scene.currentScene.SceneType);
        }));
        Get<Button>((int)Buttons.Next_Button).onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            Managers.Scene.LoadScene(Managers.Scene.currentScene.SceneType + 1);
        }));
        MoveSceneButtonOff();
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
    public void LateUpdate()
    {
        if(ARCam && UICam)
        UICam.fieldOfView = ARCam.fieldOfView;
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
        BaseScene curScene = GameObject.Find("@Scene").GetComponent<BaseScene>();
        curScene.infectionState = Define.Infection.None;
        Get<Image>((int)Images.Timer_Image).fillAmount = 1;
        Get<Text>((int)Texts.Count_Text).enabled = false;
        Get<Image>((int)Images.Hand_Image).enabled = false;
        Get<Image>((int)Images.Timer_Image).enabled = false;

    }

    public void MoveSceneButtonOn()
    {
        if (Managers.Scene.currentScene.SceneType + 1 != Define.Scene.Max)
            Get<Button>((int)Buttons.Next_Button).gameObject.SetActive(true);
  
        Get<Button>((int)Buttons.Retry_Button).gameObject.SetActive(true);
    }
    public void MoveSceneButtonOff()
    {
        Get<Button>((int)Buttons.Next_Button).gameObject.SetActive(false);
        Get<Button>((int)Buttons.Retry_Button).gameObject.SetActive(false);
    }

    public void SetInteractable(bool on)
    {
        if (!on)
        {
            Get<Button>((int)Buttons.LeftView_Button).gameObject.SetActive(false);
            Get<Button>((int)Buttons.RightView_Button).gameObject.SetActive(false);
            Get<Button>((int)Buttons.HandClean_Button).gameObject.SetActive(false);
            Get<Button>((int)Buttons.Glove_Button).gameObject.SetActive(false);
            Get<Button>((int)Buttons.Retry_Button).gameObject.SetActive(false);
            Get<Button>((int)Buttons.Next_Button).gameObject.SetActive(false);
        }
        else
        {
            Get<Button>((int)Buttons.LeftView_Button).gameObject.SetActive(true);
            Get<Button>((int)Buttons.RightView_Button).gameObject.SetActive(true);
            Get<Button>((int)Buttons.HandClean_Button).gameObject.SetActive(true);
            Get<Button>((int)Buttons.Glove_Button).gameObject.SetActive(true);
            Get<Button>((int)Buttons.Retry_Button).gameObject.SetActive(true);
            Get<Button>((int)Buttons.Next_Button).gameObject.SetActive(true);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Managers.Data.Save();
        }
    }
}

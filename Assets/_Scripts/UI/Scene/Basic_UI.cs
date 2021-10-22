using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basic_UI : UIScene
{
    public List<Camera> camList = new List<Camera>();
    public Camera currentCam;
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

    enum Buttons
    {
        LeftView_Button,
        RightView_Button,
        HandClean_Button,
        Glove_Button,
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
            //TODO HandClean
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
}

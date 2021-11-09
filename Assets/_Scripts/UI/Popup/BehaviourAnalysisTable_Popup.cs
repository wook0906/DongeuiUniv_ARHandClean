using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BehaviourAnalysisTable_Popup : UIPopup
{
    bool isInit = false;


    enum Buttons
    {
        Next_Button,
    }

    enum GameObjects
    {
        Content
    }
    // Start is called before the first frame update

    Transform scrollViewItemRoot;
    Vector2 sizeDelta;
    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        Get<Button>((int)Buttons.Next_Button).onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            ClosePopupUI();
        }));

        scrollViewItemRoot = Get<GameObject>((int)GameObjects.Content).transform;
        sizeDelta = scrollViewItemRoot.GetComponent<RectTransform>().sizeDelta;
        StartCoroutine(CorInit());
       
    }
    IEnumerator CorInit()
    {
        switch (Managers.Scene.currentScene.SceneType)
        {
            case Define.Scene.Scene_1:
                for (Define.HandCleanRecord i = Define.HandCleanRecord.S1_1; i <= Define.HandCleanRecord.S1_10; i++)
                {
                    AnalysisItem_Sub item = Managers.UI.MakeSubItem<AnalysisItem_Sub>(scrollViewItemRoot);
                    item.transform.localScale = Vector3.one;
                    yield return new WaitUntil(() => item);
                    item.SetInfo(i);
                    sizeDelta.y += 110;
                    scrollViewItemRoot.GetComponent<RectTransform>().sizeDelta = sizeDelta;
                }
                break;
            case Define.Scene.Scene_2:
                for (Define.HandCleanRecord i = Define.HandCleanRecord.S2_1; i <= Define.HandCleanRecord.S2_12; i++)
                {
                    AnalysisItem_Sub item = Managers.UI.MakeSubItem<AnalysisItem_Sub>(scrollViewItemRoot);
                    item.transform.localScale = Vector3.one;
                    yield return new WaitUntil(() => item);
                    item.SetInfo(i);
                    sizeDelta.y += 110;
                    scrollViewItemRoot.GetComponent<RectTransform>().sizeDelta = sizeDelta;
                }
                break;
            case Define.Scene.Scene_3:
                for (Define.HandCleanRecord i = Define.HandCleanRecord.S3_1; i <= Define.HandCleanRecord.S3_11; i++)
                {
                    AnalysisItem_Sub item = Managers.UI.MakeSubItem<AnalysisItem_Sub>(scrollViewItemRoot);
                    item.transform.localScale = Vector3.one;
                    yield return new WaitUntil(() => item);
                    item.SetInfo(i);
                    sizeDelta.y += 110;
                    scrollViewItemRoot.GetComponent<RectTransform>().sizeDelta = sizeDelta;
                }
                break;
            case Define.Scene.Scene_4:
                for (Define.HandCleanRecord i = Define.HandCleanRecord.S4_1; i <= Define.HandCleanRecord.S4_14; i++)
                {
                    AnalysisItem_Sub item = Managers.UI.MakeSubItem<AnalysisItem_Sub>(scrollViewItemRoot);
                    item.transform.localScale = Vector3.one;
                    yield return new WaitUntil(() => item);
                    item.SetInfo(i);
                    sizeDelta.y += 110;
                    scrollViewItemRoot.GetComponent<RectTransform>().sizeDelta = sizeDelta;
                }
                break;
            default:
                break;
        }
        isInit = true;
    }
}

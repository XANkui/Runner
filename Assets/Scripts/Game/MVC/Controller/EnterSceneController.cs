using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterSceneController : Controller
{
    public override void Excute(object data)
    {
        Debug.Log(" RegisterView E_UpdateDisance");
        ScenesArgs e = data as ScenesArgs;

        switch (e.sceneIndex)
        {
            case 1:
                break;

            case 2:
                break;

            case 3:
                break;

            case 4:
                Debug.Log(" RegisterView E_UpdateDisance");
                RegisterView(GameObject.FindWithTag(Tag.Player).GetComponent<PlayerMove>());
                RegisterView(GameObject.FindWithTag(Tag.Player).GetComponent<PlayerAnim>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UIBoard").GetComponent<UIBoard>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UIPause").GetComponent<UIPause>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UIResume").GetComponent<UIResume>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UIDead").GetComponent<UIDead>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UIFinalScore").GetComponent<UIFinalScore>());
                
                break;

            default:
                break;
        }
    }
}

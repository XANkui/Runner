using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResume : View
{

    public Image imageCount;
    public Sprite[] spriteCount;

    public override string Name => Consts.V_Resume;

    public override void HandleEvent(string name, object data)
    {
        
    }

    public void StartCount() {
        Show();
        StartCoroutine(StartCountCor());
    }

    IEnumerator StartCountCor() {
        int i = 3;
        while (i>0)
        {
            imageCount.sprite = spriteCount[i-1];
            i--;
            yield return new WaitForSeconds(1);
            if (i<0)
            {
                break;
            }
        }

        Hide();

        // TODO
        //GameModel gameModel = GetModel<GameModel>();
        //gameModel.IsPause = false;
        //gameModel.IsPlay = true;
        SendEvent(Consts.E_ContinueGame);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}

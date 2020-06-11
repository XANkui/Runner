using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFinalScore : View
{
    public Text TextDistance;
    public Text TextCoin;
    public Text TextScore;
    public Text TextGoal;

    public Slider sliderExp;
    public Text TextExp;
    public Text TextGrade;

    public override string Name => Consts.V_FinalScore;

    public override void HandleEvent(string name, object data)
    {
        throw new System.NotImplementedException();
    }
    public void Hide()
    {

        gameObject.SetActive(false);

    }


    public void Show()
    {
        gameObject.SetActive(true);
    }


    public void UpdateUI(int distance, int coin, int goal,int exp,int grade) {
        TextDistance.text = distance.ToString();
        TextCoin.text = coin.ToString();
        TextGoal.text = goal.ToString();
        TextScore.text = ((distance * (goal + 1)) + coin).ToString();

        TextExp.text = exp + "/" + (500 + grade * 100);

        // 转为浮点数才显示
        sliderExp.value = (float)exp / (500 + grade * 100);

        TextGrade.text = grade + "级";
    }


}

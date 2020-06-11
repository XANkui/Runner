using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalShowUIController : Controller
{
    public override void Excute(object data)
    {
        GameModel gameModel = GetModel<GameModel>();
        UIBoard board = GetView<UIBoard>();
        UIDead dead = GetView<UIDead>();
        UIFinalScore finalScore = GetView<UIFinalScore>();
        board.Hide();
        dead.Hide();
        finalScore.Show();
        gameModel.Exp += board.Coin + (board.Distance * (board.GoalCount + 1));
        finalScore.UpdateUI(board.Distance,board.Coin,board.GoalCount,gameModel.Exp,gameModel.Grade);

        
    }
}

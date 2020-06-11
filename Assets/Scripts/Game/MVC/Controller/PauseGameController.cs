using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameController : Controller
{
    public override void Excute(object data)
    {
        GameModel gameModel = GetModel<GameModel>();

        gameModel.IsPause = true;

        PauseArgs args = (PauseArgs)data;

        UIPause pause = GetView<UIPause>();
        pause.Coin = args.coin;
        pause.Distance = args.distance;
        pause.Score = args.score;
        pause.Show();
    }
}

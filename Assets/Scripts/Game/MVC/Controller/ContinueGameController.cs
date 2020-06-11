using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueGameController : Controller
{
    public override void Excute(object data)
    {
        GameModel gameModel = GetModel<GameModel>();

        UIBoard board = GetView<UIBoard>();

        // 判断是时间到了的贿赂事件（加时 20秒）
        if (board.Times < 0.1f)
        {
            board.Times += 20;
        }

        gameModel.IsPause = false;
        gameModel.IsPlay = true;
    }
}

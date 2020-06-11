using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriberyClickController : Controller
{
    public override void Excute(object data)
    {
        CoinArgs coin = data as CoinArgs;
        UIDead dead = GetView<UIDead>();
        GameModel gameModel = GetModel<GameModel>();


        // 如果花钱成功

        if (gameModel.GetMoney(coin.coin))
        {
            dead.BriberyTime++;
            dead.Hide();            
            UIResume resume = GetView<UIResume>();
            resume.StartCount();
        }

        
    }
}

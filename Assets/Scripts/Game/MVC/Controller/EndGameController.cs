using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameController : Controller
{
    public override void Excute(object data)
    {
        GameModel gm = GetModel<GameModel>();
        gm.IsPlay = false;

        // TODO 弹出面板
        UIDead uIDead = GetView<UIDead>();

        uIDead.Show();

    }
}

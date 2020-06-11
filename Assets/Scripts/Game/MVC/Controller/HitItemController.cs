using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitItemController : Controller
{
    public override void Excute(object data)
    {
        ItemArgs args = data as ItemArgs;

        PlayerMove playerMove = GetView<PlayerMove>();
        GameModel gameModel = GetModel<GameModel>();
        UIBoard ui = GetView<UIBoard>();
        switch (args.itemKind)
        {
            case ItemKind.ItemMagnet:
                //道具使用
                playerMove.HitMagnet();
                gameModel.Magnet -= args.spendCount;

                // 道具技能时间的显示
                ui.HitMagnet();
                break;
            case ItemKind.ItemMultiply:
                playerMove.HitMultiply();
                gameModel.Multiply -= args.spendCount;
                ui.HitMultiply();
                break;
            case ItemKind.ItemInvincible:
                playerMove.HitInvinvible();
                gameModel.Invincible -= args.spendCount;
                ui.HitInvinvible();
                break;
            default:
                break;
        }

        ui.UpdateUI();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteupController : Controller
{
    public override void Excute(object data)
    {
        // 注册所有 Controller
        RegisterControoler(Consts.E_EnterSceneController,typeof(EnterSceneController));
        RegisterControoler(Consts.E_EndGameController,typeof(EndGameController));
        RegisterControoler(Consts.E_PauseGame,typeof(PauseGameController));
        RegisterControoler(Consts.E_ResumeGame,typeof(ResumeGameController));
        RegisterControoler(Consts.E_HitItem,typeof(HitItemController));
        RegisterControoler(Consts.E_FinalShowUI,typeof(FinalShowUIController));
        RegisterControoler(Consts.E_BriberyClick,typeof(BriberyClickController));
        RegisterControoler(Consts.E_ContinueGame,typeof(ContinueGameController));
        

        // 注册所有 Model
        RegisterModel(new GameModel());


        // 初始化
        GameModel gameModel = GetModel<GameModel>();
        gameModel.Init();
    }
}

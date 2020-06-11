using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Consts 
{
    // 事件名称
    public const string E_ExitEvent = "E_ExitEvent";
    // public const string E_EnterEvent = "E_EnterEvent";
    public const string E_EnterSceneController = "E_EnterSceneController";
    public const string E_SteupController = "E_SteupController";    
    public const string E_EndGameController = "E_EndGameController";
    public const string E_PauseGame = "E_PauseGame";
    public const string E_ResumeGame = "E_ResumeGame";

    /// <summary>
    /// *******UI相关
    /// </summary>
    public const string E_UpdateDisance = "E_UpdateDisance"; //参数 DistanceArgs
    public const string E_UpdateCoin = "E_UpdateCoin"; //参数 CoinArgs
    public const string E_HitAddTime = "E_HitAddTime";

    public const string E_HitItem = "E_HitItem";//ItemArgs
    public const string E_HitGoalTrigger = "E_HitGoalTrigger";//
    public const string E_ClickGoalButton = "E_ClickGoalButton";//
    public const string E_ShootGoal = "E_ShootGoal";//进球事件
    public const string E_FinalShowUI = "E_FinalShowUI";//结算UI
    public const string E_BriberyClick = "E_BriberyClick";//贿赂事件 参数 汇率金额
    public const string E_ContinueGame = "E_ContinueGame";//继续游戏

    // Model 名称
    public const string M_GameModel = "M_GameModel";

    // View名称
    public const string V_PlayerMove = "V_PlayerMove";
    public const string V_PlayerAnim = "V_PlayerAnim";
    public const string V_Board = "V_Board";
    public const string V_Pause = "V_Pause";
    public const string V_Resume = "V_Resume";
    public const string V_Dead = "V_Dead";
    public const string V_FinalScore = "V_FinalScore";
}


public enum InputDirection {

    NULL,
    Right,
    Left,
    Down,
    Up

}

public enum ItemKind {
    ItemMagnet,
    ItemMultiply,
    ItemInvincible,
}
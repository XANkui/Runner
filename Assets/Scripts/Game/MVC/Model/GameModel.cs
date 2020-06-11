using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : Model
{


    #region 常量
    private const int INIT_COIN = 1000;
    #endregion

    #region 事件
    #endregion

    #region 字段

    private bool m_isPlay = true;
    private bool m_isPause = false;
    private int m_skillTime = 5;

    // 各个技能道具的个数
    private int m_Magnet;
    private int m_Multiply;
    private int m_Invincible;

    private int m_Grade;
    private int m_Exp;

    private int m_Coin;

    #endregion

    #region 属性

    public override string Name => Consts.M_GameModel;

    public bool IsPlay { get => m_isPlay; set => m_isPlay = value; }
    public bool IsPause { get => m_isPause; set => m_isPause = value; }
    public int SkillTime { get => m_skillTime; set => m_skillTime = value; }
    public int Magnet { get => m_Magnet; set => m_Magnet = value; }
    public int Multiply { get => m_Multiply; set => m_Multiply = value; }
    public int Invincible { get => m_Invincible; set => m_Invincible = value; }
    public int Grade { get => m_Grade; set => m_Grade = value; }
    public int Exp { get => m_Exp;
        set
        {
            // 无限判断是否符合升级要求（if 只能判断一次）
            while (value > 500 +Grade*100)
            {
                value -= (500 + Grade * 100);
                Grade++;
            }

            m_Exp = value;
        }
    }

    public int Coin { get => m_Coin; set { m_Coin = value;
            Debug.Log("现在余额："+value);
        } }

    #endregion

    #region 方法

    public void Init() {

        m_Magnet = 1;
        m_Multiply = 2;
        m_Invincible = 1;
        m_skillTime = 5;

        m_Grade = 1;
        m_Exp = 0;

        Coin = INIT_COIN;
    }

    /// <summary>
    /// 购物，并且判断钱够不够
    /// </summary>
    /// <param name="coin"></param>
    /// <returns></returns>
    public bool GetMoney(int coin) {
        if (coin <= Coin)
        {
            Coin -= coin;

            return true;
        }

        return false;
    }

    #endregion

    #region Unity回调
    #endregion

    #region 事件回调
    #endregion

    #region 帮助方法
    #endregion
}

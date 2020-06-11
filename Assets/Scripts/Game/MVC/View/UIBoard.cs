using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBoard : View
{


    #region 常量

    public const int startTimer = 50;
    #endregion

    #region 事件
    #endregion

    #region 字段

    int m_coin = 0;
    int m_distance =0;
    float m_time=0;
    int goalCount = 0;
    float m_skillTime;

    public Text textCoin;
    public Text textDistance;

    public Text textTimer;
    public Slider sliderTimer;
    public Button buttonPause;


    GameModel gameModel;

    public Button btnMagnet;
    public Button btnMultiply;
    public Button btnInvincible;

    public Text textGizmoMagnet;
    public Text textGizmoMultiply;
    public Text textGizmoInvincible;

    public Slider sliderGoal;
    public Button btnGoal;

    // 多倍积分协程
    IEnumerator MultiplyCor;

    // 吸铁石协程
    IEnumerator MagnetCor;

    // 无敌状态协程
    IEnumerator InvinvibleCor;

    #endregion

    #region 属性
    public override string Name => Consts.V_Board;

    public int Coin { get => m_coin;
        set {
            m_coin = value;
            textCoin.text = value.ToString();
        } }
    public int Distance { get => m_distance;
        set
        {
            m_distance = value;
            textDistance.text = value.ToString()+"米";
        }
    }

    public float Times { get => m_time;
        set {
            if (value < 0) {
                value = 0;
                SendEvent(Consts.E_EndGameController);
            }
            if (value > startTimer)
            {
                value = startTimer;
            }

            m_time = value;
            textTimer.text = value.ToString("f2");
            sliderTimer.value = value / startTimer;
        }

    }

    public int GoalCount { get => goalCount; set => goalCount = value; }
    #endregion

    #region 方法

    /// <summary>
    /// 更新道具数量的UI
    /// </summary>
    public void UpdateUI() {
        ShowOrHide(gameModel.Magnet,btnMagnet);
        ShowOrHide(gameModel.Multiply,btnMultiply);
        ShowOrHide(gameModel.Invincible,btnInvincible);
    }

    void ShowOrHide(int i, Button btn) {
        GameObject mask = btn.transform.Find("Mask").gameObject;

        if (i > 0)
        {
            btn.interactable = true;
            mask.SetActive(false);
        }
        else {
            btn.interactable = false;
            mask.SetActive(true);
        }
    }


    /// <summary>
    /// 暂停按钮事件
    /// </summary>
    public void OnPauseClick() {

        PauseArgs args = new PauseArgs()
        {
            coin = Coin,
            distance = Distance,
            score = Coin * 2 + Distance + GoalCount * 20
        };
        SendEvent(Consts.E_PauseGame,args);
    }

    // 更新道具技能使用时间
    /// <summary>
    /// 双倍金币技能（有技能时间）
    /// </summary>
    public void HitMultiply()
    {

        if (MultiplyCor != null)
        {
            StopCoroutine(MultiplyCor);
        }

        MultiplyCor = MultiplyCoroutine();
        StartCoroutine(MultiplyCor);
    }

    IEnumerator MultiplyCoroutine()
    {
        textGizmoMultiply.transform.parent.gameObject.SetActive(true);
        float timer = m_skillTime;
        while (timer > 0)
        {
            if (gameModel.IsPause == false && gameModel.IsPlay == true)
            {
                timer -= Time.deltaTime;
                textGizmoMultiply.text = GetTimeIntString(timer);
            }

            yield return 0;
        }
        textGizmoMultiply.transform.parent.gameObject.SetActive(false);
    }



    /// <summary>
    /// 吸铁石道具
    /// </summary>
    public void OnMagnetClick()
    {
        ItemArgs args = new ItemArgs
        {
            spendCount = 1,
            itemKind = ItemKind.ItemMagnet
        };

        SendEvent(Consts.E_HitItem, args);
    }

    /// <summary>
    /// 吸铁石道具
    /// </summary>
    public void OnMultiplyClick()
    {
        ItemArgs args = new ItemArgs
        {
            spendCount = 1,
            itemKind = ItemKind.ItemMultiply
        };

        SendEvent(Consts.E_HitItem, args);
    }

    /// <summary>
    /// 吸铁石道具
    /// </summary>
    public void OnInvincibleClick()
    {
        ItemArgs args = new ItemArgs
        {
            spendCount = 1,
            itemKind = ItemKind.ItemInvincible
        };

        SendEvent(Consts.E_HitItem, args);
    }

    /// <summary>
    /// 吸铁石功能
    /// </summary>
    public void HitMagnet()
    {
        if (MagnetCor != null)
        {
            StopCoroutine(MagnetCor);
        }
        MagnetCor = MagnetCoroutine();
        StartCoroutine(MagnetCor);
    }

    IEnumerator MagnetCoroutine()
    {
        
        textGizmoMagnet.transform.parent.gameObject.SetActive(true);
        float timer = m_skillTime;
        while (timer > 0)
        {
            if (gameModel.IsPause == false && gameModel.IsPlay == true)
            {
                timer -= Time.deltaTime;
                textGizmoMagnet.text = GetTimeIntString(timer);
            }

            yield return 0;
        }
        textGizmoMagnet.transform.parent.gameObject.SetActive(false);

    }


    /// <summary>
    /// 无敌状态
    /// </summary>
    public void HitInvinvible()
    {

        Debug.Log("Hit Invinvible");

        if (InvinvibleCor != null)
        {
            StopCoroutine(InvinvibleCor);
        }

        InvinvibleCor = InvinvibleCoroutine();
        StartCoroutine(InvinvibleCor);
    }

    IEnumerator InvinvibleCoroutine()
    {
        textGizmoInvincible.transform.parent.gameObject.SetActive(true);
        float timer = m_skillTime;
        while (timer > 0)
        {
            if (gameModel.IsPause == false && gameModel.IsPlay == true)
            {
                timer -= Time.deltaTime;
                textGizmoInvincible.text = GetTimeIntString(timer);
            }

            yield return 0;
        }
        textGizmoInvincible.transform.parent.gameObject.SetActive(false);
    }

    /// <summary>
    /// 时间转为整数字符串
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    string GetTimeIntString(float time) {
        return ((int)time + 1).ToString();
    }


    /// <summary>
    /// 激活射球功能可用的状态
    /// </summary>
    void ShowGoalClick() {
        // 显示Slider 和 按钮交互
        StartCoroutine(StartCountDown());
    }

    IEnumerator StartCountDown() {
        btnGoal.interactable = true;
        sliderGoal.value = 1;

        while (sliderGoal.value > 0) {
            if (gameModel.IsPause ==false && gameModel.IsPlay ==true)
            {
                sliderGoal.value -= 1.25f * Time.deltaTime;

            }
            yield return 0;
        }
        btnGoal.interactable = false;
        sliderGoal.value = 0;


    }

    /// <summary>
    /// 点击射门
    /// </summary>
    public void OnGoalBtnClick()
    {
        SendEvent(Consts.E_ClickGoalButton);
        sliderGoal.value = 0;
    }


    public void Hide()
    {

        gameObject.SetActive(false);

    }


    public void Show()
    {
        gameObject.SetActive(true);
    }

    #endregion

    #region Unity回调
    private void Awake()
    {
        m_time = startTimer;
        gameModel = GetModel<GameModel>();
        buttonPause.onClick.AddListener(OnPauseClick);
        UpdateUI();
        m_skillTime = gameModel.SkillTime;

        btnMagnet.onClick.AddListener(OnMagnetClick);
        btnMultiply.onClick.AddListener(OnMultiplyClick);
        btnInvincible.onClick.AddListener(OnInvincibleClick);

        btnGoal.onClick.AddListener(OnGoalBtnClick);

    }

    private void Update()
    {
        if (gameModel.IsPlay && gameModel.IsPause ==false)
        {
            Times -= Time.deltaTime;

        }
    }

   

   

    #endregion

    #region 事件回调
    public override void RegisterAttentionEvent()
    {
        //Debug.Log(" RegisterAttentionEvent E_UpdateDisance");
        AttentionList.Add(Consts.E_UpdateDisance);
        AttentionList.Add(Consts.E_UpdateCoin);
        AttentionList.Add(Consts.E_HitAddTime);
        AttentionList.Add(Consts.E_HitGoalTrigger);
        AttentionList.Add(Consts.E_ShootGoal);
    }

    public override void HandleEvent(string name, object data)
    {
        //Debug.Log(" HandleEvent E_UpdateDisance");
        switch (name)
        {
            case Consts.E_UpdateDisance:
                DistanceArgs e = (DistanceArgs)data;
                Distance = e.distance;
                //Debug.Log(" HandleEvent E_UpdateDisance");
                break;

            case Consts.E_UpdateCoin:
                CoinArgs e1 = (CoinArgs)data;
                Coin += e1.coin;
                Debug.Log(" HandleEvent E_UpdateDisance");
                break;

            case Consts.E_HitAddTime:
                
                Times += 20;
                Debug.Log(" HandleEvent E_UpdateDisance");
                break;

            case Consts.E_HitGoalTrigger:

                ShowGoalClick();
                Debug.Log(" HandleEvent E_HitGoalTrigger");
                break;
            case Consts.E_ShootGoal:

                GoalCount +=1;
                Debug.Log(" HandleEvent E_ShootGoal GoalCount " + GoalCount);
                break;

            default:
                break;
        }
    }
    #endregion

    #region 帮助方法
    #endregion





}

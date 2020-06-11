using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : View
{


    #region 常量

    const float grivaty = 9.8f;
    const float m_jumpValue = 5;
    const float m_moveSpeed = 13;

    const float m_SpeedAddDistance = 200;
    const float m_SpeedAddRate = 0.5f;
    const float m_MaxSpeed = 40;


    #endregion

    #region 事件
    #endregion

    #region 字段

    public float speed = 20;

    CharacterController m_cc;

    InputDirection m_currentInputDirection = InputDirection.NULL;

    bool activveInput = false;

    Vector3 m_mousePos;


    int m_nowIndex = 1;
    int m_targetIndex = 1;
    float m_xDistance;


    float m_yDistance;


    bool m_isSlide = false;
    float m_SlideTime;

    float m_SpeedAddCount;


    GameModel gameModel;

    // 记录速度
    float m_MaskSpeed;
    // 增加速度的速率
    float m_AddRate = 3;
    // 是否被撞击
    bool isHit = false;

    // Item 相关
    public int m_DoubleTime = 1;
    float m_skillTime;

    // 多倍积分协程
    IEnumerator MultiplyCor;

    // 吸铁石的碰撞体
    SphereCollider m_MagnetCollider;

    // 吸铁石协程
    IEnumerator MagnetCor;

    // 无敌状态协程
    IEnumerator InvinvibleCor;
    bool m_isInvinvible = false;


    // 和射门有关
    GameObject m_Ball;
    GameObject m_BallTrail;
    IEnumerator GoalCor;
    bool m_IsGoal = false;

    #endregion

    #region 属性
    public override string Name => Consts.V_PlayerMove;

    public float Speed { get => speed; set { speed = value < m_MaxSpeed ? value : m_MaxSpeed;

        } }
    #endregion

    #region 方法

    #region 移动
    IEnumerator UpdateAction() {

        while (true)
        {
            if (gameModel.IsPause == false && gameModel.IsPlay) {


                //更新 UI
                UpdateDistance();


                m_yDistance -= grivaty * Time.deltaTime;
                //Debug.Log("m_yDistance:" + m_yDistance);
                //Mathf.Max(m_yDistance,0);
                m_cc.Move(((Vector3.forward * Speed) + new Vector3(0, m_yDistance, 0)) * Time.deltaTime);



                UpdatePosition();

                MoveControl();

                UpdateSpeed();
            }


            yield return 0;
        }
    }

    void UpdateDistance() {
        DistanceArgs e = new DistanceArgs { distance = (int)transform.position.z };

        SendEvent(Consts.E_UpdateDisance,e);

        //Debug.Log("UpdateDistance SendEvent");
    }


    /// <summary>
    /// 更新速度
    /// </summary>
    private void UpdateSpeed()
    {
        m_SpeedAddCount += Speed * Time.deltaTime;
        if (m_SpeedAddCount > m_SpeedAddDistance) {
            m_SpeedAddCount = 0;
            Speed += m_SpeedAddRate;
        }
    }

    /// <summary>
    /// 获取输入的方向
    /// </summary>
    private void GetInputDirection()
    {
        m_currentInputDirection = InputDirection.NULL;


        // 鼠标滑动方向识别
        if (Input.GetMouseButtonDown(0)) {
            activveInput = true;
            m_mousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) && activveInput) {

            Vector3 Dir = Input.mousePosition - m_mousePos;

            if (Dir.magnitude > 20) {
                if (Mathf.Abs(Dir.x) > Mathf.Abs(Dir.y) && (Dir.x > 0))
                {
                    m_currentInputDirection = InputDirection.Right;
                }
                else if (Mathf.Abs(Dir.x) > Mathf.Abs(Dir.y) && (Dir.x < 0))
                {
                    m_currentInputDirection = InputDirection.Left;
                }
                else if (Mathf.Abs(Dir.x) < Mathf.Abs(Dir.y) && (Dir.y > 0))
                {
                    m_currentInputDirection = InputDirection.Up;
                }
                else if (Mathf.Abs(Dir.x) < Mathf.Abs(Dir.y) && (Dir.y < 0))
                {
                    m_currentInputDirection = InputDirection.Down;
                }

                activveInput = false;
            }



        }

        // 键盘输入
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) {
            m_currentInputDirection = InputDirection.Up;
        } else if (Input.GetKeyDown(KeyCode.S)) {
            m_currentInputDirection = InputDirection.Down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            m_currentInputDirection = InputDirection.Left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            m_currentInputDirection = InputDirection.Right;
        }


        //Debug.Log(m_currentInputDirection.ToString());
    }

    void UpdatePosition() {
        GetInputDirection();

        switch (m_currentInputDirection)
        {
            case InputDirection.NULL:
                break;
            case InputDirection.Right:
                if (m_targetIndex < 2) {
                    m_targetIndex++;
                    m_xDistance = 2;

                    Game.Instance.audioManager.PlayEffect("Se_UI_Slide");

                    SendMessage("AnimManager", m_currentInputDirection);
                }


                break;
            case InputDirection.Left:

                if (m_targetIndex > 0)
                {
                    m_targetIndex--;
                    m_xDistance = -2;

                    Game.Instance.audioManager.PlayEffect("Se_UI_Slide");

                    SendMessage("AnimManager", m_currentInputDirection);
                }
                break;
            case InputDirection.Down:
                if (m_isSlide == false) {
                    m_isSlide = true;
                    // roll 动画的时长
                    m_SlideTime = 0.733f;

                    Game.Instance.audioManager.PlayEffect("Se_UI_Huadong");

                    SendMessage("AnimManager", m_currentInputDirection);
                }

                break;
            case InputDirection.Up:
                // 在地面才能起跳
                if (m_cc.isGrounded) {

                    m_yDistance = m_jumpValue;

                    Game.Instance.audioManager.PlayEffect("Se_UI_Jump");

                    SendMessage("AnimManager", m_currentInputDirection);

                }
                break;
            default:
                break;
        }
    }


    void MoveControl() {

        // 水平移动
        if (m_targetIndex != m_nowIndex) {
            float move = Mathf.Lerp(0, m_xDistance, m_moveSpeed * Time.deltaTime);

            transform.position += new Vector3(move, 0, 0);
            m_xDistance -= move;

            if (Mathf.Abs(m_xDistance) < 0.005f) {
                m_xDistance = 0;
                m_nowIndex = m_targetIndex;

                switch (m_nowIndex)
                {
                    case 0:
                        transform.position = new Vector3(-2, transform.position.y, transform.position.z);

                        break;

                    case 1:
                        transform.position = new Vector3(0, transform.position.y, transform.position.z);

                        break;
                    case 2:
                        transform.position = new Vector3(2, transform.position.y, transform.position.z);

                        break;
                    default:
                        break;
                }

            }
        }

        //
        if (m_isSlide) {
            m_SlideTime -= Time.deltaTime;

            if (m_SlideTime < 0) {
                m_isSlide = false;
                m_SlideTime = 0;
            }
        }

    }

    #endregion

    // 减速
    public void HitObstacle() {
        if (isHit == true) {
            return;
        }

        isHit = true;
        m_MaskSpeed = Speed;
        Speed = 0;

        StartCoroutine(DecreaseSpeed());
    }

    IEnumerator DecreaseSpeed() {

        while (Speed < m_MaskSpeed) {
            Speed += Time.deltaTime + m_AddRate;
            yield return 0;
        }

        isHit = false;
    }

    /// <summary>
    /// 吃金币
    /// </summary>
    public void HitCoin() {
        // Debug.Log("迟到金币");
        CoinArgs args = new CoinArgs() {coin = m_DoubleTime };
        SendEvent(Consts.E_UpdateCoin,args);
    }

    /// <summary>
    /// 双倍金币技能（有技能时间）
    /// </summary>
    public void HitMultiply() {

        if (MultiplyCor != null) {
            StopCoroutine(MultiplyCor);
        }

        MultiplyCor = MultiplyCoroutine();
        StartCoroutine(MultiplyCor);
    }

    IEnumerator MultiplyCoroutine() {
        m_DoubleTime = 2;
        float timer = m_skillTime;
        while (timer > 0)
        {
            if (gameModel.IsPause == false && gameModel.IsPlay == true)
            {
                timer -= Time.deltaTime;
            }

            yield return 0;
        }

        //yield return new WaitForSeconds(m_skillTime);
        m_DoubleTime = 1;
    }

    /// <summary>
    /// 吸铁石功能
    /// </summary>
    public void HitMagnet() {
        if (MagnetCor != null) {
            StopCoroutine(MagnetCor);
        }
        MagnetCor = MagnetCoroutine();
        StartCoroutine(MagnetCor);
    }

    IEnumerator MagnetCoroutine() {
        m_MagnetCollider.enabled = true;

        float timer = m_skillTime;
        while (timer > 0) {
            if (gameModel.IsPause ==false && gameModel.IsPlay == true) {
                timer -= Time.deltaTime;
            }

            yield return 0;
        }

        //yield return new WaitForSeconds(m_skillTime);
        m_MagnetCollider.enabled = false;
    }





    /// <summary>
    /// 加时功能
    /// </summary>
    public void HitAddTime()
    {
        Debug.Log("Hit Add Time");
        SendEvent(Consts.E_HitAddTime);
    }

    public void HitItem(ItemKind kind) {

        ItemArgs args = new ItemArgs
        {
            spendCount = 0,
            itemKind = kind
        };

        SendEvent(Consts.E_HitItem, args);
        //switch (kind)
        //{
        //    case ItemKind.ItemMagnet:
        //        HitMagnet();
        //        break;
        //    case ItemKind.ItemMultiply:
        //        HitMultiply();
        //        break;
        //    case ItemKind.ItemInvincible:
        //        HitInvinvible();
        //        break;
        //    default:
        //        break;
        //}
    }

    /// <summary>
    /// 无敌状态
    /// </summary>
    public void HitInvinvible() {

        Debug.Log("Hit Invinvible");

        if (InvinvibleCor != null) {
            StopCoroutine(InvinvibleCor);
        }

        InvinvibleCor = InvinvibleCoroutine();
        StartCoroutine(InvinvibleCor);
    }

    IEnumerator InvinvibleCoroutine() {
        m_isInvinvible = true;
        float timer = m_skillTime;
        while (timer > 0)
        {
            if (gameModel.IsPause == false && gameModel.IsPlay == true)
            {
                timer -= Time.deltaTime;
            }

            yield return 0;

           // print("timer: "+timer);
        }

        //yield return new WaitForSeconds(m_skillTime);
        m_isInvinvible = false;
    }




    /*************************************************/
    /********************射门相关************************/

    public void OnGoalClick() {

        if (GoalCor != null)
        {
            StopCoroutine(GoalCor);
        }
        SendMessage("MessagePlayGoal");
        m_BallTrail.SetActive(true);
        m_Ball.SetActive(false);

        GoalCor = MoveBallTrail();
        StartCoroutine(GoalCor);

        // 新一轮射球的时候 m_IsGoal 设置为 false
        m_IsGoal = false;
    }

    IEnumerator MoveBallTrail() {
        while (true)
        {
            if (gameModel.IsPause ==false && gameModel.IsPlay ==true)
            {
                m_BallTrail.transform.Translate(transform.forward * 40 * Time.deltaTime);
            }
            

            yield return 0;

        }
    }

    /// <summary>
    /// 进球了
    /// </summary>
    public void HitBallDoor() {
        // 关闭拖尾球的协程
        StopCoroutine(GoalCor);

        // 拖尾球归位
        m_BallTrail.transform.localPosition = new Vector3(0,1.5f,7.19f);
        m_BallTrail.SetActive(false);
        m_Ball.SetActive(true);

        // m_IsGoal = true;
        m_IsGoal = true;

        // 进门特效
        Game.Instance.objectPool.Spawn("FX_GOAL", m_BallTrail.transform.parent);

        // 播放音效
        Game.Instance.audioManager.PlayEffect("Se_UI_Goal");

        // 进球 UI 球的个数 +1
        SendEvent(Consts.E_ShootGoal);
    }

    #endregion

    #region Unity回调


    private void Awake()
    {
        m_cc = GetComponent<CharacterController>();
        gameModel = GetModel<GameModel>();
        m_skillTime = gameModel.SkillTime;

        m_MagnetCollider = GetComponentInChildren<SphereCollider>();

        m_Ball = transform.Find("Ball").gameObject;
        m_BallTrail = GameObject.Find("BallTrail").gameObject;
        m_BallTrail.SetActive(false);
    }


    private void Start()
    {
        StartCoroutine(UpdateAction());

        
    }

    //测试时候用
    //public Transform pos;
    private void Update()
    {

        //测试时候用
        //if (Input.GetKeyDown(KeyCode.V)) {
        //    gameModel.IsPause = true;
        //}
        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    gameModel.IsPause = false;
        //}

        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    Game.Instance.objectPool.Spawn("Block_People",pos);
        //}
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tag.SmallFence)
        {
            // 无敌状态
            if (m_isInvinvible == true) {
                return;
            }

            other.gameObject.SendMessage("HitPlayer", transform.position);

            HitObstacle();

            // 声音
            Game.Instance.audioManager.PlayEffect("Se_UI_Hit");

        }
        else if (other.gameObject.tag == Tag.BigFence)
        {

            // 无敌状态
            if (m_isInvinvible == true)
            {
                return;
            }


            if (m_isSlide)
            {
                return;
            }

            other.gameObject.SendMessage("HitPlayer", transform.position);

            HitObstacle();

            // 声音
            Game.Instance.audioManager.PlayEffect("Se_UI_Hit");

        }
        else if (other.gameObject.tag == Tag.Block) {



            other.gameObject.SendMessage("HitPlayer", transform.position);

            // 声音
            Game.Instance.audioManager.PlayEffect("Se_UI_End");

            // 发送事件
            SendEvent(Consts.E_EndGameController);
        }
        else if (other.gameObject.tag == Tag.SmallBlock)
        {



            other.transform.parent.parent.SendMessage("HitPlayer", transform.position);

            // 声音
            Game.Instance.audioManager.PlayEffect("Se_UI_End");

            // 发送事件
            SendEvent(Consts.E_EndGameController);
        }
        else if (other.gameObject.tag == Tag.BeforeTrigger)// 汽车触发
        {
            Debug.Log("other.gameObject.tag == Tag.BeforeTrigger");
            other.transform.parent.SendMessage("HitTrigger", SendMessageOptions.RequireReceiver);

        }
        else if (other.gameObject.tag == Tag.BeforeGoalTrigger) // 球门射球触发
        {
            Debug.Log("other.gameObject.tag == Tag.BeforeGoalTrigger");
            SendEvent(Consts.E_HitGoalTrigger);

            // 加速特效
            Game.Instance.objectPool.Spawn("FX_SpeedUp", m_BallTrail.transform.parent);

        } else if (other.gameObject.tag == Tag.Goalkeeper) {
            // 减速
            HitObstacle();

            // 撞飞守门员
            other.transform.parent.parent.parent.SendMessage("HitGoalkeeper",SendMessageOptions.RequireReceiver);
        }
        else if (other.gameObject.tag == Tag.BallDoor)
        {
            if (m_IsGoal)
            {
                m_IsGoal = false;
                return;
            }
            // 减速
            HitObstacle();

            // 撞到球网
            Game.Instance.objectPool.Spawn("Effect_QiuWang",m_BallTrail.transform.parent);

            // 球门动画
            other.transform.parent.parent.SendMessage("HitGoalDoor",m_nowIndex);
        }
    }


    #endregion

    #region 事件回调

    public override void HandleEvent(string name, object data)
    {
        switch (name)
        {
            case Consts.E_ClickGoalButton:
                OnGoalClick();

                break;

            default:
                break;
        }
    }

    public override void RegisterAttentionEvent()
    {
        AttentionList.Add(Consts.E_ClickGoalButton);
    }

    #endregion

    #region 帮助方法
    #endregion
}

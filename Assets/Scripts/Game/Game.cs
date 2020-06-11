using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ObjectPool),typeof(AudioManager),typeof(StaticData))]
public class Game : MonoSingleton<Game>
{
    [HideInInspector]
    public ObjectPool objectPool;
    [HideInInspector]
    public AudioManager audioManager;
    [HideInInspector]
    public StaticData staticData;

    // Start is called before the first frame update
    void Start()
    {
        // 过场不销毁事件
        DontDestroyOnLoad(gameObject);

        // 初始化赋值
        objectPool = ObjectPool.Instance;
        audioManager = AudioManager.Instance;
        staticData = StaticData.Instance;

        

        // 注册Controller
        RegisterControoler(Consts.E_SteupController,typeof(SteupController));


        // 游戏启动
        SendEvent(Consts.E_SteupController);

        // 跳转场景
        LoadLevel(4);


    }

    /// <summary>
    /// 加载目标场景
    /// </summary>
    /// <param name="level"></param>
    public void LoadLevel(int level)
    {
        // 退出场景的事件发送
        ScenesArgs e = new ScenesArgs()
        {
            sceneIndex = SceneManager.GetActiveScene().buildIndex
        };

        SendEvent(Consts.E_ExitEvent,e);

        // 加载新场景
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }


    /// <summary>
    /// (MonoBehaviour自带)场景加载完成的事件
    /// </summary>
    /// <param name="level"></param>
    private void OnLevelWasLoaded(int level)
    {
        Debug.Log("进入新场景"+level);

        // 发送进入场景事件
        ScenesArgs e = new ScenesArgs() {
            sceneIndex = level
        };
        
        SendEvent(Consts.E_EnterSceneController, e);

    }


    /// <summary>
    /// 发送事件
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="data"></param>
    void SendEvent(string eventName, object data =null) {
        MVC.SendEvent(eventName, data);
    }

    /// <summary>
    /// 注册 COntroller
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="type"></param>
    void RegisterControoler(string eventName, Type type) {
        MVC.RegisterControoler(eventName,type);
    }
}

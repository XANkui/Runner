
using System.Collections.Generic;
using UnityEngine;

public abstract class View : MonoBehaviour
{
    // View 名称
    public abstract string Name { get; }

    // 事件列表
    [HideInInspector]
    public List<string> AttentionList = new List<string>();

    /// <summary>
    /// 注册事件
    /// </summary>
    public virtual void RegisterAttentionEvent() { }

    /// <summary>
    /// 处理事件
    /// </summary>
    /// <param name="name"></param>
    /// <param name="data"></param>
    public abstract void HandleEvent(string name, object data);

    /// <summary>
    /// 发送事件
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="data"></param>
    protected void SendEvent(string eventName, object data = null)
    {
        MVC.SendEvent(eventName, data);
    }

    /// <summary>
    /// 获取模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected T GetModel<T>() where T : Model
    {
        return MVC.GetModel<T>() as T;
    }

}

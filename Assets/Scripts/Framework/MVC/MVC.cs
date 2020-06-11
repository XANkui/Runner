using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MVC 管理
/// </summary>
public static class MVC  
{
    // Model 集合
    public static Dictionary<string, Model> Models = new Dictionary<string, Model>();

    // View 集合
    public static Dictionary<string, View> Views = new Dictionary<string, View>();

    // Control 集合
    public static Dictionary<string, Type> CommandMap = new Dictionary<string, Type>();

    /// <summary>
    /// 注册 Model
    /// </summary>
    /// <param name="model"></param>
    public static void RegisterModel(Model model) {
        Models[model.Name] = model;
    }

    /// <summary>
    /// 注册 VIew
    /// </summary>
    /// <param name="view"></param>
    public static void RegisterView(View view)
    {
        // 防止 来回跳转场景， View 重复调用
        if (Views.ContainsKey(view.Name)) {
            Views.Remove(view.name);
        }

        view.RegisterAttentionEvent();

        Views[view.Name] = view;
    }

    /// <summary>
    /// 注册 COntroller
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="type"></param>
    public static void RegisterControoler(string eventName, Type type) {
        CommandMap[eventName] = type;
    }

    /// <summary>
    /// 获得 对应类型的 Model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetModel<T>() where T : Model{
        foreach (Model item in Models.Values)
        {
            if (item is T) {
                return (T)item;
            }
        }

        return null;
    }

    /// <summary>
    /// 获得 对应类型的View
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetView<T>() where T : View
    {
        foreach (View item in Views.Values)
        {
            if (item is T)
            {
                return (T)item;
            }
        }

        return null;
    }

    /// <summary>
    /// 发送事件
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="data"></param>
    public static void SendEvent(string eventName, object data = null) {

        if (CommandMap.ContainsKey(eventName)) {
            Type t = CommandMap[eventName];

            // 控制器生成
            Controller c = Activator.CreateInstance(t) as Controller;
            c.Excute(data);

        }

        // View 处理
        foreach (View item in Views.Values)
        {
            if (item.AttentionList.Contains(eventName)) {
                item.HandleEvent(eventName, data);
            }
        }

    }
}

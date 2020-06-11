using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller 
{
    public abstract void Excute(object data);

    /// <summary>
    /// 获取模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected T GetModel<T>() where T : Model
    {
        return MVC.GetModel<T>() as T;
    }

    /// <summary>
    /// 获取试图
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected T GetView<T>() where T : View
    {
        return MVC.GetView<T>() as T;
    }

    /// <summary>
    /// 注册 Model
    /// </summary>
    /// <param name="model"></param>
    protected static void RegisterModel(Model model)
    {
        MVC.RegisterModel(model);
    }

    /// <summary>
    /// 注册 VIew
    /// </summary>
    /// <param name="view"></param>
    protected static void RegisterView(View view)
    {
        MVC.RegisterView(view);
    }

    /// <summary>
    /// 注册 COntroller
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="type"></param>
    protected static void RegisterControoler(string eventName, Type type)
    {
        MVC.RegisterControoler(eventName, type);
    }
}

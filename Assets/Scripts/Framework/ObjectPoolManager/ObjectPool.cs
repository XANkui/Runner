using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对象池管理类
/// </summary>
public class ObjectPool : MonoSingleton<ObjectPool>
{
    // 资源路径
    
    public  string ResourceDir = "";

    // 对象池字典集合
    Dictionary<string, SubPool> m_pools = new Dictionary<string, SubPool>();

    /// <summary>
    /// 取出对象
    /// </summary>
    /// <param name="name"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public GameObject Spawn(string name, Transform parent) {
        SubPool pool = null;

        if (m_pools.ContainsKey(name) == false) {

            RegisterNewPool(name, parent);
        }

        pool = m_pools[name];

        return pool.Spawn();
    }

    /// <summary>
    /// 释放对象
    /// </summary>
    /// <param name="go"></param>
    public void Unspawn(GameObject go) {

        SubPool pool = null;
        foreach (SubPool item in m_pools.Values)
        {
            if (item.Contain(go)) {
                pool = item;
                    
                break;
            }
        }

        pool.Unspawn(go);
    }

    /// <summary>
    /// 回收所有
    /// </summary>
    public void UnspawnAll() {
        foreach (SubPool item in m_pools.Values)
        {
            item.UnspawnAll();
        }
    }

    /// <summary>
    /// 构建新的子对象池
    /// </summary>
    /// <param name="name"></param>
    /// <param name="parent"></param>
    private void RegisterNewPool(string name, Transform parent)
    {
        // 从资源路径中加载资源
        string path = ResourceDir + "/" + name;
        GameObject go = Resources.Load(path) as GameObject;

        if (go == null) {

            Debug.Log("资源加载为空，请检查加载路径");

            return;
        }
        // 构建子对象池
        SubPool pool = new SubPool(parent,go);

        // 添加到字典中管理
        m_pools.Add(pool.Name, pool);
    }
}

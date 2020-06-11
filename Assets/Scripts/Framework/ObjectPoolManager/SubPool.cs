using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPool
{
    // 对象集合
    List<GameObject> m_objects = new List<GameObject>();

    // 对象预制体
    GameObject m_prefab;

    // 对象父物体
    Transform m_parent;

    // 对象池名称
    public string Name {
        get {
            return m_prefab.name;
        }
    }

    /// <summary>
    /// 对象池构造函数
    /// </summary>
    /// <param name="parent">父物体</param>
    /// <param name="prefab">预制体</param>
    public SubPool(Transform parent, GameObject prefab) {
        m_prefab = prefab;
        m_parent = parent;
    }


    public GameObject Spawn() {

        GameObject go = null;

        // 从对象集合中取对象
        foreach (GameObject item in m_objects)
        {
            if (item.activeSelf == false) {
                go = item;
            }
        }

        // 对像集合中没有取到
        if (go == null) {

            // 新生成对象，并添加到集合中
            go = GameObject.Instantiate(m_prefab);
            go.transform.parent = m_parent;
            m_objects.Add(go);
        }

        //显示物体，并发送消息
        go.SetActive(true);
        go.SendMessage("OnSpawn", SendMessageOptions.DontRequireReceiver);

        return go;
    }

    /// <summary>
    /// 回收对象
    /// </summary>
    /// <param name="gameObject"></param>
    public void Unspawn(GameObject gameObject) {

        // 判断游戏物体是否在该对象集合里面
        if (Contain(gameObject)) {
            //发送消息，回收物体，不一定要有接受对象
            gameObject.SendMessage("OnUnspawn", SendMessageOptions.DontRequireReceiver);
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 回收所有对象
    /// </summary>
    public void UnspawnAll() {

        foreach (GameObject item in m_objects)
        {
            // 判断是否激活
            if (item.activeSelf) {
                Unspawn(item);
            }
        }
    }

    /// <summary>
    /// 判断集合中是否包含
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public bool Contain(GameObject gameObject) {
        return m_objects.Contains(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : ResuableObject
{
    public override void OnSpawn()
    {
       
    }

    public override void OnUnspawn()
    {
        // 回收跑到没被吃的道具/或障碍物
        var itemChild = transform.Find("Item");
        if (itemChild != null)
        {
            foreach (Transform child in itemChild)
            {
                Game.Instance.objectPool.Unspawn(child.gameObject);
            }
        }
    }
}

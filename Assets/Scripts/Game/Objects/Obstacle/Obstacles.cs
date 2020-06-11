using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : ResuableObject
{
    protected Transform effectParent;

    protected virtual void Awake()
    {
        effectParent = GameObject.Find("EffectsParent").transform;
    }

    public override void OnSpawn()
    {
        
    }

    public override void OnUnspawn()
    {
        
    }

    public virtual void HitPlayer(Vector3 pos) {

        // 生成特效
        GameObject go = Game.Instance.objectPool.Spawn("FX_ZhuangJi",effectParent);
        go.transform.position = pos;

        

        // 回收
        Game.Instance.objectPool.Unspawn(gameObject);
        //Destroy(gameObject);

    }
    
}

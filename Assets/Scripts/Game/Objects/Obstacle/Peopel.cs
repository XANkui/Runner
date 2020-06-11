using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peopel : Obstacles
{
    private bool isHitted = false;
    public float speed = 10;
    private bool isFly = false;
    private Animation anim;
    protected override void Awake()
    {
        base.Awake();
        anim = GetComponentInChildren<Animation>();

        // test
        StartCoroutine(huishou());
        
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
        anim.Play("run");
    }

    public override void OnUnspawn()
    {
        base.OnUnspawn();
        anim.transform.localPosition = Vector3.zero;
        isHitted = false;
        isFly = false;
    }


    public void HitTrigger() {
        isHitted = true;
    }
    public override void HitPlayer(Vector3 pos)
    {
        // 生成特效
        GameObject go = Game.Instance.objectPool.Spawn("FX_ZhuangJi", effectParent);
        go.transform.position = pos;
        isHitted = false;
        isFly = true;
        anim.Play("fly");
    }

    private void Update()
    {
        if (isHitted) {
            transform.position -= new Vector3(0,0,speed)*Time.deltaTime;
        }

        if (isFly == true) {
            transform.position += new Vector3(0, speed, speed) * Time.deltaTime;
        }
    }

    IEnumerator huishou() {
        yield return new WaitForSeconds(3);

        Game.Instance.objectPool.Unspawn(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{

    public float moveSpeed = 40;

    public override void HitPlayer(Vector3 pos)
    {
        // 特效
        GameObject go = Game.Instance.objectPool.Spawn("FX_JinBi", effctParent);
        go.transform.position = pos;

        // 声音
        Game.Instance.audioManager.PlayEffect("Se_UI_JinBi");

        // 回收
        Game.Instance.objectPool.Unspawn(gameObject);
        //Destroy(gameObject);
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
    }

    public override void OnUnspawn()
    {
        base.OnUnspawn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.Player) {
            HitPlayer(other.transform.position);

            other.SendMessage("HitCoin", SendMessageOptions.RequireReceiver);
        } else if (other.tag == Tag.MagnetCollider) {
            // 飞向玩家
            StartCoroutine(HitMagnet(other.transform));
        }
    }


    IEnumerator HitMagnet(Transform pos) {
        bool isLoop = true;

        while (isLoop) {
            transform.position = Vector3.Lerp(transform.position,pos.position,moveSpeed*Time.deltaTime);

            if (Vector3.Distance(transform.position, pos.position) < 0.5f) {
                isLoop = false;
                HitPlayer(pos.position);
                pos.parent.SendMessage("HitCoin",SendMessageOptions.RequireReceiver);


            }

            yield return 0;
        }
    }

}

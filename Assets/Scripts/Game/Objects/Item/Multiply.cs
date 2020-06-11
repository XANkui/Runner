using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiply : Item
{
    public override void HitPlayer(Vector3 pos)
    {


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
        if (other.tag == Tag.Player)
        {
            HitPlayer(other.transform.position);

            //other.SendMessage("HitMultiply", SendMessageOptions.RequireReceiver);
            other.SendMessage("HitItem", ItemKind.ItemMultiply);
        }
    }
}

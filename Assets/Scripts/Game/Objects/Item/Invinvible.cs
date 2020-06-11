using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invinvible : Item
{
    public override void HitPlayer(Vector3 pos)
    {
        // 声音
        Game.Instance.audioManager.PlayEffect("Se_UI_Revival");

        // 回收
        Game.Instance.objectPool.Unspawn(gameObject);
        //Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.Player) {

            HitPlayer(other.transform.position);
            //other.SendMessage("HitInvinvible", SendMessageOptions.RequireReceiver);
            other.SendMessage("HitItem", ItemKind.ItemInvincible);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTime : Item
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.Player) {
            HitPlayer(other.transform.position);

            other.SendMessage("HitAddTime",SendMessageOptions.RequireReceiver);
        }
    }

    public override void HitPlayer(Vector3 pos)
    {
        // 声音
        Game.Instance.audioManager.PlayEffect("Se_UI_Time");

        // 回收
        Game.Instance.objectPool.Unspawn(gameObject);
        //Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : ResuableObject
{
    public float time = 1;

    public override void OnSpawn()
    {
        StartCoroutine(DestroyCoroutine());
    }

    public override void OnUnspawn()
    {
        StopAllCoroutines();
    }


    IEnumerator DestroyCoroutine() {
        yield return new WaitForSeconds(time);

        Game.Instance.objectPool.Unspawn(gameObject);
    }
}

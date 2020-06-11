using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : Effect
{
    public override void OnSpawn()
    {
        this.transform.localPosition = new Vector3(0,0, -4.433332f);
        this.transform.localScale = Vector3.one * 1.835967f;

        base.OnSpawn();
    }

    public override void OnUnspawn()
    {
        base.OnUnspawn();
    }
}

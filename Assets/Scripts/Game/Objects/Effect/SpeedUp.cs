using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : Effect
{
    public override void OnSpawn()
    {
        base.OnSpawn();

        transform.localPosition = new Vector3(0,0, 0.4333337f);
        transform.localScale = Vector3.one * 3.333333f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ResuableObject
{
    public float rotateSpeed = 60;
    protected Transform effctParent;
    protected virtual void Awake()
    {
        effctParent = GameObject.Find("EffectsParent").transform;
    }

    public override void OnSpawn()
    {
        
    }

    public override void OnUnspawn()
    {
        transform.localEulerAngles = Vector3.zero;
    }

    private void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    public virtual void HitPlayer(Vector3 pos) {

    }
}

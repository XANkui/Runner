using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Obstacles
{
    public bool isCaneMove = false;

    private bool isBlock = false;

    float speed = 5.0f;
    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
    }

    public override void OnUnspawn()
    {
        isBlock = false;
        base.OnUnspawn();

    }

    public override void HitPlayer(Vector3 pos)
    {
        base.HitPlayer(pos);
    }

    public void HitTrigger() {
        isBlock = true;
    }

    private void Update()
    {
        if (isBlock == true && isCaneMove == true) {
            transform.Translate(transform.forward * speed * Time.deltaTime * -1);
        }
    }
}

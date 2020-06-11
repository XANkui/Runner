using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGoal : ResuableObject
{
    public Animation goalkeeper;
    public Animation door;
    public GameObject net;

    public float seppd = 20;
    public bool isFlay = false;

    public override void OnSpawn()
    {
        goalkeeper.transform.position = Vector3.zero;
        goalkeeper.Play("standard");
    }

    public override void OnUnspawn()
    {
        goalkeeper.transform.localPosition = Vector3.zero;
        goalkeeper.Play("standard");
        door.Play("QiuMen_St");
        net.SetActive(true);
        goalkeeper.gameObject.SetActive(true);
        goalkeeper.transform.parent.parent.gameObject.SetActive(true);
        isFlay = false;
        StopAllCoroutines();
    }

    /// <summary>
    /// 进球之后，守门员隐藏
    /// </summary>
    public void ShootAGoal(int position) {
        // 隐藏守门员（带碰撞体的父物体）
        switch (position)
        {
            case -2:
                goalkeeper.Play("left_flutter");

                break;

            case 0:
                goalkeeper.Play("flutter");

                break;

            case 2:
                goalkeeper.Play("right_flutter");

                break;

            default:
                break;

        }


        StartCoroutine(HideGoalkeeper());
        
       
    }

    IEnumerator HideGoalkeeper() {
        yield return new WaitForSeconds(1);
        goalkeeper.transform.parent.parent.gameObject.SetActive(false);
    }


    public void HitGoalkeeper() {
        isFlay = true;
        goalkeeper.Play("fly");
    }

    private void Update()
    {
        if (isFlay)
        {
            goalkeeper.transform.position += new Vector3(0,seppd,seppd)*Time.deltaTime;
        }
    }

    public void HitGoalDoor(int nowIndex) {
        net.SetActive(false);

        switch (nowIndex)
        {
            case 0:
                door.Play("QiuMen_RR");
                break;

            case 1:
                door.Play("QiuMen_St");
                break;

            case 2:
                door.Play("QiuMen_LR");
                break;


            default:
                break;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : View
{

    Animation anim;
    Action PlayAnim;

    GameModel gameModel;

    private void Awake()
    {
        anim = GetComponent<Animation>();
        PlayAnim = PlayRun;
        gameModel = GetModel<GameModel>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameModel.IsPause == false && gameModel.IsPlay)
        {
            if (PlayAnim != null)
            {
                PlayAnim();
            }
        }
        else {
            anim.Stop();
        }

         
    }

    void PlayRun() {
        anim.Play("run");
    }

    void PlayLeft() {
        anim.Play("left_jump");
        if (anim["left_jump"].normalizedTime > 0.95f) {
            PlayAnim = PlayRun;
        }
    }

    void PlayRight()
    {
        anim.Play("right_jump");
        if (anim["right_jump"].normalizedTime > 0.95f)
        {
            PlayAnim = PlayRun;
        }
    }

    void PlayRoll()
    {
        anim.Play("roll");
        if (anim["roll"].normalizedTime > 0.95f)
        {
            PlayAnim = PlayRun;
        }
    }

    void PlayJump()
    {
        anim.Play("jump");
        if (anim["jump"].normalizedTime > 0.95f)
        {
            PlayAnim = PlayRun;
        }
    }

    void PlayShoot()
    {
        anim.Play("Shoot01");
        if (anim["Shoot01"].normalizedTime > 0.95f)
        {
            PlayAnim = PlayRun;
        }
    }

    public void MessagePlayGoal() {
        PlayAnim = PlayShoot;
    }

    public void AnimManager(InputDirection dir) {
        switch (dir)
        {
            case InputDirection.NULL:
                PlayAnim = PlayRun;
                break;
            case InputDirection.Right:
                PlayAnim = PlayRight;
                break;
            case InputDirection.Left:
                PlayAnim = PlayLeft;
                break;
            case InputDirection.Down:
                PlayAnim = PlayRoll;
                break;
            case InputDirection.Up:
                PlayAnim = PlayJump;
                break;
            default:
                break;
        }
    }

    public override string Name => Consts.V_PlayerAnim;

    public override void HandleEvent(string name, object data)
    {
        
    }
}

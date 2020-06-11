using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeGameController : Controller
{
    public override void Excute(object data)
    {
        UIResume resume = GetView<UIResume>();
        resume.StartCount();
    }
}

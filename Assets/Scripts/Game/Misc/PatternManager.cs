using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PatternManager : MonoSingleton<PatternManager>
{
    public List<Pattern> Patterns = new List<Pattern>(); 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


/// <summary>
/// 可序列化
/// </summary>
[Serializable]
public class PatternItem {
    public string prefabName;
    public Vector3 postion;
}

/// <summary>
/// 可序列化
/// </summary>
[Serializable]
public class Pattern {
    public List<PatternItem> PatternItems = new List<PatternItem>();
}

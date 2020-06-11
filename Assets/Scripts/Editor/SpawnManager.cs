using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnManager : EditorWindow
{
    [MenuItem("Tools/Record GameObject In Road To PatternManager")]
    static void PatternSystem() {

        GameObject spawnManager = GameObject.Find("PatternManager");

        if (spawnManager != null)
        {
            var patternManager = spawnManager.GetComponent<PatternManager>();

            // 当前鼠标选中一个游戏物体
            if (Selection.gameObjects.Length ==1)
            {
                var item = Selection.gameObjects[0].transform.Find("Item");
                if (item != null)
                {
                    Pattern pattern = new Pattern();
                    foreach (var child in item)
                    {
                        Transform childrens = child as Transform;
                        if (childrens != null)
                        {
                            // 从资源库中获取该游戏物体的预制体
                            var prefab = PrefabUtility.GetPrefabParent(childrens.gameObject);
                            if (prefab !=null)
                            {
                                PatternItem patternItem = new PatternItem {

                                    postion = childrens.localPosition,
                                    prefabName = prefab.name
                                };

                                pattern.PatternItems.Add(patternItem);

                            }
                        }
                    }
                    // 把 数据添加到 PatternManager
                    patternManager.Patterns.Add(pattern);
                }
            }
        }

    }
}

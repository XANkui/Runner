using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadChange : MonoBehaviour
{

    public GameObject roadNow;
    public GameObject roadNext;

    private GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        if (parent == null) {
            parent = new GameObject();
            parent.transform.position = Vector3.zero;
            parent.name = "Road";
        }
        roadNow = Game.Instance.objectPool.Spawn("Pattern_1", parent.transform);
        roadNext = Game.Instance.objectPool.Spawn("Pattern_2", parent.transform);
        roadNext.transform.position = roadNow.transform.position + new Vector3(0, 0, 160);

        AddItem(roadNow);
        AddItem(roadNext);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tag.Road) {
            Game.Instance.objectPool.Unspawn(other.gameObject);

            SpawnNewRoad();
        }
    }

    private void SpawnNewRoad()
    {
        int i = UnityEngine.Random.Range(1,5);

        roadNow = roadNext;

        roadNext = Game.Instance.objectPool.Spawn("Pattern_"+i.ToString(),parent.transform);
        roadNext.transform.position = roadNow.transform.position + new Vector3(0,0,160);

        AddItem(roadNext);
    }

    /// <summary>
    /// 生成跑道上的物体
    /// </summary>
    /// <param name="obj"></param>
    public void AddItem(GameObject obj) {
        var itemChild = obj.transform.Find("Item");
        if (itemChild != null)
        {
            var patterManager = PatternManager.Instance;
            if (patterManager != null && patterManager.Patterns !=null && patterManager.Patterns.Count>0)
            {
                var pattern = patterManager.Patterns[UnityEngine.Random.Range(0, patterManager.Patterns.Count)];
                if (pattern !=null && pattern.PatternItems != null && pattern.PatternItems.Count>0)
                {
                    foreach (var item in pattern.PatternItems)
                    {
                        GameObject go = Game.Instance.objectPool.Spawn(item.prefabName,itemChild);
                        go.transform.SetParent(itemChild);
                        go.transform.localPosition = item.postion;
                    }
                }
            }
        }
    }
}

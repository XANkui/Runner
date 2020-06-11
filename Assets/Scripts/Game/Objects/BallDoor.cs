using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDoor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.BallTrail)
        {
            other.transform.parent.parent.SendMessage("HitBallDoor",SendMessageOptions.RequireReceiver);
            transform.parent.parent.SendMessage("ShootAGoal", (int)other.transform.position.x);
        }
    }
}

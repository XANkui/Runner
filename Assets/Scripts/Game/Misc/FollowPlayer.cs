using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    Transform m_player;
    Vector3 m_offset;
    float speed = 20;

    private void Awake()
    {
        m_player = GameObject.FindWithTag(Tag.Player).transform;
        m_offset = transform.position - m_player.position;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, m_offset + m_player.position,speed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resawn : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform spwanpoint;
    [SerializeField] float spwanValue;
     void Update()
    {
        if(player.transform.position.y < -spwanValue)
        {
            RespawnPoint();
        }
    }

    void RespawnPoint()
    {
        transform.position = spwanpoint.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    PlayerRespawn playerRespawn;
    void Start()
    {
        playerRespawn = FindObjectOfType<PlayerRespawn>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerRespawn.UpdateRespawnPoint(transform.position);
    }
}

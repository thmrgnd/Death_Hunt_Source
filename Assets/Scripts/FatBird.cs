using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatBird : Item
{
    [SerializeField] GameObject mushroom;
    DeathManager deathManager;
    PlayerInteraction playerInteraction;
    bool diedOfBird;

    void Start()
    {
        playerInteraction = FindObjectOfType<PlayerInteraction>();
        deathManager = FindObjectOfType<DeathManager>();
    }

    void Update()
    {
        
    }

    public void KillTheBird()
    {
        gameObject.SetActive(false);
        mushroom.SetActive(true);
    }
}

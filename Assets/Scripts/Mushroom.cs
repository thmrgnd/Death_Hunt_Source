using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Item
{
    [SerializeField] GameObject initialPosition;
    DeathManager deathManager;
    PlayerInteraction playerInteraction;
    bool diedOfMushroom;
    void Start()
    {
        deathManager = FindObjectOfType<DeathManager>();
        playerInteraction = FindObjectOfType<PlayerInteraction>();
    }

    void Update()
    {
        
    }

    public override void EatItem()
    {
        if (diedOfMushroom)
            return;
        diedOfMushroom = true;
        deathManager.SetDiedOfMushroom();
    }

 }

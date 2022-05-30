using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonAlien : Item
{
    [SerializeField] GameObject alien;
    [SerializeField] Canvas killedAlienCanvas;
    [SerializeField] Sprite deadAlien;
    bool diedByAlien;
    DeathManager deathManager;
    CanvasesController canvasesController;
    void Start()
    {
        deathManager = FindObjectOfType<DeathManager>();
        canvasesController = FindObjectOfType<CanvasesController>();
    }

    void Update()
    {
        
    }


    public void SummonTheAlien()
    {
        alien.SetActive(true);
        if (diedByAlien)
            return;
        diedByAlien = true;
        Invoke("KilledByAlien", 3f);
    }

    public void KilledByAlien()
    {
        deathManager.SetDiedOfAlien();
    }

    public void KillTheAlien()
    {
        alien.GetComponentInChildren<SpriteRenderer>().sprite = deadAlien;
        canvasesController.UpdateCanvasToDisplay(killedAlienCanvas);
    }
}

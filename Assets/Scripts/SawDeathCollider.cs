using UnityEngine;

public class SawDeathCollider : MonoBehaviour
{
    DeathManager deathManager;
    PlayerInteraction playerInteraction;
    bool diedBySaw;
    void Start()
    {
        deathManager = FindObjectOfType<DeathManager>();
        playerInteraction = FindObjectOfType<PlayerInteraction>();
    }

    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (diedBySaw)
                return;
            diedBySaw = true;
            deathManager.SetDiedOfSaw();
        }
    }
}

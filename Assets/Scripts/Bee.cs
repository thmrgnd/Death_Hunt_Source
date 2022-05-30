using UnityEngine;
public class Bee : Item
{
    [SerializeField] GameObject honeyToSpawn;
    DeathManager deathManager;
    bool diedOfBee;

    void Start()
    {
        deathManager = FindObjectOfType<DeathManager>();
    }

    void Update()
    {

    }

    public override void UseItem()
    {
        KilledByBee();
    }

    public override void EatItem()
    {
        KilledByBee();
    }

    private void KilledByBee()
    {
        if (diedOfBee)
        {
            honeyToSpawn.gameObject.SetActive(true);
            gameObject.SetActive(false);
            return;
        }
        diedOfBee = true;
        deathManager.SetDiedOfBee();
    }

    public override void PickUpItem()
    {
        KilledByBee();
    }
    public override bool CheckPickUpItem()
    {
        return true;
    }
}

public class Saw : Item
{
    DeathManager deathManager;
    PlayerInteraction playerInteraction;
    bool diedBySaw;
    bool eatSaw;
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
        if (!eatSaw)
        {
            eatSaw = true;
            deathManager.SetDiedOfEatSaw();
        }
    }

}

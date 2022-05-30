using UnityEngine;

public class Honey : Item
{
    [SerializeField] GameObject beesToSpawn;
    [SerializeField] int timesToDieFromEatingHoney;
    [SerializeField] GameObject initialPosition;
    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] Color32 colorWhenEating;
    [SerializeField] Canvas killDeathWithHoney;
    PlayerInteraction playerInteraction;
    DeathManager deathManager;
    CanvasesController canvasesController;

    int timesEatenHoney;
    bool diedOfBird;
    bool killDeath;

    void Start()
    {
        deathManager = FindObjectOfType<DeathManager>();
        playerInteraction = FindObjectOfType<PlayerInteraction>();
        canvasesController = FindObjectOfType<CanvasesController>();
    }

    void Update()
    {

    }

    public override void PutDownItem()
    {
        ResetLocationOfItem();
        playerInteraction.RemoveItemFromInventory();
    }

    public override void ThrowItem()
    {
        ResetLocationOfItem();
    }

    private void ResetLocationOfItem()
    {
        Vector3 currentPos = gameObject.transform.position;
        gameObject.transform.SetParent(initialPosition.transform);
        gameObject.transform.position = currentPos - new Vector3(0, 1f, 0);
    }

    public override bool CheckPickUpItem()
    {
        return true;
    }
    public override void PickUpItem()
    {
        playerInteraction.SetCurrentItemInInventory(gameObject.GetComponent<Item>());
    }

    public override void EatItem()
    {
        UseTheHoney();
    }

    public override void UseItem()
    {
        Item i = playerInteraction.GetCurrentItemInRange();
        if (i != null)
        {
            if (i.CompareTag("FatBird") && !diedOfBird)
            {
                diedOfBird = true;
                ResetHoney();
                deathManager.SetDiedOfBird();
            }
            if (!killDeath && i.CompareTag("Death"))
            {
                killDeath = true;
                canvasesController.UpdateCanvasToDisplay(killDeathWithHoney);
            }
        }

    }

    void UseTheHoney()
    {
        timesEatenHoney++;
        if (timesEatenHoney >= timesToDieFromEatingHoney)
        {
            deathManager.SetDiedOfHoney();
        }
        else if (timesEatenHoney < timesToDieFromEatingHoney)
        {
            playerSprite.color = colorWhenEating;
            Invoke("ChangeColorOfPlayerBack", 2f);
        }
        ResetHoney();
    }


    public void ChangeColorOfPlayerBack()
    {
        playerSprite.color = Color.white;
    }
    public void ResetHoney()
    {
        beesToSpawn.gameObject.SetActive(true);
        gameObject.transform.SetParent(initialPosition.transform);
        gameObject.transform.position = initialPosition.transform.position;
        gameObject.SetActive(false);
    }
}

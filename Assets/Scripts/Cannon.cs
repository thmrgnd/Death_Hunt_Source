using UnityEngine;

public class Cannon : Item
{
    [SerializeField] GameObject initialPosition;
    [SerializeField] Color32 colorUpgraded;
    [SerializeField] GameObject timeTravelSpawn;
    [SerializeField] Canvas[] timeTravelCanvases;
    [SerializeField] Canvas[] finishGameCanvases;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioClip upgradeSound;
    [SerializeField] AudioClip timeTravelSound;
    [SerializeField] float shootSoundVolume;
    [SerializeField] float upgradeSoundVolume;
    [SerializeField] float timeTravelSoundVolume;
    [SerializeField] Canvas triedToKillDeath;
    [SerializeField] Canvas triedToKillDeathUpgraded;

    bool diedOfCannon;
    DeathManager deathManager;
    CanvasesController canvasesController;
    PlayerInteraction playerInteraction;
    bool isUpgraded = false;
    bool alienSpawned;
    bool canTimeTravel;
    bool wasWithHusband;
    bool timeTraveledOnce;
    bool finishedGame;
    bool shotDeath;
    bool shotDeathUpgraded;
    bool fellToInfinite;
    bool summonAlien;
    bool killedAlien;
    void Start()
    {
        playerInteraction = FindObjectOfType<PlayerInteraction>();
        deathManager = FindObjectOfType<DeathManager>();
        canvasesController = FindObjectOfType<CanvasesController>();
    }

    void Update()
    {

    }

    public void UpgradeCannon()
    {
        audioSource.PlayOneShot(upgradeSound, upgradeSoundVolume);
        isUpgraded = true;
        GetComponentInChildren<SpriteRenderer>().color = colorUpgraded;
    }

    public override void UseItem()
    {
        if (canTimeTravel && !timeTraveledOnce && playerInteraction.GetCurrentItemInInventory().CompareTag("Cannon"))
        {
            timeTraveledOnce = true;
            audioSource.PlayOneShot(timeTravelSound, timeTravelSoundVolume);
            playerInteraction.gameObject.GetComponent<PlayerRespawn>().ResetPlayerLocation(timeTravelSpawn);
            playerInteraction.gameObject.GetComponent<PlayerMovement>().SetCanMove();
            FindObjectOfType<ThemeMusicManager>().StopMusic();
            Invoke("TimeTravel", 5f);
            return;
        }

        Item inRange = playerInteraction.GetCurrentItemInRange();
        if (inRange != null)
        {
            if (inRange.CompareTag("Death"))
            {
                if (isUpgraded)
                {
                    if (shotDeathUpgraded)
                        return;
                    shotDeathUpgraded = true;
                    canvasesController.UpdateCanvasToDisplay(triedToKillDeathUpgraded);
                }
                else
                {
                    if (shotDeath)
                        return;
                    shotDeath = true;
                    canvasesController.UpdateCanvasToDisplay(triedToKillDeath);
                }
                return;
            }
            if (inRange.CompareTag("FatBird"))
            {
                if (!deathManager.GetDiedFromBird())
                {
                    deathManager.SetDiedOfBird();
                    return;
                }
                audioSource.PlayOneShot(shootSound, shootSoundVolume);
                inRange.GetComponent<FatBird>().KillTheBird();
                return;
            }

            if (inRange.CompareTag("InfiniteFall"))
            {
                if (fellToInfinite)
                    return;
                fellToInfinite = true;
                audioSource.PlayOneShot(shootSound, shootSoundVolume);
                playerInteraction.gameObject.GetComponent<PlayerMovement>().InfiniteFall();
                return;
            }

            if (wasWithHusband && isUpgraded && alienSpawned && inRange.CompareTag("Summon Alien"))
            {
                if (killedAlien)
                    return;
                killedAlien = true;
                audioSource.PlayOneShot(shootSound, shootSoundVolume);
                inRange.gameObject.GetComponent<SummonAlien>().KillTheAlien();
                canTimeTravel = true;
                return;
            }

            if (!alienSpawned && inRange.CompareTag("Summon Alien"))
            {
                if (summonAlien)
                    return;
                summonAlien = true;
                audioSource.PlayOneShot(shootSound, shootSoundVolume);
                inRange.gameObject.GetComponent<SummonAlien>().SummonTheAlien();
                alienSpawned = true;
                return;
            }
            //only if was with husband - so you can't time travel without seeing the bad ending

            if (inRange.CompareTag("PlayerYoung"))
            {
                if (finishedGame)
                    return;
                finishedGame = true;
                audioSource.PlayOneShot(shootSound, shootSoundVolume);
                FinishGame(); //!!!
                return;
            }
        }




    }

    public void SetWasWithHusband()
    {
        wasWithHusband = true;
    }

    private void FinishGame()
    {
        FindObjectOfType<ThemeMusicManager>().PlayCreditsTheme();
        foreach (Canvas c in finishGameCanvases)
            canvasesController.UpdateCanvasToDisplay(c);
    }

    private void TimeTravel()
    {
        FindObjectOfType<ThemeMusicManager>().PlayHeartbeatTheme();
        playerInteraction.gameObject.GetComponent<PlayerMovement>().SetCanMove();
        foreach (Canvas c in timeTravelCanvases)
            canvasesController.UpdateCanvasToDisplay(c);
    }

    public override void EatItem()
    {
        if (diedOfCannon)
            return;
        audioSource.PlayOneShot(shootSound, shootSoundVolume);
        diedOfCannon = true;
        deathManager.SetDiedOfCannon();
    }

    public override bool CheckPickUpItem()
    {
        return true;
    }
    public override void PickUpItem()
    {
        playerInteraction.SetCurrentItemInInventory(gameObject.GetComponent<Item>());
    }
    public override void PutDownItem()
    {
        ResetLocationOfItem();
        playerInteraction.RemoveItemFromInventory();
    }

    public override void ThrowItem() // when you pick up a new item instead
    {
        ResetLocationOfItem();
    }

    private void ResetLocationOfItem()
    {
        Vector3 currentPos = gameObject.transform.position;
        gameObject.transform.SetParent(initialPosition.transform);
        gameObject.transform.position = currentPos - new Vector3(0, 1f, 0);
    }
}

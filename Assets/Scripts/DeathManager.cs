using TMPro;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [Header("Texts To Display Cause Of Death")]
    [SerializeField] string textForBeeDeath;
    [SerializeField] string textForHoneyDeath;
    [SerializeField] string textForBirdDeath;
    [SerializeField] string textForFallDeath;
    [SerializeField] string textForFallToInfinityDeath;
    [SerializeField] string textForNoOxygenDeath;
    [SerializeField] string textForTooCold;
    [SerializeField] string textForOuterSpace;
    [SerializeField] string textForIdle;
    [SerializeField] string textForCannon;
    [SerializeField] string textForMushroom;
    [SerializeField] string textForAlien;
    [SerializeField] string textForSaw;
    [SerializeField] string textForEatSaw;
    [SerializeField] string textForCreator;
    [SerializeField] string textForCreator2;
    [SerializeField] string textForBomb;
    [SerializeField] string textForFire;
    [SerializeField] string textForFireEating;
    [SerializeField] string textForSinkingSand;
    [SerializeField] string textForSpikes;

    [Header("Progression")]
    [SerializeField] int deathsToGetDoubleJump;
    [SerializeField] int deathsToGetCannon;
    [SerializeField] int deathsToFindHusband;
    [SerializeField] int deathsToGetUpgradedCannon;
    [SerializeField] Canvas youUnlockedSomethingCanvas;

    [Header("Sound")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip addedDeathSound;
    [SerializeField] float addedDeathSoundVolume;
    [SerializeField] AudioClip generalDeathSound; // General Death. nice name.
    [SerializeField] float generalDeathSoundVolume;
    [SerializeField] AudioClip sawDeathSound;
    [SerializeField] float sawDeathSoundVolume;
    [SerializeField] AudioClip alienDeathSound;
    [SerializeField] float alienDeathSoundVolume;
    [SerializeField] AudioClip birdDeathSound;
    [SerializeField] float birdDeathSoundVolume;
    [SerializeField] AudioClip generalDeathSquelchSound;
    [SerializeField] float generalDeathSquelchSoundVolume;
    [SerializeField] AudioClip fireDeathSound;
    [SerializeField] float fireDeathSoundVolume;
    [SerializeField] AudioClip beeDeathSound;
    [SerializeField] float beeDeathSoundVolume;
    [SerializeField] AudioClip honeyDeathSound;
    [SerializeField] float honeyDeathSoundVolume;
    [SerializeField] AudioClip idleDeathSound;
    [SerializeField] float idleDeathSoundVolume;

    [Header("Misc")]
    [SerializeField] TextMeshProUGUI deathsCounterText;
    [SerializeField] DeathCanvasController deathCanvasController;
    [SerializeField] GameObject boxesToDoubleJump;
    [SerializeField] GameObject boxesToCannon;
    [SerializeField] GameObject boxesToReachHusband;
    [SerializeField] GameObject boxesToUpgradeCannon;
    [SerializeField] GameObject locationToRespawnAfterDoubleJump;
    [SerializeField] GameObject locationToRespawnAfterCannon;
    [SerializeField] GameObject locationToRespawnAfterFindingHusband;
    [SerializeField] GameObject locationToRespawnAfterUpgradeCannonUnlocked;


    PlayerInteraction playerInteraction;
    PlayerRespawn playerRespawn;
    CanvasesController canvasesController;

    //progression
    bool hasDoubleJump;
    bool hasCannon;
    bool hasFoundHusband;
    bool hasCannonUpgraded;

    int totalDeaths;

    bool diedOfBee;
    bool diedOfHoney;
    bool diedOfBird;
    bool diedOfFall;
    bool diedOfInfinityFall;
    bool diedOfNoOxygen;
    bool diedOfTooCold;
    bool diedOfOuterSpace;
    bool diedOfIdle;
    bool diedOfCannon;
    bool diedOfMushroom;
    bool diedOfAlien;
    bool diedOfSaw;
    bool diedOfEatSaw;
    bool diedOfCreator;
    bool diedOfCreator2;
    bool diedOfBomb;
    bool diedOfFire;
    bool diedOfFireEating;
    bool diedOfSinkingSand;
    bool diedOfSpikes;

    void Start()
    {
        playerInteraction = FindObjectOfType<PlayerInteraction>();
        playerRespawn = FindObjectOfType<PlayerRespawn>();
        canvasesController = FindObjectOfType<CanvasesController>();
    }

    void Update()
    {

    }
    public bool HasDiedFromCreator()
    {
        return diedOfCreator;
    }

    public bool GetDiedFromBird()
    {
        return diedOfBird;
    }


    #region
    public void SetDiedOfIdle()
    {
        if (diedOfIdle)
            return;
        audioSource.PlayOneShot(idleDeathSound, idleDeathSoundVolume);
        diedOfIdle = true;
        deathCanvasController.FoundDeath(textForIdle);
        HandlePlayerDeath();
    }

    public void SetDiedOfBee()
    {
        if (diedOfBee)
            return;
        audioSource.PlayOneShot(beeDeathSound, beeDeathSoundVolume);
        diedOfBee = true;
        deathCanvasController.FoundDeath(textForBeeDeath);
        HandlePlayerDeath();
    }

    public void SetDiedOfFall()
    {
        if (diedOfFall)
            return;
        audioSource.PlayOneShot(generalDeathSound, generalDeathSoundVolume);
        diedOfFall = true;
        deathCanvasController.FoundDeath(textForFallDeath);
        HandlePlayerDeath();
    }
    public bool SetDiedOfInfiniteFall()
    {
        if (diedOfInfinityFall)
            return true;
        diedOfInfinityFall = true;
        Invoke("DiedOfInfinity", 3f);
        return false;
    }

    private void DiedOfInfinity()
    {
        audioSource.PlayOneShot(generalDeathSound, generalDeathSoundVolume);
        deathCanvasController.FoundDeath(textForFallToInfinityDeath);
        HandlePlayerDeath();
    }
    public void SetDiedOfHoney()
    {
        if (diedOfHoney)
            return;
        audioSource.PlayOneShot(honeyDeathSound, honeyDeathSoundVolume);
        diedOfHoney = true;
        deathCanvasController.FoundDeath(textForHoneyDeath);
        HandlePlayerDeath();
    }

    public void SetDiedOfBird()
    {
        if (diedOfBird)
            return;
        audioSource.PlayOneShot(birdDeathSound, birdDeathSoundVolume);
        diedOfBird = true;
        deathCanvasController.FoundDeath(textForBirdDeath);
        HandlePlayerDeath();
    }

    public void SetDiedOfNoOxygen()
    {
        if (diedOfNoOxygen)
            return;
        audioSource.PlayOneShot(generalDeathSound, generalDeathSoundVolume);
        diedOfNoOxygen = true;
        deathCanvasController.FoundDeath(textForNoOxygenDeath);
        HandlePlayerDeath();
    }
    public void SetDiedOfTooCold()
    {
        if (diedOfTooCold)
            return;
        audioSource.PlayOneShot(generalDeathSound, generalDeathSoundVolume);
        diedOfTooCold = true;
        deathCanvasController.FoundDeath(textForTooCold);
        HandlePlayerDeath();
    }
    public void SetDiedOfOuterSpace()
    {
        if (diedOfOuterSpace)
            return;
        audioSource.PlayOneShot(generalDeathSquelchSound, generalDeathSquelchSoundVolume);
        diedOfOuterSpace = true;
        deathCanvasController.FoundDeath(textForOuterSpace);
        HandlePlayerDeath();
    }
    public void SetDiedOfCannon() // no need for sound - cannon does shotgun
    {
        if (diedOfCannon)
            return;
        diedOfCannon = true;
        deathCanvasController.FoundDeath(textForCannon);
        HandlePlayerDeath();
    }
    public void SetDiedOfMushroom()
    {
        if (diedOfMushroom)
            return;
        audioSource.PlayOneShot(honeyDeathSound, honeyDeathSoundVolume);
        diedOfMushroom = true;
        deathCanvasController.FoundDeath(textForMushroom);
        HandlePlayerDeath();
    }
    public void SetDiedOfAlien()
    {
        if (diedOfAlien)
            return;
        audioSource.PlayOneShot(alienDeathSound, alienDeathSoundVolume);
        diedOfAlien = true;
        deathCanvasController.FoundDeath(textForAlien);
        HandlePlayerDeath();
    }
    public void SetDiedOfSaw()
    {
        if (diedOfSaw)
            return;
        audioSource.PlayOneShot(sawDeathSound, sawDeathSoundVolume);
        diedOfSaw = true;
        deathCanvasController.FoundDeath(textForSaw);
        HandlePlayerDeath();
    }
    public void SetDiedOfEatSaw()
    {
        if (diedOfEatSaw)
            return;
        audioSource.PlayOneShot(sawDeathSound, sawDeathSoundVolume);
        diedOfEatSaw = true;
        deathCanvasController.FoundDeath(textForEatSaw);
        HandlePlayerDeath();
    }
    public void SetDiedOfCreator()
    {
        if (diedOfCreator)
            return;
        audioSource.PlayOneShot(generalDeathSound, generalDeathSoundVolume);
        diedOfCreator = true;
        deathCanvasController.FoundDeath(textForCreator);
        HandlePlayerDeath();
    }
    public void SetDiedOfBomb() // sound is in the bomb use
    {
        if (diedOfBomb)
            return;
        diedOfBomb = true;
        deathCanvasController.FoundDeath(textForBomb);
        HandlePlayerDeath();
    }
    public void SetDiedOfFire()
    {
        if (diedOfFire)
            return;
        diedOfFire = true;
        audioSource.PlayOneShot(fireDeathSound, fireDeathSoundVolume);
        deathCanvasController.FoundDeath(textForFire);
        HandlePlayerDeath();
    }
    public void SetDiedOfFireEating()
    {
        if (diedOfFireEating)
            return;
        audioSource.PlayOneShot(fireDeathSound, fireDeathSoundVolume);
        diedOfFireEating = true;
        deathCanvasController.FoundDeath(textForFireEating);
        HandlePlayerDeath();
    }
    public void SetDiedOfSinkingSand()
    {
        if (diedOfSinkingSand)
            return;
        audioSource.PlayOneShot(generalDeathSound, generalDeathSoundVolume);
        diedOfSinkingSand = true;
        deathCanvasController.FoundDeath(textForSinkingSand);
        HandlePlayerDeath();
    }
    public void SetDiedOfSpikes()
    {
        if (diedOfSpikes)
            return;
        audioSource.PlayOneShot(generalDeathSquelchSound, generalDeathSquelchSoundVolume);
        diedOfSpikes = true;
        deathCanvasController.FoundDeath(textForSpikes);
        HandlePlayerDeath();
    }
    public void SetDiedOfCreator2()
    {
        diedOfCreator2 = true;
        deathCanvasController.FoundDeath(textForCreator2);
        totalDeaths--;
        HandlePlayerDeath();
    }

    #endregion
    private void HandlePlayerDeath()
    {
        totalDeaths++;
        if (totalDeaths >= deathsToGetDoubleJump && !hasDoubleJump)
        {
            canvasesController.UpdateCanvasToDisplay(youUnlockedSomethingCanvas);
            hasDoubleJump = true;
            boxesToDoubleJump.SetActive(false);
            playerRespawn.UpdateRespawnPoint(locationToRespawnAfterDoubleJump.transform.position);
        }
        if (totalDeaths >= deathsToGetCannon && !hasCannon)
        {
            canvasesController.UpdateCanvasToDisplay(youUnlockedSomethingCanvas);
            hasCannon = true;
            boxesToCannon.SetActive(false);
            playerRespawn.UpdateRespawnPoint(locationToRespawnAfterCannon.transform.position);
        }
        if (totalDeaths >= deathsToFindHusband && !hasFoundHusband)
        {
            canvasesController.UpdateCanvasToDisplay(youUnlockedSomethingCanvas);
            hasFoundHusband = true;
            boxesToReachHusband.SetActive(false);
            playerRespawn.UpdateRespawnPoint(locationToRespawnAfterFindingHusband.transform.position);
        }
        if (FindObjectOfType<Husband>().GetFoundHusband() &&
            totalDeaths >= deathsToGetUpgradedCannon && !hasCannonUpgraded)
        {
            canvasesController.UpdateCanvasToDisplay(youUnlockedSomethingCanvas);
            hasCannonUpgraded = true;
            boxesToUpgradeCannon.SetActive(false);
            playerRespawn.UpdateRespawnPoint(locationToRespawnAfterUpgradeCannonUnlocked.transform.position);
        }

        playerInteraction.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        playerInteraction.PlayerDeath();
        playerRespawn.ResetPlayerLocation();
        Invoke("AddDeathDelay", 0.5f);
    }

    public void AddDeathDelay()
    {
        audioSource.PlayOneShot(addedDeathSound, addedDeathSoundVolume);
        deathsCounterText.text = totalDeaths.ToString();
    }

    public void CheckForUpgradeCannon()
    {
        if (totalDeaths >= deathsToGetUpgradedCannon && !hasCannonUpgraded)
        {
            hasCannonUpgraded = true;
            boxesToUpgradeCannon.SetActive(false);
        }
    }
}

using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    [SerializeField] float secondsToDieInHere;
    [SerializeField] bool tooCold;
    [SerializeField] bool noOxygen;
    [SerializeField] bool outerSpace;
    [SerializeField] bool creator;
    [SerializeField] bool creator2;
    [SerializeField] bool sinkingSand;
    [SerializeField] bool spikes;
    [SerializeField] bool saw;
    [SerializeField] bool fire;
    [SerializeField] GameObject tilemapSinkingSand;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip revealCaveSound;
    [SerializeField] float revealCaveSoundVolume;

    float timer;
    bool startTimer;
    DeathManager deathManager;
    Fire fireComponent;
    bool diedOfCreator;
    bool diedOfTooCold;
    bool diedOfNoOxygen;
    bool diedOfOuterSpace;
    bool diedOfSinkingSand;
    bool diedOfSpikes;
    bool isHiddenCaveActive;
    bool diedBySaw;
    bool diedByFire;

    void Start()
    {
        deathManager = FindObjectOfType<DeathManager>();
        fireComponent = FindObjectOfType<Fire>();
    }

    void Update()
    {
        if (startTimer)
            timer += Time.deltaTime;
        if (timer >= secondsToDieInHere)
        {
            if (creator && !deathManager.HasDiedFromCreator())
            {
                deathManager.SetDiedOfCreator();
                ResetTimer();
            }
            if (creator2 && deathManager.HasDiedFromCreator())
            {
                deathManager.SetDiedOfCreator2();
                ResetTimer();
            }
            if (sinkingSand && !diedOfSinkingSand)
            {
                diedOfSinkingSand = true;
                deathManager.SetDiedOfSinkingSand();
                ResetTimer();
            }
        }
    }

    public void RevealHiddenCave()
    {
        if (isHiddenCaveActive)
            return;
        isHiddenCaveActive = true;
        tilemapSinkingSand.SetActive(false);
        audioSource.PlayOneShot(revealCaveSound, revealCaveSoundVolume);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            if (!diedOfSpikes && spikes)
            {
                diedOfSpikes = true;
                deathManager.SetDiedOfSpikes();
            }
            if (diedOfSinkingSand)
            {
                RevealHiddenCave();
            }

            if(saw)
            {
                if (diedBySaw)
                    return;
                diedBySaw = true;
                deathManager.SetDiedOfSaw();
            }

            if (fire)
            {
                if (diedByFire)
                    return;
                diedByFire = true;
                fireComponent.SetDiedOfFire();
                deathManager.SetDiedOfFire();
            }
            if(tooCold)
            {
                if (diedOfTooCold)
                    return;
                diedOfTooCold = true;
                deathManager.SetDiedOfTooCold();
            }
            if (noOxygen)
            {
                if (diedOfNoOxygen)
                    return;
                diedOfNoOxygen = true;
                deathManager.SetDiedOfNoOxygen();
            }
            if (outerSpace)
            {
                if (diedOfOuterSpace)
                    return;
                diedOfOuterSpace = true;
                deathManager.SetDiedOfOuterSpace();
            }

            startTimer = true;
            timer = 0;
            if (sinkingSand && !diedOfSinkingSand)
                collision.gameObject.GetComponent<PlayerMovement>().EnteredSinkingSand();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ResetTimer();
            if (sinkingSand)
                collision.gameObject.GetComponent<PlayerMovement>().ExitSinkingSand();

        }
    }

    private void ResetTimer()
    {
        startTimer = false;
        timer = 0;
    }
}

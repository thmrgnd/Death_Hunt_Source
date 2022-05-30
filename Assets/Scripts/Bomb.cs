using UnityEngine;

public class Bomb : Item
{
    [SerializeField] GameObject initialPosition;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip bombDeathSound;
    [SerializeField] float bombDeathSoundVolume;
    [SerializeField] Canvas triedToKillDeath;
    PlayerInteraction playerInteraction;
    DeathManager deathManager;
    CanvasesController canvasesController;
    bool diedOfBomb;
    bool fellInfinite;
    bool killDeath;
    bool killFatBird;
    void Start()
    {
        canvasesController = FindObjectOfType<CanvasesController>();
        deathManager = FindObjectOfType<DeathManager>();
        playerInteraction = FindObjectOfType<PlayerInteraction>();
    }

    void Update()
    {

    }

    public override void UseItem()
    {
        if (diedOfBomb)
        {
            Item inRange = playerInteraction.GetCurrentItemInRange();
            if (inRange != null)
            {
                if (inRange.CompareTag("Death"))
                {
                    if (killDeath)
                        return;
                    killDeath = true;
                    audioSource.PlayOneShot(bombDeathSound, bombDeathSoundVolume);
                    canvasesController.UpdateCanvasToDisplay(triedToKillDeath);
                    return;
                }
                else if (inRange.CompareTag("InfiniteFall"))
                {
                    if (fellInfinite)
                        return;
                    fellInfinite = true;
                    audioSource.PlayOneShot(bombDeathSound, bombDeathSoundVolume);
                    playerInteraction.gameObject.GetComponent<PlayerMovement>().InfiniteFall();
                }
                else if (inRange.CompareTag("FatBird"))
                {
                    if (killFatBird)
                        return;
                    killFatBird = true;
                    audioSource.PlayOneShot(bombDeathSound, bombDeathSoundVolume);
                    inRange.GetComponent<FatBird>().KillTheBird();
                    return;
                }
            }
        }

        else
        {
            audioSource.PlayOneShot(bombDeathSound, bombDeathSoundVolume);
            diedOfBomb = true;
            deathManager.SetDiedOfBomb();
        }
    }

    public override void PickUpItem()
    {
        playerInteraction.SetCurrentItemInInventory(gameObject.GetComponent<Item>());
    }

    public override bool CheckPickUpItem()
    {
        return true;
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
}

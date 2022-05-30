using UnityEngine;

public class Husband : MonoBehaviour
{
    [SerializeField] Canvas[] husbandDeathCanvases;
    [SerializeField] Cannon cannon;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip foundHusbandSound;
    [SerializeField] float foundHusbandSoundVolume;
    [SerializeField] GameObject Death;
    PlayerMovement player;
    DeathManager deathManager;
    CanvasesController canvasesController;
    bool foundHusband;
    void Start()
    {
        deathManager = FindObjectOfType<DeathManager>();
        canvasesController = FindObjectOfType<CanvasesController>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!foundHusband && collision.CompareTag("Player"))
        {
            deathManager.CheckForUpgradeCannon();
            foundHusband = true;
            player = collision.gameObject.GetComponent<PlayerMovement>();
            player.SetCanMove();
            cannon.SetWasWithHusband();
            audioSource.PlayOneShot(foundHusbandSound,foundHusbandSoundVolume);
            FindObjectOfType<ThemeMusicManager>().StopMusic();
            Invoke("ExplainDeath", 4f);
        }
    }


    public bool GetFoundHusband()
    {
        return foundHusband;
    }

    public void ExplainDeath()
    {
        FindObjectOfType<ThemeMusicManager>().PlayEndgameTheme();
        player.SetCanMove();
        Death.SetActive(true);
        foreach (Canvas c in husbandDeathCanvases)
            canvasesController.UpdateCanvasToDisplay(c);
    }
}

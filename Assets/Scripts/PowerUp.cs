using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] Canvas doubleJumpTutorialCanvas;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip SFX;
    [SerializeField] float SFXVolume;
    PlayerMovement playerMovement;
    bool isActivated = false;
    CanvasesController canvasesController;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        canvasesController = FindObjectOfType<CanvasesController>();
    }

    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActivated)
            return;
        isActivated = true;
        canvasesController.UpdateCanvasToDisplay(doubleJumpTutorialCanvas);
        playerMovement.UnlockDoubleJump();
        audioSource.PlayOneShot(SFX, SFXVolume);
        gameObject.transform.GetChild(0).gameObject.SetActive(false); // make the sprite disappear
        Destroy(gameObject, 2f);
    }
}

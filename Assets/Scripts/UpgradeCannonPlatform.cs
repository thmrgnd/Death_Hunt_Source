using UnityEngine;

public class UpgradeCannonPlatform : MonoBehaviour
{
    [SerializeField] Canvas upgradeCannonCanvas;
    bool cannonUpgraded = false;
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!cannonUpgraded && collision.CompareTag("Player"))
        {
            FindObjectOfType<Cannon>().UpgradeCannon();
            cannonUpgraded = true;
            FindObjectOfType<CanvasesController>().UpdateCanvasToDisplay(upgradeCannonCanvas);
            gameObject.transform.GetChild(0).gameObject.SetActive(false); // make the sprite disappear
        }
    }
}

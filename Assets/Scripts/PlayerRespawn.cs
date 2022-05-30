using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] GameObject initialRespawn;
    Vector3 respawnPoint;
    void Start()
    {
        UpdateRespawnPoint(initialRespawn.transform.position);
    }

    void Update()
    {

    }

    public void UpdateRespawnPoint(Vector3 respawnPoint)
    {
        this.respawnPoint = respawnPoint;
    }
    public void ResetPlayerLocation()
    {
        gameObject.transform.position = respawnPoint;
    }
    public void ResetPlayerLocation(GameObject respawn)
    {
        gameObject.transform.position = respawn.transform.position;
    }
}

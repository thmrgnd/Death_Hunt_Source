using UnityEngine;

public class DropItemCollider : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Item i = collision.gameObject.GetComponent<PlayerInteraction>().GetCurrentItemInInventory();
            if(i != null)
            {
                i.PutDownItem();
                i.transform.position = gameObject.transform.position;
            }
        }
    }
}

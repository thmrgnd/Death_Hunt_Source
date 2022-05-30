using UnityEngine;

public class Item : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {

    }

    public virtual bool CheckPickUpItem()
    {
        return false;
    }  
    public virtual void PickUpItem()
    {

    }
    public virtual void EatItem()
    {

    }
    public virtual void UseItem()
    {

    }
    public virtual void PutDownItem()
    {

    }
    public virtual void ThrowItem()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerInteraction>()
                .SetCurrentItemInRange(gameObject.GetComponent<Item>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerInteraction>()
                .SetCurrentItemInRangeToNull();
        }
    }
}

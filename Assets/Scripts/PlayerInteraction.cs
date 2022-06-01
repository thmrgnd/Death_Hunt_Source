using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip pickUpSound;
    [SerializeField] float pickUpSoundVolume;
    [SerializeField] AudioClip putDownSound;
    [SerializeField] float putDownSoundVolume;
    Item currentItemInRange;
    Item currentItemInInventory;
    Queue<Item> currentItemsInRange;

    const string HUD_BOOL = "Item In Range";


    void Start()
    {
        currentItemsInRange = new Queue<Item>();
    }

    void Update()
    {
        if (currentItemInInventory != null || currentItemInRange != null)
            animator.SetBool(HUD_BOOL, true);
        else
            animator.SetBool(HUD_BOOL, false);
    }

    public void PlayerDeath()
    {
        currentItemInRange = null;
        currentItemsInRange.Clear();
        if (currentItemInInventory != null)
        {
            if (currentItemInInventory.CompareTag("Honey"))
            {
                currentItemInInventory.GetComponent<Honey>().ResetHoney();
                RemoveItemFromInventory();
            }
            else
                currentItemInInventory.PutDownItem();
        }
    }

    public Item GetCurrentItemInInventory()
    {
        return currentItemInInventory;
    }
    public Item GetCurrentItemInRange()
    {
        return currentItemInRange;
    }
    public void SetCurrentItemInRange(Item item)
    {
        if (currentItemInRange == null)
        {
            currentItemInRange = item;
            Debug.Log("Added To Range");
        }
        else
        {
            currentItemsInRange.Enqueue(item);
            Debug.Log("Added an item to queue");
        }
    }
    public void SetCurrentItemInRangeToNull() // the item that was in range is now no longer here. up goes the next in range
    {
        Item i;
        if (currentItemsInRange.TryPeek(out i))
        {
            currentItemInRange = currentItemsInRange.Dequeue();
            Debug.Log("new item out of queue");
        }
        else
        {
            currentItemInRange = null;
            Debug.Log("No item in queue");
        }
    }

    public void RemoveItemFromInventory()
    {
        currentItemInInventory = null;
    }

    public void SetCurrentItemInInventory(Item item)
    {
        if (currentItemInInventory != null)
            currentItemInInventory.ThrowItem();
        if (item != null)
        {
            item.gameObject.transform.SetParent(gameObject.transform);
            item.gameObject.transform.localPosition = new Vector3(0, 0.7f, 0);
        }
        currentItemInInventory = item;
    }

    public void UseItem(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (currentItemInInventory != null)
                currentItemInInventory.UseItem();
            else if (currentItemInRange != null)
                currentItemInRange.UseItem();
        }
    }

    public void EatItem(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (currentItemInInventory != null)
                currentItemInInventory.EatItem();
            else if (currentItemInRange != null)
                currentItemInRange.EatItem();
        }
    }
    public void PutDownItem(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (currentItemInInventory != null)
            {
                currentItemInInventory.PutDownItem();
                audioSource.PlayOneShot(putDownSound, putDownSoundVolume);
            }
        }
    }
    public void PickUpItem(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            TryPickUpItem();
        }
    }

    private void TryPickUpItem() // all  this to pick ap a single item even if the current targeted is not pickable.
    {
        if (currentItemInRange != null)
        {
            while (!currentItemInRange.CheckPickUpItem())
            // cant pick up item. start cycling
            {
                Item i;
                if (!currentItemsInRange.TryPeek(out i)) // no next item
                    break;

                currentItemsInRange.Enqueue(currentItemInRange); // the current is going to the bottom of the list
                currentItemInRange = currentItemsInRange.Dequeue(); // hop goes the next to check if can pick up
            }
            if (currentItemInRange.CheckPickUpItem())
            {
                audioSource.PlayOneShot(pickUpSound, pickUpSoundVolume);
                currentItemInRange.PickUpItem();
            }

        }
    }
}

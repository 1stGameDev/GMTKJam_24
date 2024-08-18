using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private string currentlyHolding = null;
    [SerializeField] private SpriteRenderer handRenderer;

    [SerializeField]
    private float PickUpDelay = 0.05f;

    private PlayerThrowing ThrowingComp;

    private string ItemToPickUpAfterDelay = null;
    private float TimeSincePickUp = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        currentlyHolding = null;

        ThrowingComp = GetComponent<PlayerThrowing>();
    }

    private void Update()
    {
        // Make it so we don't pick up an item for a short delay so we don't pick up and eat on the same frame
        TimeSincePickUp += Time.deltaTime;
        if (TimeSincePickUp >= PickUpDelay)
        {
            if (ItemToPickUpAfterDelay != null)
            {
                currentlyHolding = ItemToPickUpAfterDelay;
                ItemToPickUpAfterDelay = null;
            }
        }
    }

    public bool PickUpItem(GameObject item, string obj, Sprite sprite)
    {
        if(currentlyHolding != null)
        {
            return false;
        }

        if (TimeSincePickUp < PickUpDelay)
        {
            return false;
        }

        TimeSincePickUp = 0.0f;

        ItemToPickUpAfterDelay = obj;
        handRenderer.sprite = sprite;

        if (ThrowingComp)
        {
            ThrowingComp.SetThrowableObject(item);
        }

        return true;
    }

    public void ConsumeItem()
    {
        currentlyHolding = null;
        ItemToPickUpAfterDelay = null;
        handRenderer.sprite = null;

        if (ThrowingComp)
        {
            ThrowingComp.RemoveThrowableObject();
        }
    }

    public string CheckInventory(){
        return currentlyHolding;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private string currentlyHolding;
    [SerializeField] private SpriteRenderer handRenderer;

    private PlayerThrowing ThrowingComp;

    // Start is called before the first frame update
    void Start()
    {
        currentlyHolding = null;

        ThrowingComp = GetComponent<PlayerThrowing>();
    }

    public bool PickUpItem(GameObject item, string obj, Sprite sprite)
    {
        if(currentlyHolding != null)
        {
            return false;
        }

        currentlyHolding = obj;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private string currentlyHolding;
    [SerializeField] private SpriteRenderer handRenderer;
    // Start is called before the first frame update
    void Start()
    {
        currentlyHolding = null;
    }

    public bool PickUpItem(string obj, Sprite sprite){
        if(currentlyHolding != null){
            return false;
        }
        currentlyHolding = obj;
        handRenderer.sprite = sprite;
        return true;
    }

    public void ConsumeItem(){
        currentlyHolding = null;
        handRenderer.sprite = null;
    }

    public string CheckInventory(){
        return currentlyHolding;
    }
}

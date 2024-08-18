using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField]
    private string InventoryName;

    [SerializeField]
    private Pickable PickableComponent;

    private bool HasBeenThrown = false;

    public Pickable GetPickableComponent()
    {
        return PickableComponent;
    }

    public string GetInventoryName()
    {
        return InventoryName;
    }

    public void SetMushroomThrown()
    {
        HasBeenThrown = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!HasBeenThrown || other == null)
        {
            return;
        }

        GrowScript growable = other.gameObject.GetComponent<GrowScript>();
        if (growable)
        {
            bool used = false;
            if (InventoryName == "grow")
            {
                used = growable.Grow();
            }
            else if (InventoryName == "shrink")
            {
                used = growable.Shrink();
            }
            gameObject.SetActive(false);
        }
    }
}

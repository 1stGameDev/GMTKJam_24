using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField]
    private Pickable PickableComponent;

    public Pickable GetPickableComponent()
    {
        return PickableComponent;
    }
}

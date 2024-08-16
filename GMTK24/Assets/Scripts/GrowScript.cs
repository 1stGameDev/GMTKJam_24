using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowScript : MonoBehaviour
{

public Transform playerTransform;

    void Start()
    {
        Grow();
    }



    public void Grow()
    {

        playerTransform.localScale = new Vector3(playerTransform.localScale.x + 1, playerTransform.localScale.y + 1, playerTransform.localScale.z + 1);

        
    }
}

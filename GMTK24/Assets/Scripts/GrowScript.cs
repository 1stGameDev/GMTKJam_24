using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrowScript : MonoBehaviour
{

private Transform playerTransform;

    void Start()
    {
        playerTransform = GetComponent<Transform>();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.G)){
            Grow();
        }
        if(Input.GetKeyDown(KeyCode.F)){
            Shrink();
        }
    }



    private bool Grow()
    {
        if(playerTransform.localScale.y > 1.5){
            return false;
        }
        playerTransform.localScale = new Vector3(playerTransform.localScale.x * 1.5f, playerTransform.localScale.y * 1.5f, playerTransform.localScale.z);
        return true;
    }

    private bool Shrink(){
        if(playerTransform.localScale.y < 0.6){
            return false;
        }
        playerTransform.localScale = new Vector3(playerTransform.localScale.x / 1.5f, playerTransform.localScale.y / 1.5f, playerTransform.localScale.z);
        return true;
    }
}

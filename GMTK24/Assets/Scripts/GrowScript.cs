using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GrowScript : MonoBehaviour
{

private Transform playerTransform;
private Rigidbody2D rb;
private CharacterController2D characterController2D;

    void Start()
    {
        playerTransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        characterController2D = GetComponent<CharacterController2D>();
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
        rb.mass *= 2.5f;
        characterController2D.m_JumpForce *= 2.5f;
        return true;
    }

    private bool Shrink(){
        if(playerTransform.localScale.y < 0.6){
            return false;
        }
        playerTransform.localScale = new Vector3(playerTransform.localScale.x / 1.5f, playerTransform.localScale.y / 1.5f, playerTransform.localScale.z);
        rb.mass /= 2.5f;
        characterController2D.m_JumpForce /= 2.5f;
        return true;
    }
}

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
private Inventory inventory;
[SerializeField] private ParticleSystem growParicles;
[SerializeField] private ParticleSystem shrinkParticles;
[SerializeField] private ParticleSystem normalParticles;
private ParticleSystem currentParticles;

    void Start()
    {
        playerTransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        characterController2D = GetComponent<CharacterController2D>();
        inventory = GetComponent<Inventory>();
    }

    void Update(){

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inventory)
            {
                string currentlyHolding = inventory.CheckInventory();
                if (currentlyHolding == "grow")
                {
                    Grow();
                }
                else if (currentlyHolding == "shrink")
                {
                    Shrink();
                }
                else if(currentlyHolding == "normal"){
                    Normal();
                }
            }
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
        inventory.ConsumeItem();
        currentParticles = growParicles;
        currentParticles.Play();
        StartCoroutine(StopParticlesAfterUse());
        return true;
    }

    private bool Shrink(){
        if(playerTransform.localScale.y < 0.6){
            return false;
        }
        playerTransform.localScale = new Vector3(playerTransform.localScale.x / 1.5f, playerTransform.localScale.y / 1.5f, playerTransform.localScale.z);
        rb.mass /= 2.5f;
        characterController2D.m_JumpForce /= 2.5f;
        inventory.ConsumeItem();
        currentParticles = shrinkParticles;
        currentParticles.Play();
        StartCoroutine(StopParticlesAfterUse());
        return true;
    }

    private bool Normal(){
        playerTransform.localScale = new Vector3(1f, 1f, 1f);
        rb.mass = 1f;
        characterController2D.m_JumpForce = 700f;
        inventory.ConsumeItem();
        currentParticles = normalParticles;
        currentParticles.Play();
        StartCoroutine(StopParticlesAfterUse());
        return true;
    }
    private IEnumerator StopParticlesAfterUse()
{
    yield return new WaitForSeconds(0.5f); // Adjust the duration as needed
    currentParticles.Stop();
}
}

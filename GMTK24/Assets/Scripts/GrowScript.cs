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
    private PlayerThrowing playerThrowing;

    private int CurrentSize = 0;

    [SerializeField]
    private int MinSize = -2;

    [SerializeField]
    private int MaxSize = 2;

    void Start()
    {
        playerTransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        characterController2D = GetComponent<CharacterController2D>();
        inventory = GetComponent<Inventory>();
        playerThrowing = GetComponent<PlayerThrowing>();
    }

    void Update()
    {
        if (inventory)
        {
            if (Input.GetKeyDown(KeyCode.E))
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

    public bool Grow()
    {
        if (CurrentSize >= MaxSize)
        {
            return false;
        }

        CurrentSize++;

        playerTransform.localScale = new Vector3(playerTransform.localScale.x * 1.5f, playerTransform.localScale.y * 1.5f, playerTransform.localScale.z);
        rb.mass *= 2.5f;

        if (characterController2D)
        {
            characterController2D.m_JumpForce *= 2.5f;
        }
        
        if (inventory)
        {
            inventory.ConsumeItem();
        }
        
        currentParticles = growParicles;
        if (currentParticles)
        {
            currentParticles.Play();
            StartCoroutine(StopParticlesAfterUse());
        }

        if (playerThrowing)
        {
            playerThrowing.MultiplyThrowMultiplier(2.5f);
        }

        return true;
    }

    public bool Shrink(){
        if (CurrentSize <= MinSize)
        {
            return false;
        }

        CurrentSize--;

        playerTransform.localScale = new Vector3(playerTransform.localScale.x / 1.5f, playerTransform.localScale.y / 1.5f, playerTransform.localScale.z);
        rb.mass /= 2.5f;

        if (characterController2D)
        {
            characterController2D.m_JumpForce /= 2.5f;
        }
        
        if (inventory)
        {
            inventory.ConsumeItem();
        }
        
        currentParticles = shrinkParticles;
        if (currentParticles)
        {
            currentParticles.Play();
            StartCoroutine(StopParticlesAfterUse());
        }

        if (playerThrowing)
        {
            playerThrowing.MultiplyThrowMultiplier(1 / 2.5f);
        }

        return true;
    }

    private bool Normal(){
        if(CurrentSize == 0){
            return false;
        }
        if(CurrentSize <= -2){
            playerTransform.localPosition = new Vector3(playerTransform.localPosition.x, playerTransform.localPosition.y + 0.5f, playerTransform.localPosition.z);
        }

        CurrentSize = 0;

        playerTransform.localScale = new Vector3(playerTransform.localScale.x / Mathf.Abs(playerTransform.localScale.x), playerTransform.localScale.y / playerTransform.localScale.y, playerTransform.localScale.z);
        rb.mass = 1;

        if (characterController2D)
        {
            characterController2D.m_JumpForce = 1100;
        }
        
        if (inventory)
        {
            inventory.ConsumeItem();
        }
        
        currentParticles = normalParticles;
        if (currentParticles)
        {
            currentParticles.Play();
            StartCoroutine(StopParticlesAfterUse());
        }

        if (playerThrowing)
        {
            playerThrowing.NormalizeThrowMultiplier();
        }

        return true;
    }
    private IEnumerator StopParticlesAfterUse()
    {
        yield return new WaitForSeconds(0.5f); // Adjust the duration as needed
        currentParticles.Stop();
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GrowScript : MonoBehaviour
{

    [SerializeField] private int NormalSizeIndex = 2;
    [SerializeField] private List<float> jumps = new List<float>();
    [SerializeField] private List<float> masses = new List<float>(); 
    [SerializeField] private AudioClip growClip;
    [SerializeField] private AudioClip shrinkClip;
    [SerializeField] private AudioClip normalClip;
    private AudioSource audioSource;

    private Animator playerAnimator;
    private Transform playerTransform;
    private Rigidbody2D rb;
    private CharacterController2D characterController2D;
    private Inventory inventory;
    [SerializeField] private ParticleSystem growParicles;
    [SerializeField] private ParticleSystem shrinkParticles;
    [SerializeField] private ParticleSystem normalParticles;
    private ParticleSystem currentParticles;
    private PlayerThrowing playerThrowing;

    private int CurrentSize = 2;

    [SerializeField]
    private int MinSize = 0;

    [SerializeField]
    private int MaxSize = 4;

    void Start()
    {
        playerTransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        characterController2D = GetComponent<CharacterController2D>();
        inventory = GetComponent<Inventory>();
        playerThrowing = GetComponent<PlayerThrowing>();
        playerAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (inventory)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                string currentlyHolding = inventory.CheckInventory();
                if (currentlyHolding != "" && currentlyHolding != null)
                {
                    if (playerAnimator)
                    {
                        playerAnimator.Play("Eating", playerAnimator.GetLayerIndex("Eating"));
                    }

                    if (currentlyHolding == "grow")
                    {
                        Grow();
                    }
                    else if (currentlyHolding == "shrink")
                    {
                        Shrink();
                    }
                    else if (currentlyHolding == "normal")
                    {
                        Normal();
                    }
                }
            }
        }

        
    }

    public bool Grow()
    {
        if (CurrentSize >= masses.Count-1)
        {
            return false;
        }

        CurrentSize++;

        playerTransform.localScale = new Vector3(playerTransform.localScale.x * 1.5f, playerTransform.localScale.y * 1.5f, playerTransform.localScale.z);
        rb.mass = masses[CurrentSize];

        if (characterController2D)
        {
            characterController2D.m_JumpForce = jumps[CurrentSize];
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

        if(audioSource){
            audioSource.PlayOneShot(growClip);
        }

        return true;
    }

    public bool Shrink(){
        if (CurrentSize <= 0)
        {
            return false;
        }

        CurrentSize--;

        playerTransform.localScale = new Vector3(playerTransform.localScale.x / 1.5f, playerTransform.localScale.y / 1.5f, playerTransform.localScale.z);
        rb.mass = masses[CurrentSize];

        

        if (characterController2D)
        {
            characterController2D.m_JumpForce = jumps[CurrentSize];
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

        if(audioSource){
            audioSource.PlayOneShot(shrinkClip);
        }

        return true;
    }

    private bool Normal(){
        if(CurrentSize == 2){
            return false;
        }
        if(CurrentSize <= 0){
            playerTransform.localPosition = new Vector3(playerTransform.localPosition.x, playerTransform.localPosition.y + 0.5f, playerTransform.localPosition.z);
        }

        CurrentSize = NormalSizeIndex;

        playerTransform.localScale = new Vector3(playerTransform.localScale.x / Mathf.Abs(playerTransform.localScale.x), playerTransform.localScale.y / playerTransform.localScale.y, playerTransform.localScale.z);
        rb.mass = masses[CurrentSize];

        if (characterController2D)
        {
            characterController2D.m_JumpForce = jumps[CurrentSize];
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

        if(audioSource){
            audioSource.PlayOneShot(normalClip);
        }

        return true;
    }
    private IEnumerator StopParticlesAfterUse()
    {
        yield return new WaitForSeconds(0.5f); // Adjust the duration as needed
        currentParticles.Stop();
    }

    private void ResetSize() 
    {
       playerTransform.localScale = new Vector3(1,1,1); 
    }
        

}

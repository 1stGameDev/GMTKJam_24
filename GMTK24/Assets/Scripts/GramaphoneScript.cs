using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GramaphoneScript : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            animator.SetBool("talking", true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Player"){
            animator.SetBool("talking", false);
        }
    }
}

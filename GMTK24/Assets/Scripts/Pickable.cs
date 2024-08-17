using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{

    private bool playerInRange = false;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
           playerInRange = true;
           player = other.gameObject;
        }        
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player")){
            playerInRange = false;
            player = null;
        }
    }

    private void PickUp(){
        if(player.GetComponent<Inventory>().PickUpItem(this.tag, this.GetComponent<SpriteRenderer>().sprite)){
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{

    private MushroomSpawnerScript spawner;
    private bool playerInRange = false;
    private GameObject player;
   
   public void Initialize(MushroomSpawnerScript spw){
        spawner = spw;
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
            spawner.Picked();
            Destroy(this.gameObject);
        }
    }
}

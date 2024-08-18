using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField]
    private GameObject ParentObj;
    [SerializeField]
    private SpriteRenderer MushroomSpriteRenderer;

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
        if (player)
        {
            Inventory inv = player.GetComponent<Inventory>();
            if (inv)
            {
                Sprite sprite = null;
                if (MushroomSpriteRenderer)
                {
                    sprite = MushroomSpriteRenderer.sprite;
                }

                string name = "";
                Mushroom mushroom = ParentObj.GetComponent<Mushroom>();
                if (mushroom)
                {
                    name = mushroom.GetInventoryName();
                }

                if (inv.PickUpItem(ParentObj, name, sprite))
                {
                    if (spawner)
                    {
                        spawner.Picked();
                    }

                    if (ParentObj)
                    {
                        // Disable instead of destroy so that the references aren't deleted
                        ParentObj.SetActive(false);
                    }
                }
            }
        }
    }
}

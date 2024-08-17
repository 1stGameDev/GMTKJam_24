using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomSpawnerScript : MonoBehaviour
{
    public GameObject mushroomPrefab;
    [SerializeField] private float spawnDelay = 1.0f;
    private float spawnTimer = 0.0f;
    private bool isMushroom = false;

    void Update(){
        if(!isMushroom){
            spawnTimer += Time.deltaTime;
            if(spawnTimer >= spawnDelay){
                isMushroom = true;
                spawnTimer = 0.0f;
                Spawn();
            }
        }
    }
    
    private void Spawn(){
        GameObject newObj = Instantiate(mushroomPrefab, transform.position, Quaternion.identity);
        if (newObj)
        {
            Mushroom mushroom = newObj.GetComponent<Mushroom>();
            if (mushroom)
            {
                Pickable pickable = mushroom.GetPickableComponent();
                if (pickable)
                {
                    pickable.Initialize(this);
                }
            }
        }
    }

    public void Picked(){
        isMushroom = false;
    }
}

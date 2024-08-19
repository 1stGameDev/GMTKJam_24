using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomSpawnerScript : MonoBehaviour
{
    public GameObject mushroomPrefab;
    [SerializeField] private float spawnDelay = 1.0f;
    private ParticleSystem particles;
    private float spawnTimer = 0.0f;
    private bool isMushroom = false;

    void Start(){
        particles = GetComponent<ParticleSystem>();
    }

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
                    particles.Play();
                    StartCoroutine(StopParticlesAfterUse());
                    pickable.Initialize(this);
                }
            }
        }
    }

    private IEnumerator StopParticlesAfterUse()
    {
        yield return new WaitForSeconds(0.5f); // Adjust the duration as needed
        particles.Stop();
    }

    public void Picked(){
        isMushroom = false;
    }
}
